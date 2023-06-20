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
    string _sceneName;
    
    void Start()
    {
        _selectPanel.SetActive(true);
        _optionPanel.SetActive(false);
    }

    // ������ �ִ� �����͸� ����� ���� ���� �� ���� �� ����
    public void OnClickStartGameBtn()
    {        
        Save_Load.Instance.InitializeData();
        SceneManager.LoadScene(_sceneName);
    }

    //  ���� �����Ͱ� �����ϸ� ����
    public void OnClickLoadGameBtn()    
    {        
        if (File.Exists(Application.dataPath + "/Saves/" + "SaveFile.txt"))
            SceneManager.LoadScene(_sceneName);
        else
            Debug.Log("�����Ͱ� �������� �ʽ��ϴ�.");
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
