using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginWarnning : MonoBehaviour
{
    public TextMeshProUGUI textWarning;
    public float showTime;
    public IEnumerator loginFai(string a)
    {
        textWarning.text = a;
        textWarning.color = Color.red;
        yield return new WaitForSeconds(showTime);
        textWarning.text = "";
    }
    public IEnumerator loginSuc(string a)
    {
        textWarning.text = a;
        textWarning.color = Color.green;
        yield return new WaitForSeconds(showTime);
        textWarning.text = "";
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void Start()
    {
        textWarning.text = "";
    }
}
