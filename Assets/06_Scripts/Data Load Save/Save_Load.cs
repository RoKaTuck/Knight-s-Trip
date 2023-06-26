using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Save_Load : MonoBehaviour
{
    private PlayerData _playerData = new PlayerData();
    private QuestData _questData = new QuestData();
    private EquipData _equipData = new EquipData();
    private TownQuestData _townQuestData = new TownQuestData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "SaveFile.txt";
    private string SAVE_QUESTNAME = "QuestSave.txt";
    private string SAVE_EQUIPNAME = "EquipFile.txt";
    private string SAVE_TOWNQUESTNAME = "TownQuest.txt";

    private PlayerMoveCtrl _moveCtrl;
    private StatusCtrl _statusCtrl;
    private Inventory _inven;
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
        SaveWeaponPanelData();
        SaveTownQuestData();

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
            Debug.Log("플레이어 파일이 없습니다.");

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME))
        {
            string loadJsonQuest = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME);
            _questData = JsonUtility.FromJson<QuestData>(loadJsonQuest);

            LoadQuestData();

            Debug.Log("퀘스트 데이터 로드 완료");
        }
        else
            Debug.Log("퀘스트 파일이 없습니다.");

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME))
        {
            string loadJsonEquip = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME);
            _equipData = JsonUtility.FromJson<EquipData>(loadJsonEquip);

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
                _playerData._invenitemIdx.Add(slots[i]._item._itemID);
                _playerData._invenArrayNumber.Add(i);
                _playerData._invenItemName.Add(slots[i]._item._itemName);
                _playerData._invenItemNumber.Add(slots[i]._itemCount);
            }
        }

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, jsonPlayer); // 경로 + 파일 이름
    }

    public void SaveWeaponPanelData()
    {
        _weaponPanel = FindObjectOfType<WeaponPanel>();
        List<Item> items = _weaponPanel.GetItem();

        _equipData._equipItemIdx.Clear();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                _equipData._equipItemIdx.Add(items[i]._itemID);
            }
        }

        string jsonEquip = JsonUtility.ToJson(_equipData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME, jsonEquip); // 경로 + 파일 이름
    }

    public void SaveQuestData()
    {
        _playerData._questDataList.Clear();

        foreach (Quest quest in QuestManager.Instance._quests)
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

    public void SaveTownQuestData()
    {
        QuestNpc questNpc = FindObjectOfType<QuestNpc>();

        _townQuestData._npcQuestData = questNpc._quest;

        string jsonTownQuest = JsonUtility.ToJson(_townQuestData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_TOWNQUESTNAME, jsonTownQuest);
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

    public void LoadEquipData()
    {
        _weaponPanel = FindObjectOfType<WeaponPanel>();

        for (int i = 0; i < _equipData._equipItemIdx.Count; i++)
        {
            _weaponPanel.LoadToEquip(_equipData._equipItemIdx[i]);
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

    public void LoadTownQuestData()
    {
        string loadJsonTownQuest = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_TOWNQUESTNAME);
        _townQuestData = JsonUtility.FromJson<TownQuestData>(loadJsonTownQuest);

        QuestNpc questNpc = FindObjectOfType<QuestNpc>();

        questNpc._quest = _townQuestData._npcQuestData;
    }
}
