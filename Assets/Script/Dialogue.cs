using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject quest1;
    public GameObject ch1;
    public GameObject BG1;
    public GameObject BG;
    public GameObject ex1;
    public GameObject gift;
    public GameObject storypart2;
    public Button okex1;
    public Button inquest1;
    public Button inquest2;
    public Button inquest3;


    //pause 
    public GameObject menuPause;
    private bool isMenuPauseActive = false;

    public string[] lines;
    public float textSpeed;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        quest1.SetActive(false);
        ex1.SetActive(false);
        BG1.SetActive(false);
        BG.SetActive(true);
        menuPause.SetActive(false);
        gift.SetActive(false);
        storypart2.SetActive(false);
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (index == 15) {
            quest1.SetActive(true);
        }
        if (index == 17) {
            ex1.SetActive(true);
        }
        if (index == 21) {
            gift.SetActive(true);
            index += 1;
        }
        if (index != 13 && index != 15 && index != 21) {
            if (!isMenuPauseActive && Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];



                }
            }
        }
        if (index == 13) {
            ex1.SetActive(true);
        }
        
    }
    //menu setting
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

    void StartDialogue() { 
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() { 
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1) { 
            index++;
            textComponent.text = string.Empty;
            StartCoroutine (TypeLine());
            BG1.SetActive(true);
            BG.SetActive(false);
        }
        else
        {
            ch1.SetActive(false);
            storypart2.SetActive(true);

        }
    }

    public void correct() {
        quest1.SetActive(false);
        index += 5;
    }
    public void incorrect() {
        quest1.SetActive(false);
        index += 1;
    }
    public void okBtnEx1() {
        ex1.SetActive(false);
        index = 14;
    }
}
