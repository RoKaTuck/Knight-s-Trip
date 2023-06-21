using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Spawn : SpawnManager
{
    [SerializeField]
    private GameObject _warriorSkeletonPrefab;
    [SerializeField]
    private GameObject _bossSkeletonPrefab;
    [SerializeField]
    private Transform _bossSpawnPos;

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
        {
            CreateMonster(_spawnPos[i]);            
        }
    }

    private void Update()
    {
        // 한마리만 소환하게 만들어야함
        if (DungeonManager.Instance.CanSapwnBoss == true)
            CreateBoss();

        if (DungeonManager.Instance._mosnterCount <= 0)
            DungeonManager.Instance.CanSapwnBoss = true;
    }

    public override void CreateMonster(Transform spawnPos)
    {        
        var newMonster = _poolingSystem.InstantiateAPS("SkeletonWarrior",
                         spawnPos.position, spawnPos.rotation, Vector3.one);   
                
    }

    public override void CreateBoss()
    {
        DungeonManager.Instance.CanSapwnBoss = false;

        Instantiate(_bossSkeletonPrefab, _bossSpawnPos.position, Quaternion.identity);
    }
}
