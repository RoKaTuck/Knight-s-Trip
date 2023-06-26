using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int _gold;
    public int _Gold { get { return _gold; } set { _gold = value; } }

    public bool _isDungeon = false;
    public bool _IsDungeon { get { return _isDungeon; } set { _isDungeon = value; } }

    #region Singleton
    private static GameManager instance;

    public static GameManager Instance
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
        if (null == instance)
        {            
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {            
            Destroy(this.gameObject);
        }
    }
    #endregion


    private void OnApplicationQuit()
    {
        if (_IsDungeon == false)
        {
            //TestSaveLoad.Instance.SaveData();
            Save_Load.Instance.SaveData();
        }
    }
}
