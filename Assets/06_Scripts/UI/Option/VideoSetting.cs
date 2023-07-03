using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSetting : MonoBehaviour
{
    [Header("UI"), SerializeField]
    private ToggleGroup screen_Set;
    [SerializeField]
    private ToggleGroup graphic;
    [SerializeField]
    private TMP_Dropdown resolution;

    private Resolution[] resolutions;
    private bool _isFull;
    private bool _isHigh;
    private bool _isMiddle;
    private bool _isLow;
    private int _resolutionValue;

    private void Start()
    {
        resolutions = Screen.resolutions;

        foreach (Resolution res in resolutions)
        {
            string resolText = res.ToString().Replace("@ 60Hz", "");
            resolution.options.Add(new TMP_Dropdown.OptionData(resolText));
        }

        if (!Save_Load.Instance._optionData.VideoSet_HasData)
        {
            _resolutionValue = resolution.value;
            graphic.GetFirstActiveToggle().isOn = true;
            Full_Screen();
            Screen.SetResolution(resolutions[_resolutionValue].width,
                             resolutions[_resolutionValue].height, _isFull);
        }

        VideoSetting_Load();
    }
    public void Full_Screen()
    {
        _isFull = true;

        _resolutionValue = resolution.value;

        Screen.SetResolution(resolutions[_resolutionValue].width,
                             resolutions[_resolutionValue].height, _isFull);
    }

    public void Window_Screen()
    {
        _isFull = false;
        _resolutionValue = resolution.value;

        Screen.SetResolution(resolutions[_resolutionValue].width,
                             resolutions[_resolutionValue].height, _isFull);
    }

    public void Set_Resolution()
    {
        _resolutionValue = resolution.value;
        Screen.SetResolution(resolutions[_resolutionValue].width,
                             resolutions[_resolutionValue].height, _isFull);        
    }

    public void SetGraphic_High()
    {
        _isHigh = true;
        _isMiddle = false;
        _isLow = false;
        QualitySettings.masterTextureLimit = 0;
        QualitySettings.shadowResolution = (ShadowResolution)3;
        QualitySettings.vSyncCount = 1;
        QualitySettings.antiAliasing = 8;
        QualitySettings.anisotropicFiltering = (AnisotropicFiltering)2;
        Application.targetFrameRate = 60;                
    }

    public void SetGraphic_Middle()
    {
        _isHigh = false;
        _isMiddle = true;
        _isLow = false;
        QualitySettings.masterTextureLimit = 1;
        QualitySettings.shadowResolution = (ShadowResolution)2;
        QualitySettings.vSyncCount = 1;
        QualitySettings.antiAliasing = 4;
        QualitySettings.anisotropicFiltering = (AnisotropicFiltering)2;

        Application.targetFrameRate = 60;        
    }


    public void SetGraphic_Low()
    {
        QualitySettings.masterTextureLimit = 2;
        QualitySettings.shadowResolution = (ShadowResolution)1;
        QualitySettings.vSyncCount = 0;
        QualitySettings.antiAliasing = 2;
        QualitySettings.anisotropicFiltering = 0;
        Application.targetFrameRate = 30;
        _isHigh = false;
        _isMiddle = false;
        _isLow = true;        
    }

    public void VideoSetting_Save()
    {
        Save_Load.Instance._optionData.Full = screen_Set.GetComponentsInChildren<Toggle>()[0].isOn;
        Save_Load.Instance._optionData.Resolution_Value = _resolutionValue;        
        Save_Load.Instance._optionData.High = _isHigh;
        Save_Load.Instance._optionData.Middle = _isMiddle;
        Save_Load.Instance._optionData.Low = _isLow;
        Save_Load.Instance._optionData.VideoSet_HasData = true;
    }

    public void VideoSetting_Load()
    {
        _isFull = Save_Load.Instance._optionData.Full;
        _resolutionValue = Save_Load.Instance._optionData.Resolution_Value;
        resolution.value = _resolutionValue;        

        Set_Resolution();

        Debug.Log(_isFull);
        if (_isFull)
        {
            screen_Set.GetComponentsInChildren<Toggle>()[0].isOn = _isFull;
            Full_Screen();
        }
        else if (!_isFull)
        {
            screen_Set.GetComponentsInChildren<Toggle>()[1].isOn = true;
            Window_Screen();
        }

        if (Save_Load.Instance._optionData.High)
        {
            graphic.GetComponentsInChildren<Toggle>()[0].isOn = true;
            SetGraphic_High();
        }
        else if (Save_Load.Instance._optionData.Middle)
        {
            graphic.GetComponentsInChildren<Toggle>()[1].isOn = true;
            SetGraphic_Middle();
        }
        else if (Save_Load.Instance._optionData.Low)
        {
            graphic.GetComponentsInChildren<Toggle>()[2].isOn = true;
            SetGraphic_Low();
        }
    }
}
