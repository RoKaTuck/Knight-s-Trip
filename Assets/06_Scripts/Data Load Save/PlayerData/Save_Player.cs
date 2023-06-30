using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // �÷��̾� ��ġ
    public Vector3 _playerPos;
    public Vector3 _playerRot;

    // �÷��̾� ����
    public int _playerGold;
    public int _playerHp;
    public int _playerMp;
    public int _playerLevel;
    public int _playerExp;

    // �÷��̾� �κ��丮
    public List<int> _invenitemIdx = new List<int>();
    public List<int> _invenArrayNumber = new List<int>();
    public List<string> _invenItemName = new List<string>();
    public List<int> _invenItemNumber = new List<int>();

    // �÷��̾� ����Ʈ
    public List<QuestData> _questDataList = new List<QuestData>();
}

public partial class Save_Load 
{
    private PlayerCtrl _playerCtrl;
    private StatusCtrl _statusCtrl;
    private PlayerData _playerData = new PlayerData();

    private string SAVE_PLAYERDATA = "PlayerData.txt";
    private string INIT_PLAYERDATA = "PlayerData_Original.txt";

    public void InitializePlayer()
    {
        // �÷��̾� ������
        string initJsonPlayerData = File.ReadAllText(INIT_DATA_DIRECTORY + INIT_PLAYERDATA);
        _playerData = JsonUtility.FromJson<PlayerData>(initJsonPlayerData);

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA, jsonPlayer); // ��� + ���� �̸�
    }

    public void SavePlayerData()
    {
        _playerCtrl = FindObjectOfType<PlayerCtrl>();
        _statusCtrl = FindObjectOfType<StatusCtrl>();

        _playerData._playerPos = _playerCtrl.gameObject.transform.position;
        _playerData._playerRot = _playerCtrl.gameObject.transform.eulerAngles;

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA, jsonPlayer); // ��� + ���� �̸�
    }

    public void SavePlayerStatData()
    {
        _playerData._playerHp = _statusCtrl._Hp;
        _playerData._playerMp = _statusCtrl._Mp;

        _playerData._playerLevel = GameManager.Instance._Level;
        _playerData._playerExp = GameManager.Instance._Exp;

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA, jsonPlayer); // ��� + ���� �̸�
    }

    public void LoadPlayerData()
    {
        string loadJsonPlayer = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA);
        _playerData = JsonUtility.FromJson<PlayerData>(loadJsonPlayer);

        _playerCtrl = FindObjectOfType<PlayerCtrl>();
        _statusCtrl = FindObjectOfType<StatusCtrl>();

        _playerCtrl.gameObject.transform.position = _playerData._playerPos;
        _playerCtrl.gameObject.transform.eulerAngles = _playerData._playerRot;
    }

    public void LoadPlayerStatData()
    {
        string loadJsonPlayer = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA);
        _playerData = JsonUtility.FromJson<PlayerData>(loadJsonPlayer);

        GameManager.Instance._Level = _playerData._playerLevel;
        GameManager.Instance._Exp = _playerData._playerExp;

        _statusCtrl._Hp = _playerData._playerHp;
        _statusCtrl._Mp = _playerData._playerMp;
    }
}

