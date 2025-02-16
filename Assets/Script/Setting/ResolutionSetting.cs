using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResolutionSetting : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropDown;
    [SerializeField] TMP_Text text;
    [SerializeField] Toggle toggleFullscreen;
    private bool isUpdating = false;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] bool isFullscreen;
    private void Awake()
    {
        LoadResolution();
    }
    void Start()
    {
        LoadResolution();
    }

    void OnEnable()
    {
        //LoadResolution(); // โหลดค่าทุกครั้งที่กลับมา Scene นี้
    }

    public void SetFullScreen()
    {
        if (isUpdating) return; // ป้องกันการเรียกซ้ำ

        isUpdating = true;
        isFullscreen = toggleFullscreen.isOn;

        Debug.Log("SetFullScreen: " + isFullscreen);

        SaveResolution(width, height, isFullscreen, resolutionDropDown.value);
        isUpdating = false;
    }

    public void SetResolution()
    {
        switch (resolutionDropDown.value)
        {
            case 0: width = 1920; height = 1080; break;
            case 1: width = 1280; height = 720; break;
            case 2: width = 960; height = 540; break;
            default:
                Debug.LogWarning("Invalid option selected.");
                return;
        }

        SaveResolution(width, height, toggleFullscreen.isOn, resolutionDropDown.value);
    }

    private void SaveResolution(int width, int height, bool fullscreen, int dropDownValue)
    {
        Debug.Log($"Saving Resolution: {width}x{height}, Fullscreen: {fullscreen}, DropDown: {dropDownValue}");

        PlayerPrefs.SetInt("DropDownValue", dropDownValue);
        PlayerPrefs.SetInt("ResolutionWidth", width);
        PlayerPrefs.SetInt("ResolutionHeight", height);
        PlayerPrefs.SetInt("IsFullscreen", fullscreen ? 1 : 0);
        PlayerPrefs.Save();

        ApplyResolution(width, height, fullscreen);
    }

    private void ApplyResolution(int width, int height, bool fullscreen)
    {
        Debug.Log($"Applying Resolution: {width}x{height}, Fullscreen: {fullscreen}");
        text.text = $"Applying Resolution: {width}x{height}, Fullscreen: {fullscreen}";

        if (fullscreen)
        {
            Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
        }
        else
        {
            Screen.SetResolution(width, height, FullScreenMode.Windowed);
        }
    }

    private void LoadResolution()
    {
        width = PlayerPrefs.GetInt("ResolutionWidth", 1920);
        height = PlayerPrefs.GetInt("ResolutionHeight", 1080);
        isFullscreen = PlayerPrefs.GetInt("IsFullscreen", 1) == 1;
        resolutionDropDown.value = PlayerPrefs.GetInt("DropDownValue", 0);

        Debug.Log($"Loaded Resolution: {width}x{height}, Fullscreen: {isFullscreen}");

        isUpdating = true;
        toggleFullscreen.isOn = isFullscreen;
        isUpdating = false;

        ApplyResolution(width, height, isFullscreen);
    }
}
