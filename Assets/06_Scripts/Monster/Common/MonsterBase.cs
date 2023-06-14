using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterBase 
{    
    void Attack();
    void Patrol(); 
    void Idle() { }
}
