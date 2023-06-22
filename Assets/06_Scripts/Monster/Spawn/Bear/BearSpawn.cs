using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BearSpawn : SpawnManager
{
    [SerializeField, Header("Bear Spawn Attribute")]        
    private GameObject _bossBearPrefab;

    private Transform _bossSpawnPos;

    private void Start()
    {
        _bossSpawnPos = transform.GetChild(0).GetComponent<Transform>();
        CreateMonster(_bossSpawnPos);
    }

    public override void CreateMonster(Transform spawnPos)
    {
        GameObject boss = Instantiate(_bossBearPrefab, spawnPos.position, spawnPos.rotation);                
    }
}
