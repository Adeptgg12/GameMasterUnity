using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddKeyUser : MonoBehaviour
{
    string keystr;
    private int keyint;
    public GameObject Okcontinued;
    private Connection connection;
    public async void CallStorykey()
    {
        connection = new Connection();
        StartCoroutine(StoryHTMLCSS());

    }
    ////////////////////////////////////////STORY////////////////////////////////////////////////////
    IEnumerator StoryHTMLCSS()
    {
        WWW www = new WWW(connection.storyHTMLCSS);
        yield return www;
        keystr = www.text;
        keyint = int.Parse(keystr);
        keyint += 1;
        Debug.Log("1."+keyint);
        StartCoroutine(UpdateKey());
        
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
