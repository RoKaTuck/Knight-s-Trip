using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioSetting : MonoBehaviour
{
    [Header("UI"), SerializeField]
    private Slider total_Volume;
    [SerializeField]
    private Slider bgm;
    [SerializeField]
    private Slider sound_Effect;

    private AudioSource[] _seArray;

    private void Start()
    {
        AudioSetting_Load();
    }

    public void SetTotal_Volume()
    {
        if (total_Volume.value > bgm.value)
            SoundManager.instance.GetCurBgm().volume = total_Volume.value;            
        else if (total_Volume.value < bgm.value)
            SoundManager.instance.GetCurBgm().volume = bgm.value;
        else if (total_Volume.value == bgm.value)
            SoundManager.instance.GetCurBgm().volume = total_Volume.value;

        if (total_Volume.value == 0)
        {
            SoundManager.instance.GetCurBgm().volume = 0;

            _seArray = SoundManager.instance.GetCurSE();

            for(int i = 0; i < _seArray.Length; i++)
                _seArray[i].volume = 0;            
        }

        if (bgm.value == 0)
            SoundManager.instance.GetCurBgm().volume = 0;

        if (sound_Effect.value == 0)
        {
            _seArray = SoundManager.instance.GetCurSE();

            for (int i = 0; i < _seArray.Length; i++)
                _seArray[i].volume = 0;
        }

        if (_seArray != null)
        {
            for (int i = 0; i < _seArray.Length; i++)
            {
                if (total_Volume.value > sound_Effect.value)
                    _seArray[i].volume = total_Volume.value;
                else if (total_Volume.value < sound_Effect.value)
                    _seArray[i].volume = sound_Effect.value;
                else if (total_Volume.value == sound_Effect.value)
                    _seArray[i].volume = total_Volume.value;
            }
        }
    }

    public void AudioSetting_Save()
    {
        Save_Load.Instance._optionData.Bgm = bgm.value;
        Save_Load.Instance._optionData.SE = sound_Effect.value;
        Save_Load.Instance._optionData.Total_Volume = total_Volume.value;
        Save_Load.Instance._optionData.AudioSet_HasData = true;
    }

    public void AudioSetting_Load()
    {
        bgm.value = Save_Load.Instance._optionData.Bgm;
        sound_Effect.value = Save_Load.Instance._optionData.SE;
        total_Volume.value = Save_Load.Instance._optionData.Total_Volume;
    }
}
