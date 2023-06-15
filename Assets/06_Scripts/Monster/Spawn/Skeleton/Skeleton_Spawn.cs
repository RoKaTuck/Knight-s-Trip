using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Spawn : SpawnManager
{
    [SerializeField]
    private GameObject _warriorSkeletonPrefab;

    private ObjectPoolingSystem _poolingSystem;
    private Transform[] _spawnPos;

    private void Start()
    {
        _spawnPos = GetComponentsInChildren<Transform>();
        _poolingSystem = ObjectPoolingSystem._instance;

        for(int i = 1; i < _spawnPos.Length; ++i)
        {
            CreateMonster(_spawnPos[i]);            
        }
    }

    public override void CreateMonster(Transform spawnPos)
    {        
        var newMonster = _poolingSystem.InstantiateAPS("SkeletonWarrior",
                         spawnPos.position, spawnPos.rotation, Vector3.one);   
                
    }
}
