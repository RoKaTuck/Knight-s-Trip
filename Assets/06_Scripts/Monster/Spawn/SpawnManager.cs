using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpawnManager : MonoBehaviour
{
    public virtual void CreateBoss(){}

    public abstract GameObject CreateMonster(GameObject mosnterPrefab);    
}
