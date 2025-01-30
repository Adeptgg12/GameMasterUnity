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

    void Start()
    {
        LoadResolution();
    }

    public void SetFullScreen()
    {
        if (isUpdating) return; // ป้องกันการเรียกซ้ำ

        isUpdating = true; // กำลังอัปเดต
        isFullscreen = toggleFullscreen.isOn; // อัพเดตสถานะ fullscreen ตาม Toggle
        SaveResolution(width, height, isFullscreen, resolutionDropDown.value); // บันทึกการตั้งค่า
        isUpdating = false; // เสร็จสิ้นการอัปเดต
    }

    public void SetResolution()
    {
        switch (resolutionDropDown.value)
        {
            case 0: // Option 0
                width = 1920;
                height = 1080;
                break;
            case 1: // Option 1
                width = 1280;
                height = 720;
                break;
            case 2: // Option 2
                width = 960;
                height = 540;
                break;
            default:
                Debug.LogWarning("Invalid option selected.");
                break;
        }

        SaveResolution(width, height, toggleFullscreen.isOn, resolutionDropDown.value);
    }

    private void SaveResolution(int width, int height, bool fullscreen, int dropDownValue)
    {
        PlayerPrefs.SetInt("DropDownValue", dropDownValue);
        PlayerPrefs.SetInt("ResolutionWidth", width);
        PlayerPrefs.SetInt("ResolutionHeight", height);
        PlayerPrefs.SetInt("IsFullscreen", fullscreen ? 1 : 0);

        ApplyResolution(width, height, fullscreen);
        PlayerPrefs.Save();
    }
    private void ApplyResolution(int width, int height, bool fullscreen)
    {
        Screen.SetResolution(width, height, fullscreen);
    }

    private void LoadResolution()
    {
        width = PlayerPrefs.GetInt("ResolutionWidth", 1920); // Default 1920x1080
        height = PlayerPrefs.GetInt("ResolutionHeight", 1080);
        isFullscreen = PlayerPrefs.GetInt("IsFullscreen") != 0; 
        resolutionDropDown.value = PlayerPrefs.GetInt("DropDownValue", 0);
        

        isUpdating = true; // ป้องกันการกระตุ้นซ้ำ
        toggleFullscreen.isOn = isFullscreen; // ตั้งค่า Toggle
        isUpdating = false; // เสร็จสิ้นการตั้งค่า
        ApplyResolution(width, height, isFullscreen); // ใช้การตั้งค่าที่โหลดมา
    }
}
