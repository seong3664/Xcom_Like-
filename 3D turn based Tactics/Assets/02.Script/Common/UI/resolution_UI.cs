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
        // ȭ�� ��� �ɼ� �߰�
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

        // �ػ� �ɼ� �߰�
        List<string> resolutionOptions = GetResolutionOptions();
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = optimalResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // �ػ� ���� �� �ڵ����� ȭ�� ��� ������Ʈ
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // ������ ���� ������ �ػ󵵷� ���۵ǵ��� ����
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

        // ���� ȭ�� ��带 �ٽ� ������ ���� ���
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
