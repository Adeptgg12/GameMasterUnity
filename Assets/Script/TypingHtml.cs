using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingHtml : MonoBehaviour
{
    public GameObject ScoreScreen;
    public UnityEngine.UI.Button buttonok;
    public TextMeshProUGUI wordOutput = null;
    public TextMeshProUGUI wpmOutput = null;

    public TextMeshProUGUI wordIngame = null;
    public TextMeshProUGUI wpmIngame = null;
    public TextMeshProUGUI accuracyOutput = null;
    public TextMeshProUGUI timerOutput = null; // UI for countdown timer

    private string remainingWord = string.Empty;
    private string currentWord = "testword";

    private float startTime;
    private float timeRemaining = 60f; // Total time in seconds
    private int correctWords = 0;
    private int totalTypedLetters = 0;
    private int correctTypedLetters = 0;

    private bool isGameActive = true; // To check if the game is still active

    private void Start()
    {
        buttonok.interactable = true;
        ScoreScreen.SetActive(false);
        startTime = Time.time;
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        // Get new word from a word bank (this is just a placeholder)
        SetRemainingWord(currentWord);
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
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    private void UpdateStats()
    {
        float elapsedTime = Time.time - startTime;
        float wpm = (correctWords / (elapsedTime / 60f));
        float accuracy = (totalTypedLetters > 0)
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
        ScoreScreen.SetActive(true);
        float elapsedTime = Time.time - startTime;
        float wpm = (correctWords / (elapsedTime / 60f));
        float accuracy = (totalTypedLetters > 0)
            ? ((float)correctTypedLetters / totalTypedLetters) * 100f
            : 0f;

        // Display WPM and Accuracy
        wpmOutput.text = wpm.ToString("F1"); // Format to one decimal place
        accuracyOutput.text = accuracy.ToString("F1"); // Add percentage symbol
    }


    public void loadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
