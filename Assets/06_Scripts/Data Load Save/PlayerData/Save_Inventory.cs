using System.IO;
using UnityEngine;

public partial class Save_Load
{
    private Inventory _inven;

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
}
