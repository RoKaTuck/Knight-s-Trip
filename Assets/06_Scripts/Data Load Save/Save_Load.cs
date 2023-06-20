using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 _playerPos;
    public Vector3 _playerRot;
    public int _playerGold;
    public int _playerHp;
    public int _playerMp;
    //public int _playerSp;

    public List<int> _invenArrayNumber = new List<int>();
    public List<string> _invenItemName = new List<string>();
    public List<int> _invenItemNumber = new List<int>();
}

public class Save_Load : MonoBehaviour
{
    private PlayerData _playerData = new PlayerData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "SaveFile.txt";

    private PlayerMoveCtrl _moveCtrl;
    private StatusCtrl _statusCtrl;
    private Inventory _inven;

    private static Save_Load instance;

    public static Save_Load Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {            
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {            
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if(Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void InitializeData()
    {
        _playerData._playerPos = Vector3.zero;
        _playerData._playerRot = Vector3.zero;
        _playerData._playerGold = 10000;

        _playerData._playerHp = 100;
        _playerData._playerMp = 100;
        //_playerData._playerSp = 1000;

        _playerData._invenArrayNumber.Clear();
        _playerData._invenItemName.Clear();
        _playerData._invenItemNumber.Clear();

        string json = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json); // 경로 + 파일 이름

        Debug.Log("초기화 완료");
        Debug.Log(json);
    }

    public void SaveData()
    {                 
        SavePlayerData();
        SaveInventoryData();
       

        string json = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json); // 경로 + 파일 이름
                

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            _playerData = JsonUtility.FromJson<PlayerData>(loadJson);

            LoadPlayerData();
            LoadInventoryData();                      

            Debug.Log("로드 완료");
        }
        else
            Debug.Log("세이브 파일이 없습니다.");
    }

    private void SavePlayerData()
    {
        _moveCtrl = FindObjectOfType<PlayerMoveCtrl>();
        _statusCtrl = FindObjectOfType<StatusCtrl>();

        _playerData._playerPos = _moveCtrl.gameObject.transform.position;
        _playerData._playerRot = _moveCtrl.gameObject.transform.eulerAngles;

        _playerData._playerHp = _statusCtrl._Hp;
        _playerData._playerMp = _statusCtrl._Mp;
        //_playerData._playerSp = _statusCtrl._CurrentSp;
    }

    private void SaveInventoryData()
    {
        _inven = FindObjectOfType<Inventory>();

        Slot[] slots = _inven.GetSlot();
        _playerData._playerGold = GameManager.Instance._Gold;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i]._item != null)
            {
                _playerData._invenArrayNumber.Add(i);
                _playerData._invenItemName.Add(slots[i]._item._itemName);
                _playerData._invenItemNumber.Add(slots[i]._itemCount);
            }
        }
    }

    private void LoadPlayerData()
    {
        _moveCtrl = FindObjectOfType<PlayerMoveCtrl>();
        _statusCtrl = FindObjectOfType<StatusCtrl>();

        _moveCtrl.gameObject.transform.position = _playerData._playerPos;
        _moveCtrl.gameObject.transform.eulerAngles = _playerData._playerRot;

        _statusCtrl._Hp = _playerData._playerHp;
        _statusCtrl._Mp = _playerData._playerMp;
        //_statusCtrl._CurrentSp = _playerData._playerSp;
    }

    private void LoadInventoryData()
    {
        _inven = FindObjectOfType<Inventory>();

        GameManager.Instance._Gold = _playerData._playerGold;

        for (int i = 0; i < _playerData._invenItemName.Count; i++)
        {
            _inven.LoadToInven(_playerData._invenArrayNumber[i],
                               _playerData._invenItemName[i], _playerData._invenItemNumber[i]);
        }
    }
}
