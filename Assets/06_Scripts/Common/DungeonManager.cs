using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
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

    public int _mosnterCount;
    public bool _canSpawnBoss = false;

    public int MonsterCount { get { return _mosnterCount; } set { _mosnterCount = value; } }
    public bool CanSapwnBoss { get { return _canSpawnBoss; } set { _canSpawnBoss = value; } }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }
}
