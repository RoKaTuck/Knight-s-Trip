using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int _gold;
    public int _Gold { get { return _gold; } set { _gold = value; } }

    public bool _uiActive = false;
    public bool _UiActive { get { return _uiActive; } set { _uiActive = value; } }

    public bool _isDungeon = false;
    public bool _IsDungeon { get { return _isDungeon; } set { _isDungeon = value; } }

    #region Sigleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
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
        if(_IsDungeon == false)
            Save_Load.Instance.SaveData();        
    }
}
