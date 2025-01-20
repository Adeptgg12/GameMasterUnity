using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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
    }

    public void CallLogin()
    {
        connection = new Connection();
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", nameField.text);
        form.AddField("Password", passwordField.text);

        UnityWebRequest request = UnityWebRequest.Post(connection.loginLink, form);

        // เพิ่ม HTTP headers เช่น Authorization หรือ Content-Type
        request.SetRequestHeader("User-Agent",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");


        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success && request.downloadHandler.text == "0")
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
            Debug.Log("User login fail: " + request.downloadHandler.text);
            StartCoroutine(loginWarnning.loginFai("User login fail: " + request.downloadHandler.text));
        }
    }

    public void loadSceneRegister()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
