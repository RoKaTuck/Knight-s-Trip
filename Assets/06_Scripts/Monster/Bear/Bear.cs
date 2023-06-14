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
        Debug.Log("�� ����");
    }

    public override void Patrol()
    {
        Debug.Log("�� ����");
    }

    public override void Idle()
    {
        Debug.Log("�� ���");
    }
}
