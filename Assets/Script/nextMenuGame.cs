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
}
