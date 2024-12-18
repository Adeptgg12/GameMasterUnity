using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterWarning : MonoBehaviour
{
    public TextMeshProUGUI textWarning;
    public float showTime;
    public IEnumerator cooldown(string a)
    {
        textWarning.text = a;
        textWarning.color = Color.red;
        yield return new WaitForSeconds(showTime);
        textWarning.text = "";
    }
    public IEnumerator registerSuc(string a)
    {
        textWarning.text = a;
        textWarning.color = Color.green;
        yield return new WaitForSeconds(showTime);
        textWarning.text = "";
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void Start()
    {
        textWarning.text = "";
    }
}
