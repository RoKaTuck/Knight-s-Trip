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

        Debug.Log("�ʱ�ȭ �Ϸ�");
    }

    public void SaveData()
    {
        SavePlayerData();
        SavePlayerStatData();
        SaveInventoryData();
        SaveQuestData();
        SaveWeaponPanelData();
        SaveTownQuestData();

        Debug.Log("���� �Ϸ�");
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA))
        {
            LoadPlayerData();
            LoadPlayerStatData();
            LoadInventoryData();

            Debug.Log("�÷��̾� ������ �ε� �Ϸ�");
        }
        else
            Debug.Log("�÷��̾� ������ �����ϴ�.");

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME))
        {            
            LoadQuestData();

            Debug.Log("����Ʈ ������ �ε� �Ϸ�");
        }
        else
            Debug.Log("����Ʈ ������ �����ϴ�.");

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME))
        {            
            LoadEquipData();

            Debug.Log("���â ������ �ε� �Ϸ�");
        }
        else
            Debug.Log("���â ������ �����ϴ�.");

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_TOWNQUESTNAME))
        {
            LoadTownQuestData();

            Debug.Log("���� ����Ʈ NPC ������ �ε� �Ϸ�");
        }
        else
            Debug.Log("���� ����Ʈ NPC ������ ������ �����ϴ�.");
    }
   
}
