using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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

    public void activeEdit()
    {
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
        UnityWebRequest www = UnityWebRequest.Get(connection.seeUsername);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            username = www.downloadHandler.text;
            nameField.text = username;
            Debug.Log(username);
        }
        else
        {
            Debug.LogError("Failed to fetch username: " + www.error);
        }
    }

    ////////////////////////////////////// Password ///////////////////////////////////////
    public void Callpassword()
    {
        connection = new Connection();
        StartCoroutine(seePassword());
    }

    IEnumerator seePassword()
    {
        UnityWebRequest www = UnityWebRequest.Get(connection.seePassword);
        www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            password = www.downloadHandler.text;
            passField.text = password;
            Debug.Log(passField.text);
        }
        else
        {
            Debug.LogError("Failed to fetch password: " + www.error);
        }
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
        int lengthpassword = password.Length;

        if (lengthpassword >= 8)
        {
            WWWForm form = new WWWForm();
            form.AddField("Name", username);
            form.AddField("Password", password);

            UnityWebRequest www = UnityWebRequest.Post(connection.changePass, form);
            www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                alert = www.downloadHandler.text;
                textSaveWarning.text = alert;
                textSaveWarning.color = Color.green;
                passField.interactable = false;
                SaveTXT.SetActive(true);
            }
            else
            {
                Debug.LogError("Failed to change password: " + www.error);
                alert = "Error: " + www.error;
                textSaveWarning.text = alert;
                textSaveWarning.color = Color.red;
            }
        }
        else
        {
            alert = "Password must be at least 8 characters";
            textSaveWarning.text = alert;
            textSaveWarning.color = Color.red;
            passField.interactable = false;
            SaveTXT.SetActive(true);
        }
    }

    ////////////////////////////////////////////// alertsave /////////////////////////////////////////////////////
    public void disActiveSave()
    {
        SaveTXT.SetActive(false);
    }

    ///////////////////////////////////////////// confirm logout /////////////////////////////////////////////////
    public void Conlogout()
    {
        Conlog.SetActive(true);
    }

    public void CancelLog()
    {
        Conlog.SetActive(false);
    }

    public void ConfirmLog()
    {
        Conlog.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
