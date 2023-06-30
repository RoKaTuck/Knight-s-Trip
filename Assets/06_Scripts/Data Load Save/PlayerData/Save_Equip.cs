using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipData
{
    // 장비창 데이터
    public List<int> _equipItemIdx = new List<int>();
}

public partial class Save_Load
{
    private WeaponPanel _weaponPanel;
    private EquipData _equipData = new EquipData();

    private string SAVE_EQUIPNAME = "EquipFile.txt";
    private string INIT_EQUIPNAME = "EquipFile_Original.txt";

    public void InitializeEquip()
    {
        string initJsonEquipData = File.ReadAllText(INIT_DATA_DIRECTORY + INIT_EQUIPNAME);
        _equipData = JsonUtility.FromJson<EquipData>(initJsonEquipData);

        string jsonEquip = JsonUtility.ToJson(_equipData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME, jsonEquip); // 경로 + 파일 이름        
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
}
