using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropDown;
    Resolution[] resolutions;
    public TMP_Dropdown QualityDropDown;
    public int Quality;
    public Slider sliderVolume;
    public float sliderVolumeValue;
    // Start is called before the first frame update
    void Start()
    {
        Quality = PlayerPrefs.GetInt("QualityNumber", 3);
        QualityDropDown.value = Quality;

        CheckResolution();
        CheckQuality();

        sliderVolume.value = PlayerPrefs.GetFloat("VolumeAudio", 0.5f);
        AudioListener.volume = sliderVolume.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckQuality() 
    {
        QualitySettings.SetQualityLevel(QualityDropDown.value);
        PlayerPrefs.SetInt("QualityNumber", QualityDropDown.value);
        Quality = QualityDropDown.value;
    }

    public void CheckResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        int ActualRes = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                ActualRes = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = ActualRes;
        resolutionDropDown.RefreshShownValue();
        resolutionDropDown.value = PlayerPrefs.GetInt("ResolutionNumber", 0);
    }

    public void ChangeRes(int _ResIndex) 
    {
        PlayerPrefs.SetInt("ResolutionNumber", resolutionDropDown.value);
        Resolution res = resolutions[_ResIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void ChangeVolumeSlider(float _value)
    { 
        sliderVolumeValue= _value;
        PlayerPrefs.SetFloat("VolumeAudio", sliderVolumeValue);
        AudioListener.volume = sliderVolumeValue;
    }

}
