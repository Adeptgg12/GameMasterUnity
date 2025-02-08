using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TypingHtml : MonoBehaviour
{
    [Header("Component")]
    public GameObject ScoreScreen;
    public UnityEngine.UI.Button buttonok;
    public TextMeshProUGUI wordOutput = null;
    public TextMeshProUGUI typedWordOutput = null; // แสดงข้อความที่ผู้ใช้พิมพ์
    public TextMeshProUGUI wpmOutput = null;
    public TextMeshProUGUI accuracyOutput = null;
    public TextMeshProUGUI timerOutput = null;
    public TextMeshProUGUI wordIngame = null;
    public TextMeshProUGUI wpmIngame = null;
    public WordBank WordBank = null;
    private Connection connection;
    private float wpm;
    private float accuracy;
    private string remainingWord = string.Empty;
    [SerializeField] string currentWord = string.Empty;
    [SerializeField] string typedLetters = "";  // เก็บข้อความที่ผู้ใช้พิมพ์
    private float startTime;
    private float timeRemaining = 60f;
    private int correctWords = 0;
    private int totalTypedLetters = 0;
    private int correctTypedLetters = 0;
    private bool isGameActive = true;
    private bool isStart = false;
    private bool isCorrect = true;
    private int keyint;
    string keystr;
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
            typedLetters = "";  // เคลียร์ข้อความที่พิมพ์แล้ว
            UpdateTypedWordDisplay();
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
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Backspace))
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
            {
                char keyCheck = keysPressed[0];
                char wordIn = wordOutput.text[0];
                if (keyCheck == wordIn)
                {
                    EnterLetter(keysPressed);
                    Debug.Log("Correct");
                }
                else
                {
                    EnterLetter(keysPressed);
                    Debug.Log("Incorrect");
                }
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        totalTypedLetters++;

        // ถ้าเป็นตัวอักษรที่ถูกต้อง
        if (IsCorrectLetter(typedLetter))
        {
            if (isCorrect)
            {
                correctTypedLetters++;
                // เพิ่มตัวอักษรที่ถูกต้องลงใน typedLetters
                typedLetters += typedLetter;
                RemoveLetter();

                // ถ้าคำเสร็จแล้ว
                if (IsWordComplete())
                {
                    correctWords++;
                    SetCurrentWord();
                }
            }
            else
            {
                typedLetters = typedLetters.Substring(0, typedLetters.Length - 1);

                correctTypedLetters++;
                // เพิ่มตัวอักษรที่ถูกต้องลงใน typedLetters
                typedLetters += typedLetter;
                RemoveLetter();

                // ถ้าคำเสร็จแล้ว
                if (IsWordComplete())
                {
                    correctWords++;
                    SetCurrentWord();
                }
                isCorrect = true;
            }
        }
        else
        {
            isCorrect = false;
            // ถ้าพิมพ์ผิด ต้องรอให้พิมพ์ตัวที่ถูกต้องแล้วจึงจะลบ
            if (typedLetters == "")
            {
                typedLetters += typedLetter;
            }
            else
            {
                if (typedLetters.Length < currentWord.Length && typedLetters[typedLetters.Length - 1] != currentWord[typedLetters.Length - 1])
                {
                    // แทนที่ตัวที่ผิดด้วยตัวที่พิมพ์ใหม่
                    typedLetters = typedLetters.Substring(0, typedLetters.Length - 1) + typedLetter;
                }
                else
                {
                    //RemoveLetter();
                    typedLetters += typedLetter; // เพิ่มตัวอักษรที่ผิด
                }
            }
        }
        // อัปเดตการแสดงผลข้อความที่พิมพ์
        UpdateTypedWordDisplay();
    }

    private void UpdateTypedWordDisplay()
    {
        string displayText = "";

        // ตรวจสอบแต่ละตัวอักษร
        for (int i = 0; i < typedLetters.Length; i++)
        {
            // เปรียบเทียบตัวที่พิมพ์กับคำที่ต้องพิมพ์
            if (i < currentWord.Length && typedLetters[i] == currentWord[i])
            {
                displayText += "<color=green>" + typedLetters[i] + "</color>";  // สีเขียวถ้าถูก
            }
            else if (i < currentWord.Length && typedLetters[i] != currentWord[i])
            {
                displayText += "<color=red>" + typedLetters[i] + "</color>";  // สีแดงถ้าผิด
            }
        }

        // แสดงข้อความในช่องแสดงผล
        typedWordOutput.text = displayText;
    }
    private bool IsCorrectLetter(string letter)
    {
        if (string.IsNullOrEmpty(remainingWord))
            return false;

        return remainingWord[0].ToString().Equals(letter, StringComparison.OrdinalIgnoreCase);
    }

    private void RemoveLetter()
    {
        if (remainingWord.Length > 0)
        {
            remainingWord = remainingWord.Substring(1);
            wordOutput.text = remainingWord;  // อัปเดตคำที่เหลือ
        }
    }

    private bool IsWordComplete()
    {
        return string.IsNullOrEmpty(remainingWord);
    }

    private void UpdateStats()
    {
        float elapsedTime = (Time.time - startTime) / 60f;
        if (elapsedTime == 0) elapsedTime = 1;

        wpm = ((float)totalTypedLetters / 5f) / elapsedTime;
        accuracy = (totalTypedLetters > 0) ? ((float)correctTypedLetters / totalTypedLetters) * 100f : 0f;

        wpmIngame.text = $"WPM: {wpm:F1}";
        wordIngame.text = $"ACC: {accuracy:F1}%";
    }

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


    private void EndGame()
    {
        isGameActive = false;
        ScoreScreen.SetActive(true);

        float elapsedTime = (Time.time - startTime) / 60f;

        wpm = ((float)totalTypedLetters / 5f) / elapsedTime;
        accuracy = (totalTypedLetters > 0) ? ((float)correctTypedLetters / totalTypedLetters) * 100f : 0f;

        wpmOutput.text = wpm.ToString("F2");
        accuracyOutput.text = $"{accuracy:F2}";
        connection = new Connection();
        
        StartCoroutine(StoryHTMLCSS(5));
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

    IEnumerator StoryHTMLCSS(int number)
    {
        // ใช้ UnityWebRequest และตั้งค่า header
        UnityWebRequest www = UnityWebRequest.Get(connection.storyHTMLCSS);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        // รอให้คำขอเสร็จสิ้น
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            keystr = www.downloadHandler.text;
            keyint = int.Parse(keystr);
            CheckKey(number);
        }
        else
        {
            Debug.LogError("Request failed: " + www.error);
        }
    }
    public void CheckKey(int number)
    {
        Debug.Log("number = " + number);
        Debug.Log("keyint = " + keyint);

        if (number == 5)
        {
            if (keyint < 3)
            {
                Debug.Log("if5");
                keyint += 1;
                Debug.Log(keyint);
                StartCoroutine(UpdateKey());
            }
        }
    }
    IEnumerator UpdateKey()
    {
        WWWForm form = new WWWForm();
        form.AddField("StoryKeyValue", keyint);

        // ใช้ UnityWebRequest และตั้งค่า header
        UnityWebRequest www = UnityWebRequest.Post(connection.updateKey, form);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        // รอให้คำขอเสร็จสิ้น
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Request failed: " + www.error);
        }
    }
    public void LoadScene()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
