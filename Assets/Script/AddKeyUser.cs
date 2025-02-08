using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AddKeyUser : MonoBehaviour
{
    string keystr;
    private int keyint;
    public GameObject Okcontinued;
    private Connection connection;
    public GameObject gift;

    public async void CallStorykey(int number)
    {
        Debug.Log("CallStorykey...");
        connection = new Connection();
        StartCoroutine(StoryHTMLCSS(number));
    }

    ////////////////////////////////////////STORY////////////////////////////////////////////////////
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
        if (number == 1)
        {
            if (keyint < 1)
            {
                Debug.Log("if1");
                gift.SetActive(true);
                keyint += 1;
                Debug.Log(keyint);
                StartCoroutine(UpdateKey());
            }
        }
        else if (number == 2)
        {
            if (keyint < 2)
            {
                Debug.Log("if2");
                gift.SetActive(true);
                keyint += 1;
                Debug.Log(keyint);
                StartCoroutine(UpdateKey());
            }
        }
        else if (number == 3)
        {
            if (keyint < 4)
            {
                Debug.Log("if3");
                gift.SetActive(true);
                keyint += 1;
                Debug.Log(keyint);
                StartCoroutine(UpdateKey());
            }
        }
        else if (number == 4)
        {
            if (keyint < 5)
            {
                Debug.Log("if4");
                gift.SetActive(true);
                keyint += 1;
                Debug.Log(keyint);
                StartCoroutine(UpdateKey());
            }
        }
        else if (number == 5)
        {
            if (keyint < 3)
            {
                Debug.Log("if5");
                gift.SetActive(true);
                keyint += 1;
                Debug.Log(keyint);
                StartCoroutine(UpdateKey());
            }
        }
        else
        {
            Okcontinued.SetActive(false);
        }
    }

    ////////////////////////////////////////Add Key////////////////////////////////////////////////////
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

    public void OktoCon()
    {
        Okcontinued.SetActive(false);
    }
}
