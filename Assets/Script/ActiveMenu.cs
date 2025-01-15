using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public class ActiveMenu : MonoBehaviour
{
    public GameObject activeMenuisland1;
    public GameObject activeMenuisland2;
    public GameObject userpass;
    public GameObject score;
    public GameObject islandonegamelist;
    public GameObject setting;

    public Button island1;
    public Button island2;
    public Button island3;
    public Button island4;

    string keystr;
    private int keyint;
    private Connection connection;
    public void Start()
    {
        Key();
        island2.interactable = false;
        island3.interactable = false;
        island4.interactable = false;
        userpass.SetActive(false);
        score.SetActive(false);
        islandonegamelist.SetActive(false);
        setting.SetActive(false);
    }
    public async void Key()
    {
        connection = new Connection();
        StartCoroutine(StoryHTMLCSS());
    }
    IEnumerator StoryHTMLCSS()
    {
        WWW www = new WWW(connection.storyHTMLCSS);
        yield return www;
        keystr = www.text;
        keyint = int.Parse(keystr);
    }
    public void ActiveIsland()
    {
        if (activeMenuisland1.activeSelf != true && activeMenuisland2.activeSelf != true)
        {
            activeMenuisland1.SetActive(true);
            activeMenuisland2.SetActive(true);
            userpass.SetActive(false);
            islandonegamelist.SetActive(false);
            score.SetActive(false);
            setting.SetActive(false);

            AudioSystem.Instance.PlaySFX("Sfx_click");
        }
    }
    public void ActiveUserpass()
    {
        if (userpass.activeSelf != true && userpass.activeSelf != true)
        {
            activeMenuisland1.SetActive(false);
            activeMenuisland2.SetActive(false);
            score.SetActive(false);
            islandonegamelist.SetActive(false);
            userpass.SetActive(true);
            setting.SetActive(false);

            AudioSystem.Instance.PlaySFX("Sfx_click");
        }
    }

    public void ActiveScore()
    {
        if (score.activeSelf != true && score.activeSelf != true)
        {
            activeMenuisland1.SetActive(false);
            activeMenuisland2.SetActive(false);
            userpass.SetActive(false);
            islandonegamelist.SetActive(false);
            score.SetActive(true);
            setting.SetActive(false);

            AudioSystem.Instance.PlaySFX("Sfx_click");
        }
    }

    public void Activeislandonegamelist()
    {
        //call story key
        Key();
        Debug.Log("keyint = " + keyint);
        if (islandonegamelist.activeSelf != true && islandonegamelist.activeSelf != true)
        {
            activeMenuisland1.SetActive(false);
            activeMenuisland2.SetActive(false);
            userpass.SetActive(false);
            score.SetActive(false);
            islandonegamelist.SetActive(true);
            setting.SetActive(false);

            AudioSystem.Instance.PlaySFX("Sfx_click");
            if (keyint >= 2) {
                island2.interactable = true;
            }
            if (keyint >= 4)
            {
                island3.interactable = true;
            }
            if (keyint >= 6)
            {
                island4.interactable = true;
            }
        }
    }

    public void ActiveSettings()
    {
        if (setting.activeSelf != true && setting.activeSelf != true)
        {
            activeMenuisland1.SetActive(false);
            activeMenuisland2.SetActive(false);
            userpass.SetActive(false);
            score.SetActive(false);
            islandonegamelist.SetActive(false);
            setting.SetActive(true);

            AudioSystem.Instance.PlaySFX("Sfx_click");
        }
    }

    public void nextSceneIsland2MiniGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(7);
    }
}
