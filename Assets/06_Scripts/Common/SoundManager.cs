using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string _name; // ���� �̸�
    public AudioClip _clip; // ��
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }

    #region SingleTon
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    #endregion SingleTon

    public AudioSource[] _audioSourceEffects;
    public AudioSource _audioSourceBgm;

    public string[] _playSoundName;

    public Sound[] _effectSounds;
    public Sound[] _bgmSounds;
    
    public AudioSource _curBgm;

    private void Start()
    {                 
        _playSoundName = new string[_audioSourceEffects.Length];
    }

    public void PlayeBgm(string name)
    {
        for (int i = 0; i < _bgmSounds.Length; i++)
        {
            if (name == _bgmSounds[i]._name)
            {
                _audioSourceBgm.clip = _bgmSounds[i]._clip;
                _audioSourceBgm.Play();
                _curBgm = _audioSourceBgm;

                Debug.Log("��� ���� AudioSource�� ������Դϴ�.");

                return;
            }
        }

        Debug.Log(name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
    }

    public void PlaySE(string name)
    {
        for (int i = 0; i < _effectSounds.Length; i++)
        {
            if(name == _effectSounds[i]._name)
            {
                for (int j = 0; j < _audioSourceEffects.Length; j++)
                {
                    if (!_audioSourceEffects[j].isPlaying)
                    {
                        _playSoundName[j] = _effectSounds[i]._name;
                        _audioSourceEffects[j].clip = _effectSounds[i]._clip;
                        _audioSourceEffects[j].Play();                        
                        return;
                    }
                }

                Debug.Log("��� ���� AudioSource�� ������Դϴ�.");

                return;
            }
        }

        Debug.Log(name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
    }

    public void StopAllSE()
    {
        for (int i = 0; i < _audioSourceEffects.Length; i++)
        {
            _audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string name)
    {
        for (int i = 0; i < _audioSourceEffects.Length; i++)
        {
            if (_playSoundName[i] == name)
            {
                _audioSourceEffects[i].Stop();

                return;
            }
        }

        Debug.Log("�������" + name + "���尡 �����ϴ�.");
    }

    public AudioSource GetCurBgm()
    {
        return _curBgm;
    }

    public AudioSource[] GetCurSE()
    {
        return _audioSourceEffects;
    }

}
