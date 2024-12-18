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
        // ��˹����������鹢ͧ Scene
        currentSceneName = SceneManager.GetActiveScene().name;
        UpdateSoundEffectForScene(currentSceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // ź event listener ����� object �١�����
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
                soundUse = "Sfx_default"; // ���§������鹶�� Scene ������˹�
                Debug.LogWarning($"No specific sound for scene: {sceneName}");
                break;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name; // �ѻവ���� Scene �Ѩ�غѹ
        UpdateSoundEffectForScene(currentSceneName);

        // ��� sound effect ����Ѻ�������¹ Scene
        if (!string.IsNullOrEmpty(soundUse))
        {
            AudioSystem.Instance.PlaySFX(soundUse);
        }
    }

    public void Update()
    {
        // ������ҧ���������§ effect ����ͤ�ԡ�����
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!string.IsNullOrEmpty(soundUse))
            {
                AudioSystem.Instance.PlaySFX(soundUse);
            }
        }
    }
}
