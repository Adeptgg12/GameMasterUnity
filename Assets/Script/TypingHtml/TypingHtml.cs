using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TypingHtml : MonoBehaviour
{
    public GameObject ScoreScreen;
    public UnityEngine.UI.Button buttonok;
    public TextMeshProUGUI wordOutput = null;
    public TextMeshProUGUI wpmOutput = null;
    float wpm;
    float accuracy;
    private Connection connection; // connection string
    public TextMeshProUGUI wordIngame = null;
    public TextMeshProUGUI wpmIngame = null;
    public TextMeshProUGUI accuracyOutput = null;
    public TextMeshProUGUI timerOutput = null; // UI for countdown timer
    public WordBank WordBank = null;
    public bool isStart;
    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    private float startTime;
    [SerializeField]
    private float timeRemaining = 60f; // Total time in seconds
    private int correctWords = 0;
    private int totalTypedLetters = 0;
    private int correctTypedLetters = 0;

    private bool isGameActive = true; // To check if the game is still active

    private void Start()
    {
        if (WordBank == null)
        {
            Debug.LogError("WordBank is not assigned.");
            enabled = false;
            return;
        }

        buttonok.interactable = true;
        ScoreScreen.SetActive(false);
        startTime = Time.time;
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        currentWord = WordBank.GetWord();
        if (!string.IsNullOrEmpty(currentWord))
        {
            SetRemainingWord(currentWord);
        }
        else
        {
            Debug.LogWarning("No more words available in WordBank.");
            EndGame();
        }
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.richText = false;
        wordOutput.text = remainingWord;
    }

    private void Update()
    {
        if (isGameActive)
        {
            CheckInput();
            UpdateStats();
            UpdateTimer();
        }
        if (Input.anyKeyDown)
        {
            isStart = true;
        }
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        totalTypedLetters++;
        if (IsCorrectLetter(typedLetter))
        {
            correctTypedLetters++;
            RemoveLetter();

            if (IsWordComplete())
            {
                correctWords++;
                SetCurrentWord();
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        if (string.IsNullOrEmpty(remainingWord))
            return false;

        return remainingWord[0].ToString().Equals(letter, StringComparison.OrdinalIgnoreCase);
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return string.IsNullOrEmpty(remainingWord);
    }

    private void UpdateStats()
    {
        float elapsedTime = (Time.time - startTime) / 60f; // Convert to minutes
        if (elapsedTime == 0) elapsedTime = 1; // Avoid division by zero

        // Update WPM using the new formula (GWAM)
        wpm = ((float)totalTypedLetters / 5f) / elapsedTime;

        // Update accuracy
        accuracy = (totalTypedLetters > 0)
            ? ((float)correctTypedLetters / totalTypedLetters) * 100f
            : 0f;

        // Update in-game UI
        wpmIngame.text = $"WPM: {wpm:F1}";
        wordIngame.text = $"ACC: {accuracy:F1}%";
    }

    private void UpdateTimer()
    {
        if (isStart == true)
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
        else
        {
        }

    }

    private void EndGame()
    {
        isGameActive = false;
        ScoreScreen.SetActive(true);

        float elapsedTime = (Time.time - startTime) / 60f; // Convert to minutes

        // Calculate final WPM using the new formula (GWAM)
        wpm = ((float)totalTypedLetters / 5f) / elapsedTime;

        // Calculate final accuracy
        accuracy = (totalTypedLetters > 0)
            ? ((float)correctTypedLetters / totalTypedLetters) * 100f
            : 0f;

        // Update UI with final stats
        wpmOutput.text = wpm.ToString("F2");
        accuracyOutput.text = $"{accuracy:F2}";

        // Connect to the database and update scores
        connection = new Connection();
        StartCoroutine(UpdateBestScore(connection.scoreMiniGameHtmlSpeed, connection.UpdateSpeedHtml, wpm, accuracy));
    }
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
                form.AddField("TypingHTMLSpeedscore", newWpm.ToString("F2"));
                form.AddField("TypingHTMLACCscore", newAccuracy.ToString("F2"));

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
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
