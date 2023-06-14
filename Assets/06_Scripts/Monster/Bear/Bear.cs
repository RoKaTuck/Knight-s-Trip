using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonsterCtrl
{
    private void Awake()
    {
        InitState(this, FSM_IdleState._Inst);
    }

    public override void Attack()
    {
        Debug.Log("°õ ¾îÅÃ");
    }

    public override void Patrol()
    {
        Debug.Log("°õ ¼øÂû");
    }

    public override void Idle()
    {
        Debug.Log("°õ ´ë±â");
    }
}
