using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SettingsScript : MonoBehaviour
{
    public GameObject panel;
    public AudioMixer mixer;

    public Slider backgroundVolumeSlider;
    public Slider sfxVolumeSlider;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public Toggle fullscreenToggle;
    bool isFullscreen = true;

    public TMP_Dropdown qualityDropdown;

    void Start()
    {
        SetupResolutions();
        SetupQuality();
    }

    void SetupResolutions()
    {
        List<Resolution> unique = new List<Resolution>();
        foreach (Resolution r in Screen.resolutions)
        {
            bool exists = unique.Exists(u => u.width == r.width && u.height == r.height);
            if (!exists) unique.Add(r);
        }
        resolutions = unique.ToArray();

        List<string> options = new List<string>();
        int currentIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentIndex = i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();

        isFullscreen = Screen.fullScreen;

        if (fullscreenToggle != null)
        {
            fullscreenToggle.SetIsOnWithoutNotify(isFullscreen);
        }
    }

    public void SetResolution(int index)
    {
        Resolution r = resolutions[index];
        Screen.SetResolution(r.width, r.height, isFullscreen);

        Debug.Log("Resolution set to: " + r.width + " x " + r.height + " | Fullscreen: " + isFullscreen);
    }

    public void SetFullscreen(bool value)
    {
        isFullscreen = value;
        Screen.fullScreen = value;

        Debug.Log("Fullscreen set to: " + value);
    }
    void SetupQuality()
    {
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));

        qualityDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
        qualityDropdown.RefreshShownValue();
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        Debug.Log("Quality set to: " + QualitySettings.names[index]);
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void AdjustBackgroundVolume()
    {
        float dB = Mathf.Log10(backgroundVolumeSlider.value) * 20f;
        mixer.SetFloat("BGMusicVolume", dB);
    }

    public void AdjustSFXVolume()
    {
        float dB = Mathf.Log10(sfxVolumeSlider.value) * 20f;
        mixer.SetFloat("SFXVolume", dB);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }
}
