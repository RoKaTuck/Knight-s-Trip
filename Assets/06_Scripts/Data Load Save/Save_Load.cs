using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct SaveData
{
    public Vector3 _playerPos;
}

public class Save_Load : MonoBehaviour
{
    private SaveData _saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "SaveFile.txt";

    private PlayerMoveCtrl _moveCtrl;

    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if(Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveData()
    { 
        _moveCtrl =FindObjectOfType<PlayerMoveCtrl>();

        _saveData._playerPos = _moveCtrl.transform.position;

        string json = JsonUtility.ToJson(_saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData()
    {

    }
}
