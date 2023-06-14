using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BearType
{
    Normal,
    Boss
}

public class BearSpawn : SpawnManager
{
    [SerializeField, Header("Bear Spawn Attribute")]
    private List<BearData> _bearDatas;
    [SerializeField]
    private GameObject _NormalBearPrefab;
    [SerializeField]
    private GameObject _BossBearPrefab;

    private void Start()
    {
        //var bear = CreateMonster(_NormalBearPrefab);
    }

    public override void CreateMonster(Transform spawnPos)
    {
        //var newBear = Instantiate(_NormalBearPrefab).GetComponent<NormalBear>();
        //newBear.BearData = _bearDatas[(int)BearType.Normal];

        //return newBear.gameObject;
        //        
        
    }
}
