using System;
using System.Collections;
using UnityEngine;

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
        WWW www = new WWW(connection.storyHTMLCSS);
        yield return www;
        keystr = www.text;
        keyint = int.Parse(keystr);
        CheckKey(number);
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
        WWW www = new WWW(connection.updateKey, form);
        yield return www;
        Debug.Log(www.text);
    }


    public void OktoCon() {
        Okcontinued.SetActive(false);
    }




}
