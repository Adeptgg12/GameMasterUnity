using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class WordManager : MonoBehaviour
{

	[Header("Words Mechanics")]
	public List<Word> words;

	public WordSpawner wordSpawner;

	bool hasActiveWord;
	private Word activeWord;

	//BoxSpawner
	public BoxSpawnManager boxSpawner;

	[Header("UI")]
	//Inputfield
	public TMP_InputField showText;
	public Animator anim;

    public GameObject ScoreScreen;
    [Header("Timer")]
	[SerializeField] bool isStart;
	[SerializeField] float timeRemaining = 60f;
	[SerializeField] bool isGameActive = true;
	[SerializeField] TMP_Text timerOutput;
	[SerializeField] TMP_Text wpmOutput;
	[SerializeField] TMP_Text accuracyOutput;
	[SerializeField] TMP_Text wpmOutputEnd;
	[SerializeField] TMP_Text accuracyOutputEnd;

    //connection
    private Connection connection; // connection string
    float startTime;
	float wpm;
	float accuracy;
	int totalTypedLetters = 0;
	int correctTypedLetters = 0;

	#region Start Script
	public void Start()
	{
		startTime += Time.time;
        ScoreScreen.SetActive(false);
    }

	void Update()
	{
		if (!hasActiveWord)
		{
			showText.text = "";
		}
		if (isGameActive) {
            UpdateTimer();
            UpdateStats();
        }
            
	}
	#endregion

	public void AddWord()
	{
		Word word = new Word(WordGenerator.GetRandomWord(), wordSpawner.SpawnWord());
		//Debug.Log(word.word);

		words.Add(word);
	}

	#region Input form keyboard
	public void TypeLetter(char letter)
	{
		totalTypedLetters++;

		if (!isStart)
		{
			isStart = true;
		}

		if (hasActiveWord)
		{
			//ใส่ Letter หลังจากตัวแรก
			if (activeWord.GetNextLetter() == letter)
			{
				activeWord.TypeLetter();
				boxSpawner.SpawnObject();
				showText.text += letter;

				correctTypedLetters++;
				Debug.Log("Correct letter but not first letter");
			}
			else
			{
				anim.SetTrigger("Incorrect");
				Debug.Log("Not correct");
			}
		}
		else
		{
			if (words[0].GetNextLetter() == letter)
			{
				activeWord = words[0];
				hasActiveWord = true;
				words[0].TypeLetter();
				boxSpawner.SpawnObject();
				correctTypedLetters++;
				Debug.Log("Correct first letter");

				//ShowText
				showText.text += letter;
				//break;
			}
			else
			{
				anim.SetTrigger("Incorrect");
				Debug.Log("Not correct");
			}
			//ใส่ Letter ตัวแรก
			// foreach (Word word in words)
			// {
			// 	if (word.GetNextLetter() == letter)
			// 	{
			// 		activeWord = word;
			// 		hasActiveWord = true;
			// 		word.TypeLetter();
			// 		boxSpawner.SpawnObject();
			// 		correctTypedLetters++;
			// 		Debug.Log("Correct first letter");

			// 		//ShowText
			// 		showText.text += letter;
			// 		break;
			// 	}
			// 	else
			// 	{
			// 		anim.SetTrigger("Incorrect");
			// 		Debug.Log("Not correct");
			// 	}
			// }
		}

		if (hasActiveWord && activeWord.WordTyped())
		{
			hasActiveWord = false;
			words.Remove(activeWord);
		}
	}
	#endregion

	#region Timer
	private void UpdateTimer()
	{
		if (isStart)
		{
			timeRemaining -= Time.deltaTime;
			if (timeRemaining <= 0)
			{
				timeRemaining = 0;
				isGameActive = false;
				EndGame();
			}

			timerOutput.text = $"{timeRemaining:F1}s";
		}
	}

	private void UpdateStats()
	{
		float elapsedTime = (Time.time - startTime) / 60f;
		if (elapsedTime == 0) elapsedTime = 1;

		wpm = ((float)totalTypedLetters / 5f) / elapsedTime;
		accuracy = (totalTypedLetters > 0) ? ((float)correctTypedLetters / totalTypedLetters) * 100f : 0f;

		wpmOutput.text = $"WPM: {wpm:F1}";
		accuracyOutput.text = $"ACC: {accuracy:F1}%";
	}

	private void EndGame()
	{
		isGameActive = false;
		isStart = false;
		ScoreScreen.SetActive(true);

		float elapsedTime = (Time.time - startTime) / 60f;

		wpm = ((float)totalTypedLetters / 5f) / elapsedTime;
		accuracy = (totalTypedLetters > 0) ? ((float)correctTypedLetters / totalTypedLetters) * 100f : 0f;

		wpmOutputEnd.text = wpm.ToString("F2");
		accuracyOutputEnd.text = $"{accuracy:F2}";

        connection = new Connection();
        StartCoroutine(UpdateBestScore(connection.scoreMiniGameCSSSpeed, connection.UpdateSpeedCss, wpm, accuracy));
    }
    #endregion
    #region connection
    IEnumerator UpdateBestScore(string getUrl, string updateUrl, float newWpm, float newAccuracy)
    {
        UnityWebRequest www = UnityWebRequest.Get(getUrl);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string serverScoreText = www.downloadHandler.text;
            Debug.Log($"Server WPM Score: {serverScoreText}");

            if (serverScoreText == "None" || (float.TryParse(serverScoreText, out float serverWpm) && newWpm > serverWpm))
            {
                WWWForm form = new WWWForm();
                form.AddField("TypingCssSpeedscore", newWpm.ToString("F2"));
                form.AddField("TypingCssACCscore", newAccuracy.ToString("F2"));

                UnityWebRequest updateRequest = UnityWebRequest.Post(updateUrl, form);
                updateRequest.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                yield return updateRequest.SendWebRequest();

                if (updateRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log($"Updated WPM and Accuracy: {updateRequest.downloadHandler.text}");
                }
                else
                {
                    Debug.LogError($"Error updating scores: {updateRequest.error}");
                }
            }
            else
            {
                Debug.Log($"Current server WPM ({serverWpm:F2}) is higher or equal to the new WPM ({newWpm:F2}).");
            }
        }
        else
        {
            Debug.LogError($"Error fetching WPM score: {www.error}");
        }
    }
    #endregion
}
