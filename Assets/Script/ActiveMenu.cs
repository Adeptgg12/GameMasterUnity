using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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
    public Button BigislandHTMLCss;

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
        if (keyint >= 6)
        {
            BigislandHTMLCss.interactable = true;
            AudioSystem.Instance.PlaySFX("Sfx_click");
        }
        else
        {
            BigislandHTMLCss.interactable = false;
        }
    }

    public async void Key()
    {
        connection = new Connection();
        StartCoroutine(StoryHTMLCSS());
    }

    IEnumerator StoryHTMLCSS()
    {
        // Use UnityWebRequest to set headers
        UnityWebRequest www = UnityWebRequest.Get(connection.storyHTMLCSS);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        // Wait until the request is completed
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            keystr = www.downloadHandler.text;
            keyint = int.Parse(keystr);
        }
        else
        {
            Debug.LogError("Request failed: " + www.error);
        }
    }

    public void ActiveIsland()
    {
        //call story key
        Key();
        Debug.Log("keyint = " + keyint);
        if (activeMenuisland1.activeSelf != true && activeMenuisland2.activeSelf != true)
        {
            activeMenuisland1.SetActive(true);
            activeMenuisland2.SetActive(true);
            userpass.SetActive(false);
            islandonegamelist.SetActive(false);
            score.SetActive(false);
            setting.SetActive(false);

            AudioSystem.Instance.PlaySFX("Sfx_click");
            if (keyint >= 6) {
                BigislandHTMLCss.interactable = true;
                AudioSystem.Instance.PlaySFX("Sfx_click");
            }
            else
            {
                BigislandHTMLCss.interactable = false;
            }
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
            island2.interactable = keyint >= 2;
            island3.interactable = keyint >= 3;
            island4.interactable = keyint >= 5;


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

    public void nextSceneIsland2MiniGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(7);
    }
}
