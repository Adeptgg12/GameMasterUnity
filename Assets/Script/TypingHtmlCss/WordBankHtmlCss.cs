using System.Collections.Generic;
using UnityEngine;

public class WordBankHtmlCss : MonoBehaviour
{
    private List<string> originalWords = new List<string>() {
    "{color:red;}",
    "{color:blue;}",
    "{color:green;}",
    "{font-size:16px;}",
    "{font-size:20px;}",
    "{font-size:32px;}",
    "{margin:10px;}",
    "{margin:15px;}",
    "{padding:5px;}",
    "{padding:10px;}",
    "{padding:20px;}",
    "{text-align:left;}",
    "{text-align:center;}",
    "{text-align:right;}",
    "{border-radius:5px;}",
    "{border-radius:50%;}",
    "{background-color:#f0f0f0;}",
    "{background-color:#ffffff;}",
    "{width:100%;}",
    "{width:150px;}",
    "{height:auto;}",
    "{display:block;}",
    "{font-weight:bold;}",
    "{font-weight:normal;}",
    "{opacity:1;}",
    "{cursor:pointer;}",
    "{list-style:none;}",
    "{list-style:square;}",
    "{overflow:hidden;}",
    "{overflow:scroll;}",
    "<!DOCTYPE html>",
    "<html>",
    "</html>",
    "<head>",
    "</head>",
    "<title>",
    "</title>",
    "<body>",
    "</body>",
    "<h1>",
    "</h1>",
    "<h2>",
    "</h2>",
    "<h3>",
    "</h3>",
    "<h4>",
    "</h4>",
    "<h5>",
    "</h5>",
    "<h6>",
    "</h6>",
    "<p>",
    "</p>",
    "<b>",
    "</b>",
    "<i>",
    "</i>",
    "<u>",
    "</u>",
    "<s>",
    "</s>",
    "<del>",
    "</del>",
    "<strong>",
    "</strong>",
    "<em>",
    "</em>",
    "<br>",
    "<a>",
    "</a>",
    "<img>",
    "<form>",
    "</form>",
    "<input>",
    "<label>",
    "</label>",
    "<button>",
    "</button>",
    "<style>",
    "</style>",
    "<link>",
    "<meta>",
    "<script>",
    "</script>"
};



    private List<string> workingWords = new List<string>();

    private void Awake()
    {
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConvertToLower(workingWords);
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);
            string temporary = list[i];
            list[i] = list[random];
            list[random] = temporary;
        }
    }

    private void ConvertToLower(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }

    public string GetWord()
    {
        string newWord = string.Empty;
        if (workingWords.Count != 0)
        {
            newWord = workingWords[workingWords.Count - 1];
            workingWords.RemoveAt(workingWords.Count - 1);
        }
        return newWord;
    }
}
