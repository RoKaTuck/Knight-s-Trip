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

    // ������ �ִ� �����͸� ����� ���� ���� �� ���� �� ����
    public void OnClickStartGameBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_sceneName);
    }

    //  ���� �����Ͱ� �����ϸ� ����
    public void OnClickLoadGameBtn()    
    {

    }

    //  �ɼ� �г� Ȱ��ȭ && �ɼ� ��ҵ�
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

    //  ���� �α׾ƿ� �ϰ� ��������
    public void OnClickExitBtn()    
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
