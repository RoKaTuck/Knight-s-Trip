using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Save_Load : MonoBehaviour
{
    private QuestData _questData         = new QuestData();
    private EquipData _equipData         = new EquipData();
    private PlayerData _playerData       = new PlayerData();
    private TownQuestData _townQuestData = new TownQuestData();

    private string SAVE_DATA_DIRECTORY;
    private string INIT_DATA_DIRECTORY;

    // SAVE
    private string SAVE_QUESTNAME     = "QuestSave.txt";
    private string SAVE_EQUIPNAME     = "EquipFile.txt";
    private string SAVE_PLAYERDATA    = "PlayerData.txt";
    private string SAVE_TOWNQUESTNAME = "TownQuest.txt";    

    // Init
    private string INIT_QUESTNAME     = "QuestSave_Original.txt";
    private string INIT_EQUIPNAME     = "EquipFile_Original.txt";    
    private string INIT_PLAYERDATA    = "PlayerData_Original.txt";
    private string INIT_TOWNQUESTNAME = "TownQuest_Original.txt";

    private Inventory _inven;
    private PlayerCtrl _playerCtrl;
    private StatusCtrl _statusCtrl;
    private WeaponPanel _weaponPanel;

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
            Destroy(gameObject);
    }
    #endregion

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";
        INIT_DATA_DIRECTORY = Application.dataPath + "/Saves/" + "Initialize/";

        if (Directory.Exists(SAVE_DATA_DIRECTORY) && Directory.Exists(INIT_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
            Directory.CreateDirectory(INIT_DATA_DIRECTORY);
        }
    }

    public void InitializeData()
    {
        // 플레이어 데이터
        string initJsonPlayerData = File.ReadAllText(INIT_DATA_DIRECTORY + INIT_PLAYERDATA);
        _playerData = JsonUtility.FromJson<PlayerData>(initJsonPlayerData);

        // 마을 퀘스트 데이터
        string initJsonTownQuest = File.ReadAllText(INIT_DATA_DIRECTORY + INIT_TOWNQUESTNAME);
        _townQuestData = JsonUtility.FromJson<TownQuestData>(initJsonTownQuest);

        // 장비 데이터
        string initJsonEquipData = File.ReadAllText(INIT_DATA_DIRECTORY + INIT_EQUIPNAME);
        _equipData = JsonUtility.FromJson<EquipData>(initJsonEquipData);

        // 퀘스트 데이터
        string initJsonQuestData = File.ReadAllText(INIT_DATA_DIRECTORY + INIT_QUESTNAME);
        _questData = JsonUtility.FromJson<QuestData>(initJsonQuestData);

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);
        string jsonQuest = JsonUtility.ToJson(_questData, true);
        string jsonEquip = JsonUtility.ToJson(_equipData, true);
        string jsonTown = JsonUtility.ToJson(_townQuestData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA, jsonPlayer); // 경로 + 파일 이름
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME, jsonQuest); // 경로 + 파일 이름
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME, jsonEquip); // 경로 + 파일 이름
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_TOWNQUESTNAME, jsonTown); // 경로 + 파일 이름

        Debug.Log("초기화 완료");
    }

    public void SaveData()
    {
        SavePlayerData();
        SavePlayerStatData();
        SaveInventoryData();
        SaveQuestData();
        SaveWeaponPanelData();
        SaveTownQuestData();

        Debug.Log("저장 완료");
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA))
        {
            LoadPlayerData();
            LoadPlayerStatData();
            LoadInventoryData();

            Debug.Log("플레이어 데이터 로드 완료");
        }
        else
            Debug.Log("플레이어 파일이 없습니다.");

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME))
        {            
            LoadQuestData();

            Debug.Log("퀘스트 데이터 로드 완료");
        }
        else
            Debug.Log("퀘스트 파일이 없습니다.");

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME))
        {            
            LoadEquipData();

            Debug.Log("장비창 데이터 로드 완료");
        }
        else
            Debug.Log("장비창 파일이 없습니다.");

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_TOWNQUESTNAME))
        {
            LoadTownQuestData();

            Debug.Log("마을 퀘스트 NPC 데이터 로드 완료");
        }
        else
            Debug.Log("마을 퀘스트 NPC 데이터 파일이 없습니다.");
    }

    public void SavePlayerData()
    {
        _playerCtrl = FindObjectOfType<PlayerCtrl>();
        _statusCtrl = FindObjectOfType<StatusCtrl>();

        _playerData._playerPos = _playerCtrl.gameObject.transform.position;
        _playerData._playerRot = _playerCtrl.gameObject.transform.eulerAngles;        

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA, jsonPlayer); // 경로 + 파일 이름
    }

    public void SavePlayerStatData()
    {
        _playerData._playerHp = _statusCtrl._Hp;
        _playerData._playerMp = _statusCtrl._Mp;

        _playerData._playerLevel = GameManager.Instance._Level;
        _playerData._playerExp   = GameManager.Instance._Exp;

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA, jsonPlayer); // 경로 + 파일 이름
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

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA, jsonPlayer); // 경로 + 파일 이름
    }

    public void SaveWeaponPanelData()
    {
        _weaponPanel = FindObjectOfType<WeaponPanel>();
        List<Item> items = _weaponPanel.GetItem();

        _equipData._equipItemIdx.Clear();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
                _equipData._equipItemIdx.Add(items[i]._itemID);
        }

        string jsonEquip = JsonUtility.ToJson(_equipData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME, jsonEquip); // 경로 + 파일 이름
    }

    public void SaveQuestData()
    {
        _playerData._questDataList.Clear();

        foreach (Quest quest in QuestManager.Instance._quests)
        {
            _questData._questID                = quest._questID;
            _questData._questName              = quest._questName;
            _questData._questReward            = quest._questReward;
            _questData._questDescription       = quest._questDescription;
            _questData._questConditionMax      = quest._questConditionMax;
            _questData._questConditionString   = quest._questConditionString;
            _questData._questConditionCurCount = quest._questConditionCurCount;

            _playerData._questDataList.Add(_questData);
        }

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);
        string jsonQuest = JsonUtility.ToJson(_questData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA, jsonPlayer);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME, jsonQuest); // 경로 + 파일 이름
    }

    public void SaveTownQuestData()
    {
        QuestNpc questNpc = FindObjectOfType<QuestNpc>();

        _townQuestData._npcQuestData = questNpc._quest;

        string jsonTownQuest = JsonUtility.ToJson(_townQuestData, true);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_TOWNQUESTNAME, jsonTownQuest);
    }

    public void LoadPlayerData()
    {
        string loadJsonPlayer = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA);
        _playerData = JsonUtility.FromJson<PlayerData>(loadJsonPlayer);

        _playerCtrl = FindObjectOfType<PlayerCtrl>();
        _statusCtrl = FindObjectOfType<StatusCtrl>();

        _playerCtrl.gameObject.transform.position    = _playerData._playerPos;
        _playerCtrl.gameObject.transform.eulerAngles = _playerData._playerRot;        
    }

    public void LoadPlayerStatData()
    {
        string loadJsonPlayer = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA);
        _playerData = JsonUtility.FromJson<PlayerData>(loadJsonPlayer);

        GameManager.Instance._Level = _playerData._playerLevel;
        GameManager.Instance._Exp   = _playerData._playerExp;

        _statusCtrl._Hp = _playerData._playerHp;
        _statusCtrl._Mp = _playerData._playerMp;
    }

    public void LoadInventoryData()
    {
        string loadJsonPlayer = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA);
        _playerData = JsonUtility.FromJson<PlayerData>(loadJsonPlayer);

        _inven = FindObjectOfType<Inventory>();

        GameManager.Instance._Gold = _playerData._playerGold;

        for (int i = 0; i < _playerData._invenItemName.Count; i++)
        {
            _inven.LoadToInven(_playerData._invenArrayNumber[i],
                               _playerData._invenItemName[i], _playerData._invenItemNumber[i]);
        }
    }

    public void LoadEquipData()
    {
        string loadJsonEquip = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME);
        _equipData = JsonUtility.FromJson<EquipData>(loadJsonEquip);

        _weaponPanel = FindObjectOfType<WeaponPanel>();

        for (int i = 0; i < _equipData._equipItemIdx.Count; i++)
        {
            _weaponPanel.LoadToEquip(_equipData._equipItemIdx[i]);
        }
    }

    public void LoadQuestData()
    {
        string loadJsonQuest = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME);
        _questData = JsonUtility.FromJson<QuestData>(loadJsonQuest);

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

    public void LoadTownQuestData()
    {
        string loadJsonTownQuest = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_TOWNQUESTNAME);
        _townQuestData = JsonUtility.FromJson<TownQuestData>(loadJsonTownQuest);

        QuestNpc questNpc = FindObjectOfType<QuestNpc>();
        questNpc._quest = _townQuestData._npcQuestData;
    }
    
    public void LoadTownQuestOriginalData()
    {
        string loadJsonTownQuest = File.ReadAllText(INIT_DATA_DIRECTORY + INIT_TOWNQUESTNAME);
        _townQuestData = JsonUtility.FromJson<TownQuestData>(loadJsonTownQuest);        
    }
}
