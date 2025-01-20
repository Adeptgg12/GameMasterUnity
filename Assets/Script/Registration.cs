using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

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
    string username;
    int lenghtusername;

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
        username = nameField.text;
        lenghtusername = username.Length;

        if (lenghtusername >= 4)
        {
            if (passwordField.text == ConpasswordField.text)
            {
                if (lenghtpassword >= 8)
                {
                    WWWForm form = new WWWForm();
                    form.AddField("Name", nameField.text);
                    form.AddField("Password", passwordField.text);

                    // Using UnityWebRequest instead of WWW
                    UnityWebRequest www = UnityWebRequest.Post(connection.register, form);

                    // Set the User-Agent header
                    www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        string responseText = www.downloadHandler.text;
                        Debug.Log(responseText);
                        int result = int.Parse(responseText);
                        if (result == 0)
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
                            Debug.Log("User create fail" + www.downloadHandler.text);
                            if (www.downloadHandler.text == "Name already exit")
                            {
                                StartCoroutine(registerWarning.cooldown("UserName Already exist!!"));
                            }
                            else
                            {
                                Debug.Log("User create fail ===>" + www.downloadHandler.text);
                                StartCoroutine(registerWarning.cooldown("User create fail!!"));
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Request failed: " + www.error);
                    }
                }
                else
                {
                    Debug.Log("password more than 8 word");
                    StartCoroutine(registerWarning.cooldown("Password more than 8 word"));
                }

            }
            else
            {
                StartCoroutine(registerWarning.cooldown("Password not match"));
                Debug.Log("Password not match");
            }

        }
        else
        {
            StartCoroutine(registerWarning.cooldown("Username more than 4 word"));
            Debug.Log("Username not valid");
        }
    }

    public void loadScene(int a)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(a);
        Debug.Log("test");
    }
}
