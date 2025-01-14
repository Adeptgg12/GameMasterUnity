
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    public LoginWarnning loginWarnning;
    public Button submitButton;
    public Button registerButton;
    public GameObject btn_Exit;
    [SerializeField] public TMP_InputField nameField = default;
    [SerializeField] public TMP_InputField passwordField = default;

    private Connection connection;

    public void Start()
    {
        submitButton.interactable = true;
        registerButton.interactable = true;
        nameField.interactable = true;
        passwordField.interactable = true;
        //AudioSystem.Instance.PlayMusic("MooDeng");
    }
    public void CallLogin()
    {
        connection = new Connection();
        StartCoroutine(Login());
        AudioSystem.Instance.PlaySFX("Sfx_click");
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", nameField.text);
        form.AddField("Password", passwordField.text);
        WWW www = new WWW(connection.loginLink, form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("User login successful");
            StartCoroutine(loginWarnning.loginSuc("User login successful"));
            submitButton.interactable = false;
            registerButton.interactable = false;
            nameField.interactable = false;
            passwordField.interactable = false;
        }
        else
        {
            Debug.Log("User login fail" + www.text);
            StartCoroutine(loginWarnning.loginFai("User login fail" + www.text));
        }
    }

    public void loadSceneRegister() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ExitGame() 
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }

}
