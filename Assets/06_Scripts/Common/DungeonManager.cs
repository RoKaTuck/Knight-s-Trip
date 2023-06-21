using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _townPortal;

    public int _questId = 0;
    public int _mosnterCount;
    public bool _canSpawnBoss = false;
    public bool _dungeonClear = false;

    public int MonsterCount { get { return _mosnterCount; } set { _mosnterCount = value; } }
    public bool CanSapwnBoss { get { return _canSpawnBoss; } set { _canSpawnBoss = value; } }
    public bool DungeonClear { get { return _dungeonClear; } set { _dungeonClear = value; } }

    #region Singleton
    private static DungeonManager _instance;
    
    public static DungeonManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new DungeonManager();
            return _instance;
        }
    }



    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }
    #endregion

    private void Start()
    {
        GameManager.Instance._IsDungeon = true;
    }

    private void Update()
    {
        ActivePotal();
    }

    private void ActivePotal()
    {
        if(_dungeonClear == true)
        {
            _dungeonClear = false;
            _townPortal.SetActive(true);
        }
    }
}
