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

                    // ใช้ UnityWebRequest แทน WWW
                    UnityWebRequest www = UnityWebRequest.Post(connection.register, form);

                    // ตั้งค่า User-Agent Header
                    www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        // ดึงข้อความตอบกลับจากเซิร์ฟเวอร์และ Trim ช่องว่าง
                        string responseText = www.downloadHandler.text.Trim();

                        Debug.Log("Response from server: " + responseText); // Log เพื่อตรวจสอบค่า

                        if (responseText == "a")
                        {
                            Debug.Log("User create successful");
                            StartCoroutine(registerWarning.registerSuc("User create successful"));
                            submitButton.interactable = false;
                            backButton.interactable = false;
                            nameField.interactable = false;
                            passwordField.interactable = false;
                            ConpasswordField.interactable = false;
                        }
                        else if (responseText == "Name already exit")
                        {
                            Debug.Log("Username already exists");
                            StartCoroutine(registerWarning.cooldown("UserName Already exist!!"));
                        }
                        else
                        {
                            Debug.LogError("User creation failed, unexpected response: " + responseText);
                            StartCoroutine(registerWarning.cooldown("User create fail!!"));
                        }
                    }
                    else
                    {
                        Debug.LogError("Request failed: " + www.error);
                    }
                }
                else
                {
                    Debug.Log("Password must be at least 8 characters");
                    StartCoroutine(registerWarning.cooldown("Password must be at least 8 characters"));
                }
            }
            else
            {
                Debug.Log("Passwords do not match");
                StartCoroutine(registerWarning.cooldown("Passwords do not match"));
            }
        }
        else
        {
            Debug.Log("Username must be at least 4 characters");
            StartCoroutine(registerWarning.cooldown("Username must be at least 4 characters"));
        }
    }


    public void loadScene(int a)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(a);
        Debug.Log("test");
    }
}
