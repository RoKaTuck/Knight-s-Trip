using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public partial class Save_Load : MonoBehaviour
{
    private string SAVE_DATA_DIRECTORY;
    private string INIT_DATA_DIRECTORY;    

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
        InitializePlayer();
        InitializeQuest();
        InitializeEquip();
        InitializeTownQuest();

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
   
}
