using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int _level = 1;
    public int _exp = 0;
    public int _maxExp = 100;
    public int _gold;
    public bool _isDungeon = false;
    public bool _inGame = false;
    public bool _playerDeath = false;

    public int _Level { get { return _level; } set { _level = value; } }
    public int _Exp { get { return _exp; } set { _exp = value; } }
    public int _MaxExp { get { return _maxExp; } set { _maxExp = value; } }
    public int _Gold { get { return _gold; } set { _gold = value; } }
    public bool _IsDungeon { get { return _isDungeon; } set { _isDungeon = value; } }
    public bool _InGame { get { return _inGame; } set { _inGame = value; } }
    public bool _PlayerDeath { get { return _playerDeath; } set { _playerDeath = value; } }

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
        if (_IsDungeon == false && _InGame == true)
        {
            //TestSaveLoad.Instance.SaveData();
            Save_Load.Instance.SaveData();
        }
    }
    
    public void IncreaseExp(int count)
    {
        _Exp += count;

        if(_Exp >= _MaxExp)
        {
            int levelUpCount = _Exp / _MaxExp;

            _Level += levelUpCount;
            _Exp -= levelUpCount * _MaxExp;
            _MaxExp *= 2 * levelUpCount;
        }        
    }

    public void DecreaseExp(int count)
    {
        _Exp -= count;

        if (_Exp <= 0)
            _Exp = 0;
    }

    
}
