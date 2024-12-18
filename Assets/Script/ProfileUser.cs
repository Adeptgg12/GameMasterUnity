using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUser : MonoBehaviour
{
    public GameObject SaveTXT;
    public GameObject Conlog;
    public TextMeshProUGUI textSaveWarning;
    public Button editPass;
    public Button CancelBtn;
    public Button ConfirmBtn;
    [SerializeField] public TMP_InputField nameField = default;
    [SerializeField] public TMP_InputField passField = default;
    string username;
    string password;
    string alert;
    private Connection connection;
    public void Start()
    {
        SaveTXT.SetActive(false);
        Conlog.SetActive(false);
        nameField.interactable = false;
        passField.interactable = false;
    }
    public void activeEdit() {
        passField.interactable = true;
    }


    ///////////////////////////// username /////////////////////////////////////
    public void Callusername()
    {
        connection = new Connection();
        StartCoroutine(seeUsername());
    }

    IEnumerator seeUsername()
    {
        WWW www = new WWW(connection.seeUsername);
        yield return www;
        username = www.text;
        nameField.text = username;
        Debug.Log(username);
    }
    ////////////////////////////////////// Password ///////////////////////////////////////
    public void Callpassword()
    {
        connection = new Connection();
        StartCoroutine(seePassword());
    }

    IEnumerator seePassword()
    {
        WWW www = new WWW(connection.seePassword);
        yield return www;
        password = www.text;
        passField.text = password;
        Debug.Log(passField.text);
    }

    ////////////////////////////////////// Change Password ///////////////////////////////////////
    public void CallChangePass()
    {
        connection = new Connection();
        StartCoroutine(ChangePass());
    }

    IEnumerator ChangePass()
    {

        password = passField.text;
        int lenghtpassword = password.Length;
        if (lenghtpassword >= 8)
        {
            WWWForm form = new WWWForm();
            form.AddField("Name", username);
            form.AddField("Password", password);
            Debug.Log(username);
            Debug.Log(password);
            WWW www = new WWW(connection.changePass, form);
            yield return www;
            Debug.Log(www.text);
            alert = www.text;
            textSaveWarning.text = alert;
            textSaveWarning.color = Color.green;
            passField.interactable = false;
            SaveTXT.SetActive(true);
        }
        else {
            alert = "Password more than 8 word";
            textSaveWarning.text = alert;
            textSaveWarning.color = Color.red;
            passField.interactable = false;
            SaveTXT.SetActive(true);
        }
        
    }
    ////////////////////////////////////////////// alertsave /////////////////////////////////////////////////////

    public void disActiveSave() {
        SaveTXT.SetActive(false);
    }

    ///////////////////////////////////////////// confirm logout /////////////////////////////////////////////////

    public void Conlogout() {
        Conlog.SetActive(true);
    }

    public void CancelLog() {
        Conlog.SetActive(false);
    }
    public void ConfirmLog()
    {
        Conlog.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }


}
