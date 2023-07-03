using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPage : MonoBehaviour
{
    [SerializeField, Header("Select Page Attribute")]
    GameObject _selectPanel;
    [SerializeField]
    GameObject _optionPanel;
    [SerializeField]
    private GameObject _videoBase;
    [SerializeField]
    private GameObject _musicBase;
    [SerializeField]
    string _sceneName;

    // 필요한 컴포넌트
    [SerializeField]
    private VideoSetting _videoSetting;
    [SerializeField]
    private AudioSetting _audioSetting;

    private bool _isVideoActive = false;
    private bool _isMusicActive = false;    

    void Start()
    {
        Save_Load.Instance.LoadOptionData();

        _selectPanel.SetActive(true);
        _optionPanel.SetActive(false);
    }

    // 기존에 있던 데이터를 지우고 새로 만들 지 질문 후 실행
    public void OnClickStartGameBtn()
    {
        //TestSaveLoad.Instance.InitializeData();
        Save_Load.Instance.InitializeData();
        SceneManager.LoadScene(_sceneName);
    }

    //  기존 데이터가 존재하면 실행
    public void OnClickLoadGameBtn()    
    {        
        if (File.Exists(Application.dataPath + "/Saves/" + "PlayerData.txt"))
            SceneManager.LoadScene(_sceneName);
        else
            Debug.Log("데이터가 존재하지 않습니다.");
    }

    //  옵션 패널 활성화 && 옵션 요소들
    public void OnClickOptionsBtn() 
    {
        _selectPanel.SetActive(false);
        _optionPanel.SetActive(true);
    }

    public void OnClickOptionExitBtn()
    {
        _selectPanel.SetActive(true);
        _optionPanel.SetActive(false);
    }

    public void OnClickVideoBtn()
    {
        _isVideoActive = !_isVideoActive;

        if (_isVideoActive)
        {            
            _videoBase.SetActive(true);
        }
        else
        {
            _videoSetting.VideoSetting_Save();
            Save_Load.Instance.SaveOptionData();
            _videoBase.SetActive(false);
        }
    }

    public void OnClickMusicBtn()
    {
        _isMusicActive = !_isMusicActive;

        if (_isMusicActive)
            _musicBase.SetActive(true);
        else
        {
            _audioSetting.AudioSetting_Save();
            Save_Load.Instance.SaveOptionData();
            _musicBase.SetActive(false);
        }
    }    

    //  계정 로그아웃 하고 게임종료
    public void OnClickExitBtn()    
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
