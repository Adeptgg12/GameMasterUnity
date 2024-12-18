using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserScore : MonoBehaviour
{
    public TextMeshProUGUI textScoreKey;
    public TextMeshProUGUI textScoreSpeedhtml;
    public TextMeshProUGUI textScoreacchtml;
    public TextMeshProUGUI textScoreSpeedCSS;
    public TextMeshProUGUI textScoreaccCSS;
    public TextMeshProUGUI textScoreSpeedHTMLCSS;
    public TextMeshProUGUI textScoreaccHTMLCSS;

    private Connection connection;

    public void CallScoreMiniGameSpeedandACC()
    {
        connection = new Connection();
        StartCoroutine(ScoreMiniGameHtmlSpeed());
        StartCoroutine(ScoreMiniGameHtmlACC());
        StartCoroutine(ScoreMiniGameCSSSpeed());
        StartCoroutine(ScoreMiniGameCSSACC());
        StartCoroutine(ScoreMiniGameHTMLCSSSpeed());
        StartCoroutine(ScoreMiniGameHTMLCSSACC());
        StartCoroutine(StoryHTMLCSS());

    }
    ////////////////////////////////////////STORY////////////////////////////////////////////////////
    IEnumerator StoryHTMLCSS()
    {
        WWW www = new WWW(connection.userstoryHTMLCSS);
        yield return www;
        Debug.Log(www.text);
        textScoreKey.text = www.text;
        textScoreKey.color = Color.black;

    }

    ////////////////////////////////////////MINIGAME HTML////////////////////////////////////////////////////

    IEnumerator ScoreMiniGameHtmlSpeed() {
        WWW www = new WWW(connection.scoreMiniGameHtmlSpeed);
        yield return www;

        Debug.Log(www.text);
        textScoreSpeedhtml.text = www.text;
        textScoreSpeedhtml.color = Color.black;

    }
    IEnumerator ScoreMiniGameHtmlACC()
    {
        WWW www = new WWW(connection.scoreMiniGameHtmlACC);
        yield return www;
        Debug.Log(www.text);
        textScoreacchtml.text = www.text;
        textScoreacchtml.color = Color.black;

    }
    //////////////////////////////////////////MINIGAME CSS//////////////////////////////////////////////////////
    IEnumerator ScoreMiniGameCSSSpeed()
    {
        WWW www = new WWW(connection.scoreMiniGameCSSSpeed);
        yield return www;

        Debug.Log(www.text);
        textScoreSpeedCSS.text = www.text;
        textScoreSpeedCSS.color = Color.black;

    }
    IEnumerator ScoreMiniGameCSSACC()
    {
        WWW www = new WWW(connection.scoreMiniGameCSSACC);
        yield return www;
        Debug.Log(www.text);
        textScoreaccCSS.text = www.text;
        textScoreaccCSS.color = Color.black;

    }
    //////////////////////////////////////////MINIGAME HTML CSS//////////////////////////////////////////////////////

    IEnumerator ScoreMiniGameHTMLCSSSpeed()
    {
        WWW www = new WWW(connection.scoreMiniGameHTMLCSSSpeed);
        yield return www;

        Debug.Log(www.text);
        textScoreSpeedHTMLCSS.text = www.text;
        textScoreSpeedHTMLCSS.color = Color.black;

    }
    IEnumerator ScoreMiniGameHTMLCSSACC()
    {
        WWW www = new WWW(connection.scoreMiniGameHTMLCSSACC);
        yield return www;
        Debug.Log(www.text);
        textScoreaccHTMLCSS.text = www.text;
        textScoreaccHTMLCSS.color = Color.black;

    }

}
