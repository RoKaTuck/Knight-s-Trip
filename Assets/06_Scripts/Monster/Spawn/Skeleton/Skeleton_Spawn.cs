using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Spawn : SpawnManager
{    
    public int _spawnCount;

    private ObjectPoolingSystem _poolingSystem;
    private Transform[] _spawnPos;

    private void Start()
    {
        _spawnPos = GetComponentsInChildren<Transform>();
        _poolingSystem = ObjectPoolingSystem._instance;
        _spawnCount = _spawnPos.Length - 1;

        DungeonManager.Instance.MonsterCount = _spawnCount;

        for (int i = 1; i < _spawnPos.Length; ++i)
            CreateMonster(_spawnPos[i]);            
    }

    private void Update()
    {
        if (DungeonManager.Instance.MonsterCount <= 0 && DungeonManager.Instance.DungeonClear == false)
        {
            DungeonManager.Instance.DungeonClear = true;
        }
    }

    public override void CreateMonster(Transform spawnPos)
    {        
        var newMonster = _poolingSystem.InstantiateAPS("SkeletonWarrior",
                         spawnPos.position, spawnPos.rotation, Vector3.one);                   
    }    
}
