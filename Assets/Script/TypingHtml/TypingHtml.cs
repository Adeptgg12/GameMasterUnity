using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public class TypingHtml : MonoBehaviour
{
    public GameObject ScoreScreen;
    public UnityEngine.UI.Button buttonok;
    public TextMeshProUGUI wordOutput = null;
    public TextMeshProUGUI wpmOutput = null;
    float wpm;
    float accuracy;
    private Connection connection; // connection string
    string htmlspeed;
    string htmlacc;
    float htmlspeedint;
    float htmlaccint;
    public TextMeshProUGUI wordIngame = null;
    public TextMeshProUGUI wpmIngame = null;
    public TextMeshProUGUI accuracyOutput = null;
    public TextMeshProUGUI timerOutput = null; // UI for countdown timer
    public WordBank WordBank = null;

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
        float elapsedTime = Time.time - startTime;
        if (elapsedTime == 0) elapsedTime = 1; // Avoid division by zero

        wpm = (correctWords / (elapsedTime / 60f));
        accuracy = (totalTypedLetters > 0)
            ? ((float)correctTypedLetters / totalTypedLetters) * 100f
            : 0f;

        wpmIngame.text = $"WPM: {wpm:F1}";
        wordIngame.text = $"ACC: {accuracy:F1}%";

        Debug.Log(wpmOutput.text = $"WPM: {wpm:F1}");
        Debug.Log(accuracyOutput.text = $"Accuracy: {accuracy:F1}%");
    }

    private void UpdateTimer()
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

    private void EndGame()
    {
        isGameActive = false;
        ScoreScreen.SetActive(true);
        float elapsedTime = Time.time - startTime;
        wpm = (correctWords / (elapsedTime / 60f));
        accuracy = (totalTypedLetters > 0)
            ? ((float)correctTypedLetters / totalTypedLetters) * 100f
            : 0f;

        wpmOutput.text = wpm.ToString("F2");
        accuracyOutput.text = $"{accuracy:F2}%";

        //connect db
        connection = new Connection();
        StartCoroutine(TypingHtmlSpeedDb());
        StartCoroutine(TypingAccSpeedDb());


    }

    IEnumerator TypingHtmlSpeedDb()
    {
        WWW www = new WWW(connection.scoreMiniGameHtmlSpeed);
        yield return www;
        htmlspeed = www.text;
        Debug.Log(htmlspeed);
        if (htmlspeed != "None") {
            htmlspeedint = float.Parse(htmlspeed);
            if (htmlspeedint < wpm) {
                StartCoroutine(UpdateSpeedHtml(wpm));
                Debug.Log("htmlspeedint < wpm");
            }
        }
        if (htmlspeed == "None") 
        {
            StartCoroutine(UpdateSpeedHtml(wpm));
        }
        
        Debug.Log(htmlspeedint);



    }

    IEnumerator UpdateSpeedHtml(float speedhtml)
    {
        WWWForm form = new WWWForm();
        form.AddField("TypingHTMLSpeedscore", speedhtml.ToString("F2"));
        WWW www = new WWW(connection.UpdateSpeedHtml, form);
        yield return www;
        Debug.Log(www.text);
    }

    IEnumerator TypingAccSpeedDb()
    {
        WWW www = new WWW(connection.scoreMiniGameHtmlACC);
        yield return www;
        htmlacc = www.text;
        Debug.Log(htmlacc);
        if (htmlacc != "None")
        {
            htmlaccint = float.Parse(htmlacc);
            if (htmlaccint < accuracy)
            {
                Debug.Log("htmlaccint < accuracy");
                StartCoroutine(UpdateAccHtml(accuracy));
            }
        }
        if (htmlacc == "None")
        {
            StartCoroutine(UpdateAccHtml(accuracy));
        }

        Debug.Log(htmlacc);


    }

    IEnumerator UpdateAccHtml(float acchtml)
    {
        WWWForm form = new WWWForm();
        form.AddField("TypingHTMLACCscore", acchtml.ToString("F2"));
        WWW www = new WWW(connection.UpdateAccHtml, form);
        yield return www;
        Debug.Log(www.text);
    }
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
