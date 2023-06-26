using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipData
{
    // ���â ������
    public List<int> _equipItemIdx = new List<int>();
}


public class MainEquipData : MonoBehaviour
{
    private EquipData _equipData = new EquipData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_EQUIPNAME = "EquipFile.txt";
    private WeaponPanel _weaponPanel;

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
        _equipData._equipItemIdx.Clear();

        string jsonEquip = JsonUtility.ToJson(_equipData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME, jsonEquip); // ��� + ���� �̸�        

        Debug.Log("�ʱ�ȭ �Ϸ�");
    }

    public void SaveEquipData()
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

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME, jsonEquip); // ��� + ���� �̸�

        Debug.Log("���â ����");
    }

    public void LoadEquipData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME))
        {
            string loadJsonEquip = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_EQUIPNAME);
            _equipData = JsonUtility.FromJson<EquipData>(loadJsonEquip);

            _weaponPanel = FindObjectOfType<WeaponPanel>();

            for (int i = 0; i < _equipData._equipItemIdx.Count; i++)
            {
                _weaponPanel.LoadToEquip(_equipData._equipItemIdx[i]);
            }

            Debug.Log("���â ������ �ε� �Ϸ�");
        }        
    }
}
