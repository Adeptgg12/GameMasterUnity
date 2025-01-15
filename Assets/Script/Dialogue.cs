using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject quest1;
    public GameObject quest2;
    public GameObject ch1;
    public GameObject BG1;
    public GameObject BG;
    public GameObject ex1;
    public GameObject ex2;
    public GameObject gift;
    public GameObject incorrect_txt;
    public GameObject incorrect2_txt;
    public Button okex1;
    public Button inquest1;
    public Button inquest2;
    public Button inquest3;

    public Button inquest4;
    public Button inquest5;
    public Button inquest6;

    public AddKeyUser addkey;
    //pause 
    public GameObject menuPause;
    private bool isMenuPauseActive = false;

    public string[] lines;
    public float textSpeed;
    [SerializeField]
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        quest1.SetActive(false); // คำถาม
        quest2.SetActive(false); // คำถาม
        incorrect_txt.SetActive(false);
        incorrect2_txt.SetActive(false);
        ex1.SetActive(false); //ตัวอย่าง 
        ex2.SetActive(false); //ตัวอย่าง 
        BG1.SetActive(false);
        BG.SetActive(true);
        menuPause.SetActive(false);
        gift.SetActive(false);
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.richText = true;
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
        if (index == 5) {
            ex1.SetActive(true); //ตัวอย่าง
        }
        if (index == 7)
        {
            quest1.SetActive(true); //คำถาม
        }
        if (index == 14)
        {
            textComponent.richText = false;
        }
        if (index == 15)
        {
            textComponent.richText = false;
        } 
        if (index == 21)
        {
            quest2.SetActive(true); //คำถาม
        }
        if (index == 40)
        {
            ex2.SetActive(true);
        }
        if (index != 5 && index != 7 && index != 8 && gift.activeSelf != true && index != 21 && ex2.activeSelf != true) {
            if (!isMenuPauseActive && Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                    if (index >= 66)
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
        Debug.Log(index);
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
            BG1.SetActive(true);
            BG.SetActive(false);
        }
        else
        {
            ch1.SetActive(false);

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

    public void okBtnEx1()
    {
        ex1.SetActive(false);
        index = 6;
    }

   #region popup
    public void okBtnEx2()
    {
        ex2.SetActive(false);
        index += 1;
    }
    public void correct()
    {
        index += 2;
        quest1.SetActive(false);
        addkey.CallStorykey(1);
    }
    public void incorrect()
    {
        incorrect_txt.SetActive(true);
    }

    public void correct2()
    {
        index += 1;
        quest2.SetActive(false);
        addkey.CallStorykey(2);
    }

    public void incorrect2()
    {
        incorrect2_txt.SetActive(true);
    }
#endregion
}
