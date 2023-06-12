using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectPage : MonoBehaviour
{
    [SerializeField, Header("Select Page Attribute")]
    GameObject _selectPanel;
    [SerializeField]
    GameObject _optionPanel;    
    [SerializeField]
    string _sceneName;
    
    void Start()
    {
        _selectPanel.SetActive(true);
        _optionPanel.SetActive(false);
    }

    // 기존에 있던 데이터를 지우고 새로 만들 지 질문 후 실행
    public void OnClickStartGameBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_sceneName);
    }

    //  기존 데이터가 존재하면 실행
    public void OnClickLoadGameBtn()    
    {

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
