using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Dialogue2 : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    //pause 
    public GameObject menuPause;
    private bool isMenuPauseActive = false;
    public GameObject ex01;
    public GameObject ex02;
    public GameObject BG1;
    public GameObject BG2;
    public GameObject Give;
    public GameObject incorrect_txt;
    public GameObject incorrect_txt2;
    public TMP_InputField EX01InputField;
    public TMP_InputField EX02InputField;
    public AddKeyUser addkey;
    public string[] lines;
    public float textSpeed;
    private int index;

    private int linesLength;
    // Start is called before the first frame update
    void Start()
    {
        BG1.SetActive(true);
        Give.SetActive(false);
        incorrect_txt.SetActive(false);
        incorrect_txt2.SetActive(false);
        BG2.SetActive(false);
        ex01.SetActive(false);
        ex02.SetActive(false);
        menuPause.SetActive(false);
        linesLength = lines.Length;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {

        if (index == 17)
        {
            ex01.SetActive(true);
        }
        if (index == 29)
        {
            ex02.SetActive(true);
        }
        //puase esc menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuPauseActive)
            {
                ResumeGame(); // กด Esc อีกครั้งเพื่อ Resume
            }
            else
            {
                PauseGame(); // กด Esc เพื่อ Pause
            }
        }

        if (ex01.activeSelf != true && Give.activeSelf != true && ex02.activeSelf != true)
        {
            if (!isMenuPauseActive && Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                    if (index >= (linesLength - 1))
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
                        Debug.Log("ออกเกมจร้า");

                    }
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            BG2.SetActive(true);
            BG1.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Checkex01() {
        string answeruser = EX01InputField.text;
        string answeruserlower = answeruser.ToLower();
        string answer = "font-size";

        if (answeruserlower == answer) 
        {
            ex01.SetActive(false);
            addkey.CallStorykey(3);
            index += 1;
        }
        else
        {
            incorrect_txt.SetActive(true);
        }
    }
    public void Checkex02()
    {
        string answeruser = EX02InputField.text;
        string answeruserlower = answeruser.ToLower();
        string answer = "background-color";

        if (answeruserlower == answer)
        {
            ex02.SetActive(false);
            addkey.CallStorykey(4);
            index += 1;
        }
        else
        {
            incorrect_txt2.SetActive(true);
        }
    }

    public void addKeyUser() {
        
        Give.SetActive(false);
    }


    #region pauseMenu
    public void ResumeGame()
    {
        menuPause.SetActive(false); // ซ่อนเมนู Pause
        Time.timeScale = 1f; // กลับมาเล่นเกมตามปกติ
        isMenuPauseActive = false; // เปลี่ยนสถานะเป็นปิดเมนู
    }

    public void PauseGame()
    {
        menuPause.SetActive(true); // แสดงเมนู Pause
        Time.timeScale = 0f; // หยุดการทำงานของเกม
        isMenuPauseActive = true; // เปลี่ยนสถานะเป็นเปิดเมนู
    }
    public void nextSceneMainmenu()
    {
        ResumeGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
    #endregion
}