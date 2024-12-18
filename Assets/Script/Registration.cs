
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Registration : MonoBehaviour
{
    public RegisterWarning registerWarning;
    public Button submitButton;
    public Button backButton;
    [SerializeField] public TMP_InputField nameField = default;
    [SerializeField] public TMP_InputField passwordField = default;
    [SerializeField] public TMP_InputField ConpasswordField = default;
    private Connection connection;
    string password;
    int lenghtpassword;
    public void Start()
    {
        submitButton.interactable = true;
        backButton.interactable = true;
        nameField.interactable = true;
        passwordField.interactable = true;
        ConpasswordField.interactable = true;
    }
    public void CallRegister()
    {
        connection = new Connection();
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        password = passwordField.text;
        lenghtpassword = password.Length;

        if (passwordField.text == ConpasswordField.text)
        {
            if (lenghtpassword >= 8)
            {
                WWWForm form = new WWWForm();
                form.AddField("Name", nameField.text);
                form.AddField("Password", passwordField.text);
                WWW www = new WWW(connection.register, form);
                yield return www;
                if (www.text == "0")
                {
                    Debug.Log("User create successful");
                    StartCoroutine(registerWarning.registerSuc("User create successful"));
                    submitButton.interactable = false;
                    backButton.interactable = false;
                    nameField.interactable = false;
                    passwordField.interactable = false;
                    ConpasswordField.interactable = false;
                }
                else
                {
                    Debug.Log("User create fail" + www.text);
                    StartCoroutine(registerWarning.cooldown("User create fail"));
                }
            }
            else {
                Debug.Log("password more than 8 word");
                StartCoroutine(registerWarning.cooldown("Password more than 8 word"));
            }
           
        }
        else {
            StartCoroutine (registerWarning.cooldown("Password not match"));
            Debug.Log("Password not match");
        }
       
    }
    public void loadScene(int a) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(a);
        Debug.Log("test");
    }

}
