using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class UserScore : MonoBehaviour
{
    public TextMeshProUGUI textScoreKey;
    public TextMeshProUGUI textScoreSpeedhtml;
    public TextMeshProUGUI textScoreacchtml;
    public TextMeshProUGUI textScoreSpeedCSS;
    public TextMeshProUGUI textScoreaccCSS;
    public TextMeshProUGUI textScoreSpeedHTMLCSS;
    public TextMeshProUGUI textScoreaccHTMLCSS;
    public TextMeshProUGUI textScoreResult;
    //img rank
    public GameObject imgExcellent;
    public GameObject imgGood;
    public GameObject imgAverage;
    public GameObject imgFair;
    public GameObject imgPoor;

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
        StartCoroutine(ResultAll());
        StartCoroutine(scoreResult());

    }

    ////////////////////////////////////////STORY////////////////////////////////////////////////////
    IEnumerator StoryHTMLCSS()
    {
        UnityWebRequest www = UnityWebRequest.Get(connection.userstoryHTMLCSS);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);
            textScoreKey.text = www.downloadHandler.text;
            textScoreKey.color = Color.black;
        }
        else
        {
            Debug.LogError("Failed to fetch story: " + www.error);
        }
    }

    ////////////////////////////////////////MINIGAME HTML////////////////////////////////////////////////////
    IEnumerator ScoreMiniGameHtmlSpeed()
    {
        UnityWebRequest www = UnityWebRequest.Get(connection.scoreMiniGameHtmlSpeed);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);
            textScoreSpeedhtml.text = www.downloadHandler.text;
            textScoreSpeedhtml.color = Color.black;
        }
        else
        {
            Debug.LogError("Failed to fetch HTML speed score: " + www.error);
        }
    }

    IEnumerator ScoreMiniGameHtmlACC()
    {
        UnityWebRequest www = UnityWebRequest.Get(connection.scoreMiniGameHtmlACC);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);
            textScoreacchtml.text = www.downloadHandler.text;
            textScoreacchtml.color = Color.black;
        }
        else
        {
            Debug.LogError("Failed to fetch HTML ACC score: " + www.error);
        }
    }

    //////////////////////////////////////////MINIGAME CSS////////////////////////////////////////////////////
    IEnumerator ScoreMiniGameCSSSpeed()
    {
        UnityWebRequest www = UnityWebRequest.Get(connection.scoreMiniGameCSSSpeed);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);
            textScoreSpeedCSS.text = www.downloadHandler.text;
            textScoreSpeedCSS.color = Color.black;
        }
        else
        {
            Debug.LogError("Failed to fetch CSS speed score: " + www.error);
        }
    }

    IEnumerator ScoreMiniGameCSSACC()
    {
        UnityWebRequest www = UnityWebRequest.Get(connection.scoreMiniGameCSSACC);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);
            textScoreaccCSS.text = www.downloadHandler.text;
            textScoreaccCSS.color = Color.black;
        }
        else
        {
            Debug.LogError("Failed to fetch CSS ACC score: " + www.error);
        }
    }

    //////////////////////////////////////////MINIGAME HTML CSS////////////////////////////////////////////////////
    IEnumerator ScoreMiniGameHTMLCSSSpeed()
    {
        UnityWebRequest www = UnityWebRequest.Get(connection.scoreMiniGameHTMLCSSSpeed);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);
            textScoreSpeedHTMLCSS.text = www.downloadHandler.text;
            textScoreSpeedHTMLCSS.color = Color.black;
        }
        else
        {
            Debug.LogError("Failed to fetch HTML CSS speed score: " + www.error);
        }
    }

    IEnumerator ScoreMiniGameHTMLCSSACC()
    {
        UnityWebRequest www = UnityWebRequest.Get(connection.scoreMiniGameHTMLCSSACC);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);
            textScoreaccHTMLCSS.text = www.downloadHandler.text;
            textScoreaccHTMLCSS.color = Color.black;
        }
        else
        {
            Debug.LogError("Failed to fetch HTML CSS ACC score: " + www.error);
        }
    }
    //// saveResult all
    ///
    IEnumerator ResultAll()
    {
        UnityWebRequest www = UnityWebRequest.Get(connection.scoreall);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string resultcallbacks = www.downloadHandler.text;
            Debug.Log(resultcallbacks);
        }
        else
        {
            Debug.LogError("Failed to fetch username: " + www.error);
        }
    }

    IEnumerator scoreResult()
    {
        UnityWebRequest www = UnityWebRequest.Get(connection.scoreResult);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string resultcallback = www.downloadHandler.text;
            textScoreResult.text = resultcallback;
            imgRank(resultcallback);
            Debug.Log(resultcallback);
        }
        else
        {
            Debug.LogError("Failed to fetch username: " + www.error);
        }
    }

    public void imgRank(string rank) {
        if (rank == "Excellent")
        {
            imgExcellent.SetActive(true);
            imgGood.SetActive(false);
            imgAverage.SetActive(false);
            imgFair.SetActive(false);
            imgPoor.SetActive(false);
        }
        else if(rank == "Good")
        {
            imgExcellent.SetActive(false);
            imgGood.SetActive(true);
            imgAverage.SetActive(false);
            imgFair.SetActive(false);
            imgPoor.SetActive(false);
        }
        else if (rank == "Average")
        {
            imgExcellent.SetActive(false);
            imgGood.SetActive(false);
            imgAverage.SetActive(true);
            imgFair.SetActive(false);
            imgPoor.SetActive(false);
        }
        else if (rank == "Fair")
        {
            imgExcellent.SetActive(false);
            imgGood.SetActive(false);
            imgAverage.SetActive(false);
            imgFair.SetActive(true);
            imgPoor.SetActive(false);
        }
        else
        {
            imgExcellent.SetActive(false);
            imgGood.SetActive(false);
            imgAverage.SetActive(false);
            imgFair.SetActive(false);
            imgPoor.SetActive(true);
        }
    }
}
