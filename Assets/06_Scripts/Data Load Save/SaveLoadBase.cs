using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadBase : MonoBehaviour
{
    protected string SAVE_DATA_DIRECTORY;

    protected string SAVE_FILENAME = "SaveFile.txt";
    protected string SAVE_QUESTNAME = "QuestSave.txt";
    protected string SAVE_EQUIPNAME = "EquipFile.txt";

    protected PlayerMoveCtrl _moveCtrl;
    protected StatusCtrl _statusCtrl;
    protected Inventory _inven;
    protected WeaponPanel _weaponPanel;

    public virtual void Save() { }
    public virtual void SaveAdditory() { }
    public virtual void Load() { }
    public virtual void Initialize() { }

    protected void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);            
        }
    }
}
