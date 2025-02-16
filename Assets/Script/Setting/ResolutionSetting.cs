using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResolutionSetting : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropDown;
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
        //LoadResolution();
    }

    void Update()
    {
        if (isFullscreen == true) {
            resolutionDropDown.interactable = true;
        }
        else
        {
            resolutionDropDown.interactable = false;
            resolutionDropDown.value = 1;
        }
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

        SaveResolution(width, height, isFullscreen, resolutionDropDown.value);
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
        isFullscreen = PlayerPrefs.GetInt("IsFullscreen", 1) == 1;
        toggleFullscreen.isOn = isFullscreen;
        if (isFullscreen == true) {
            width = PlayerPrefs.GetInt("ResolutionWidth", 1920);
            height = PlayerPrefs.GetInt("ResolutionHeight", 1080);
            
            resolutionDropDown.value = PlayerPrefs.GetInt("DropDownValue", 0);

            Debug.Log($"Loaded Resolution: {width}x{height}, Fullscreen: {isFullscreen}");

            isUpdating = true;
            isUpdating = false;

            ApplyResolution(width, height, isFullscreen);
        }
        else
        {
            PlayerPrefs.SetInt("DropDownValue", 1);
            PlayerPrefs.SetInt("ResolutionWidth", 1280);
            PlayerPrefs.SetInt("ResolutionHeight", 720);
            PlayerPrefs.Save();
            Debug.Log($"Loaded Resolution: 1280x720, Fullscreen: {isFullscreen}");
            ApplyResolution(1280, 720, isFullscreen);
        }
    }
}
