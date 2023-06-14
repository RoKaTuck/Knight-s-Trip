using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpawnManager : MonoBehaviour
{
    public abstract void CreateMonster(Transform spawnPos); 
    public virtual void CreateBoss(){}
}
