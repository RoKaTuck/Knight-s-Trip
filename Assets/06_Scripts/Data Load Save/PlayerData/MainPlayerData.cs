using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // 플레이어 위치
    public Vector3 _playerPos;
    public Vector3 _playerRot;

    // 플레이어 스텟
    public int _playerGold;
    public int _playerHp;
    public int _playerMp;
    public int _playerLevel;
    public int _playerExp;

    // 플레이어 인벤토리
    public List<int> _invenitemIdx = new List<int>();
    public List<int> _invenArrayNumber = new List<int>();
    public List<string> _invenItemName = new List<string>();
    public List<int> _invenItemNumber = new List<int>();

    // 플레이어 퀘스트
    public List<QuestData> _questDataList = new List<QuestData>();
}

public class MainPlayerData : MonoBehaviour
{
    private PlayerData _playerData = new PlayerData();

    private PlayerMoveCtrl _moveCtrl;
    private StatusCtrl _statusCtrl;
    private Inventory _inven;

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "SaveFile.txt";

    private void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }
    }

    public void Initialize()
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
        _playerData._questDataList.Clear();

        string json = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json); // 경로 + 파일 이름        
    }
    

    public void SavePlayerData()
    {
        _moveCtrl = FindObjectOfType<PlayerMoveCtrl>();
        _statusCtrl = FindObjectOfType<StatusCtrl>();

        _playerData._playerPos = _moveCtrl.gameObject.transform.position;
        _playerData._playerRot = _moveCtrl.gameObject.transform.eulerAngles;

        _playerData._playerHp = _statusCtrl._Hp;
        _playerData._playerMp = _statusCtrl._Mp;

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, jsonPlayer); // 경로 + 파일 이름

        Debug.Log("플레이어 저장");
    }

    public void SaveInventoryData()
    {
        _inven = FindObjectOfType<Inventory>();

        Slot[] slots = _inven.GetSlot();
        _playerData._playerGold = GameManager.Instance._Gold;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i]._item != null)
            {
                _playerData._invenitemIdx.Add(slots[i]._item._itemID);
                _playerData._invenArrayNumber.Add(i);
                _playerData._invenItemName.Add(slots[i]._item._itemName);
                _playerData._invenItemNumber.Add(slots[i]._itemCount);
            }
        }

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, jsonPlayer); // 경로 + 파일 이름

        Debug.Log("인벤토리 저장");
    }

    public void LoadPlayerData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJsonPlayer = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            _playerData = JsonUtility.FromJson<PlayerData>(loadJsonPlayer);

            _moveCtrl = FindObjectOfType<PlayerMoveCtrl>();
            _statusCtrl = FindObjectOfType<StatusCtrl>();

            _moveCtrl.gameObject.transform.position = _playerData._playerPos;
            _moveCtrl.gameObject.transform.eulerAngles = _playerData._playerRot;

            _statusCtrl._Hp = _playerData._playerHp;
            _statusCtrl._Mp = _playerData._playerMp;                        

            Debug.Log("플레이어 로드");
        }
    }

    public void LoadInventoryData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJsonPlayer = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            _playerData = JsonUtility.FromJson<PlayerData>(loadJsonPlayer);

            _inven = FindObjectOfType<Inventory>();

            GameManager.Instance._Gold = _playerData._playerGold;

            for (int i = 0; i < _playerData._invenItemName.Count; i++)
            {
                _inven.LoadToInven(_playerData._invenArrayNumber[i],
                                   _playerData._invenItemName[i], _playerData._invenItemNumber[i]);
            }

            Debug.Log("인벤토리 로드");
        }        
    }
}
