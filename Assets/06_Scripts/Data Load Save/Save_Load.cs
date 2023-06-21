using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    
    // 플레이어 인벤토리
    public List<int> _invenArrayNumber = new List<int>();
    public List<string> _invenItemName = new List<string>();
    public List<int> _invenItemNumber = new List<int>();

    // 플레이어 퀘스트
    public List<QuestData> _questDataList = new List<QuestData>();
}

[System.Serializable]
public class QuestData
{
    public int _questID; // 퀘스트 아이디
    public string _questName; // 퀘스트 이름    
    public string _questDescription; // 퀘스트 설명
    public int _questReward; // 보상
    public string _questConditionString;
    public int _questConditionCurCount = 0; // 현재 퀘스트 수집 혹은 사냥 수 
    public int _questConditionMax; // 퀘스트 수집 혹은 사냥 최대 수
    public bool _isCompleted; // 퀘스트 완료 여부
}

public class Save_Load : MonoBehaviour
{
    private PlayerData _playerData = new PlayerData();
    private QuestData _questData   = new QuestData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME  = "SaveFile.txt";
    private string SAVE_QUESTNAME = "QuestSave.txt";

    private PlayerMoveCtrl _moveCtrl;
    private StatusCtrl _statusCtrl;
    private Inventory _inven;

    #region Singleton
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
    #endregion
    
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);            
        }
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
        _playerData._questDataList.Clear();

        _questData._isCompleted = false;
        _questData._questConditionCurCount = 0;
        _questData._questConditionMax = 0;
        _questData._questConditionString = string.Empty;
        _questData._questDescription = string.Empty;
        _questData._questID = 0;
        _questData._questName = string.Empty;
        _questData._questReward = 0;

        string json = JsonUtility.ToJson(_playerData, true);
        string jsonQuest = JsonUtility.ToJson(_questData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json); // 경로 + 파일 이름
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME, jsonQuest); // 경로 + 파일 이름

        Debug.Log("초기화 완료");        
    }

    public void SaveData()
    {                 
        SavePlayerData();
        SaveInventoryData();
        SaveQuestData();                

        Debug.Log("저장 완료");        
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJsonPlayer = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            _playerData = JsonUtility.FromJson<PlayerData>(loadJsonPlayer);
            LoadPlayerData();
            LoadInventoryData();
            
            Debug.Log("플레이어 데이터 로드 완료");
        }
        else
            Debug.Log("세이브 파일이 없습니다.");

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME))
        {
            string loadJsonQuest = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME);
            _questData = JsonUtility.FromJson<QuestData>(loadJsonQuest);

            LoadQuestData();

            Debug.Log("퀘스트 데이터 로드 완료");
        }
        else
            Debug.Log("세이브 파일이 없습니다.");
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
                _playerData._invenArrayNumber.Add(i);
                _playerData._invenItemName.Add(slots[i]._item._itemName);
                _playerData._invenItemNumber.Add(slots[i]._itemCount);
            }
        }

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, jsonPlayer); // 경로 + 파일 이름
    }
    public void SaveQuestData()
    {
        _playerData._questDataList.Clear();
        
        foreach(Quest quest in QuestManager.Instance._quests)
        {
            _questData._questID = quest._questID;
            _questData._questName = quest._questName;
            _questData._questDescription = quest._questDescription;
            _questData._questConditionCurCount = quest._questConditionCurCount;
            _questData._questConditionMax = quest._questConditionMax;
            _questData._questConditionString = quest._questConditionString;
            _questData._questReward = quest._questReward;

            _playerData._questDataList.Add(_questData);
        }

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);
        string jsonQuest = JsonUtility.ToJson(_questData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, jsonPlayer);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME, jsonQuest); // 경로 + 파일 이름
    }

    public void LoadPlayerData()
    {
        _moveCtrl = FindObjectOfType<PlayerMoveCtrl>();
        _statusCtrl = FindObjectOfType<StatusCtrl>();

        _moveCtrl.gameObject.transform.position = _playerData._playerPos;
        _moveCtrl.gameObject.transform.eulerAngles = _playerData._playerRot;

        _statusCtrl._Hp = _playerData._playerHp;
        _statusCtrl._Mp = _playerData._playerMp;        
    }

    public void LoadInventoryData()
    {
        _inven = FindObjectOfType<Inventory>();

        GameManager.Instance._Gold = _playerData._playerGold;

        for (int i = 0; i < _playerData._invenItemName.Count; i++)
        {
            _inven.LoadToInven(_playerData._invenArrayNumber[i],
                               _playerData._invenItemName[i], _playerData._invenItemNumber[i]);
        }
    }


    public void LoadQuestData()
    {
        QuestManager.Instance._quests.Clear();

        foreach (QuestData questData in _playerData._questDataList)
        {
            Quest quest = new Quest(questData._questID, questData._questName, questData._questDescription,
                                    questData._questReward, questData._questConditionString, questData._questConditionCurCount,
                                    questData._questConditionMax);      
            quest._IsCompleted = questData._isCompleted;

            QuestManager.Instance._quests.Add(quest);
        }
    }
}
