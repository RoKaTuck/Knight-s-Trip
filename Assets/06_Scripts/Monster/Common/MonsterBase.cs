using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterBase 
{    
    void Attack();
    void Chase();
    void Patrol(); 
    void Idle() { }
}
