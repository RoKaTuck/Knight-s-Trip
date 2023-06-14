using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Spawn : SpawnManager
{
    [SerializeField]
    private GameObject _warriorSkeletonPrefab;

    private Transform[] _spawnPos;

    private void Start()
    {
        _spawnPos = GetComponentsInChildren<Transform>();
        
        for(int i = 1; i < _spawnPos.Length; ++i)
        {
            CreateMonster(_spawnPos[i]);
            Debug.Log(_spawnPos[i].position);
        }
    }

    public override void CreateMonster(Transform spawnPos)
    {
        var newMonster = SkeletonObjPool.GetObject();
        newMonster.transform.position = spawnPos.position;
        newMonster.transform.rotation = spawnPos.rotation;        
    }
}
