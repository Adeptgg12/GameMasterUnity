using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextMenuGame : MonoBehaviour
{
    public Button playGame;
    public void nextScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    public void nextSceneStoryHTML()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }

    public void nextSceneMiniGameHtml()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }
    public void nextSceneMiniGameCssl()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }
    public void nextSceneStoryCss()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(8);
    }
}
