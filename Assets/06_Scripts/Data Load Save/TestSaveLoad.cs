using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestSaveLoad : MonoBehaviour
{
    [SerializeField]
    private MainQuestData _questData;
    [SerializeField]
    private MainEquipData _equipData;
    [SerializeField]
    private MainPlayerData _playerData;

    #region Singleton
    private static TestSaveLoad instance;

    public static TestSaveLoad Instance
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

    private void Start()
    {
        _questData = GetComponent<MainQuestData>();
        _equipData = GetComponent<MainEquipData>();
        _playerData = GetComponent<MainPlayerData>();
    }

    public void InitializeData()
    {
        _playerData.Initialize();
        _equipData.Initialize();
        _questData.Initialize();
    }

    public void SaveData()
    {
        _playerData.SavePlayerData();
        _playerData.SaveInventoryData();
        _equipData.SaveEquipData();
        _questData.SaveQuestData();
    }

    public void LoadData()
    {
        _playerData.LoadPlayerData();
        _playerData.LoadInventoryData();
        _equipData.LoadEquipData();
        _questData.LoadQuestData();
    }

    public void SavePlayerData()
    {
        _playerData.SavePlayerData();
    }

    public void SaveInventoryData()
    {
        _playerData.SaveInventoryData();
    }

    public void SaveWeaponPanelData()
    {
        _equipData.SaveEquipData();
    }

    public void SaveQuestData()
    {
        _questData.SaveQuestData();
    }

    public void LoadPlayerData()
    {
        _playerData.LoadPlayerData();
    }

    public void LoadInventoryData()
    {
        _playerData.LoadInventoryData();
    }

    public void LoadEquipData()
    {
        _equipData.LoadEquipData();
    }

    public void LoadQuestData()
    {
        _questData.LoadQuestData();
    }
}
