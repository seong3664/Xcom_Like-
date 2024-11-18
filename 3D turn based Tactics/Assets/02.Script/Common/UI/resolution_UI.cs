using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionUI : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    private List<Resolution> resolutions = new List<Resolution>();
    private int optimalResolutionIndex = 0;

    public TMP_Dropdown windowModeDropdown;

    public enum ScreenMode
    {
        FullScreenWindow,
        Window
    }

    private void Start()
    {
        // 화면 모드 옵션 추가
<<<<<<< HEAD
        List<string> windowModeOptions = new List<string> { "Full Screen", "Window" };
=======
        List<string> windowModeOptions = new List<string> {
            "Full Screen",
            "Window"
        };
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
        windowModeDropdown.ClearOptions();
        windowModeDropdown.AddOptions(windowModeOptions);
        windowModeDropdown.RefreshShownValue();
        windowModeDropdown.onValueChanged.AddListener(index => ChangeFullScreenMode((ScreenMode)index));

        // 해상도 옵션 추가
        List<string> resolutionOptions = GetResolutionOptions();
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = optimalResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // 해상도 변경 시 자동으로 화면 모드 업데이트
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // 게임이 가장 적합한 해상도로 시작되도록 설정
        SetResolution(optimalResolutionIndex);
    }

    private List<string> GetResolutionOptions()
    {
        resolutions.Clear();
        resolutions.Add(new Resolution { width = 1280, height = 720 });
        resolutions.Add(new Resolution { width = 1600, height = 900 });
        resolutions.Add(new Resolution { width = 1920, height = 1080 });
        resolutions.Add(new Resolution { width = 2560, height = 1440 });
        resolutions.Add(new Resolution { width = 2560, height = 1600 });

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                optimalResolutionIndex = i;
                option += " *";
            }
            options.Add(option);
        }

        return options;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);

        // 현재 화면 모드를 다시 적용해 맞춤 출력
        ChangeFullScreenMode((ScreenMode)windowModeDropdown.value);
    }

    private void ChangeFullScreenMode(ScreenMode mode)
    {
        switch (mode)
        {
            case ScreenMode.FullScreenWindow:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case ScreenMode.Window:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }
}
