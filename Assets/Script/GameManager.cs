using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string currentSceneName;
    public string soundUse;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // กำหนดค่าเริ่มต้นของ Scene
        currentSceneName = SceneManager.GetActiveScene().name;
        UpdateSoundEffectForScene(currentSceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // ลบ event listener เมื่อ object ถูกทำลาย
    }

    private void UpdateSoundEffectForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Login":
                soundUse = "Sfx_click";
                break;
            case "mainmenu":
                soundUse = "Sfx_test";
                break;
            default:
                soundUse = "Sfx_default"; // เสียงเริ่มต้นถ้า Scene ไม่ได้กำหนด
                Debug.LogWarning($"No specific sound for scene: {sceneName}");
                break;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name; // อัปเดตชื่อ Scene ปัจจุบัน
        UpdateSoundEffectForScene(currentSceneName);

        // เล่น sound effect สำหรับการเปลี่ยน Scene
        if (!string.IsNullOrEmpty(soundUse))
        {
            AudioSystem.Instance.PlaySFX(soundUse);
        }
    }

    public void Update()
    {
        // ตัวอย่างการเล่นเสียง effect เมื่อคลิกเมาส์
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!string.IsNullOrEmpty(soundUse))
            {
                AudioSystem.Instance.PlaySFX(soundUse);
            }
        }
    }
}
