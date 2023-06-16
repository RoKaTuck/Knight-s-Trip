using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_AttackState : FSMSingleton<FSM_AttackState>, IFSMState<MonsterCtrl>
{
    public void Enter(MonsterCtrl e)
    {
        Debug.Log("Attack State µπ¿‘");
    }

    public void Execute(MonsterCtrl e)
    {
        e.Attack();
    }

    public void Exit(MonsterCtrl e)
    {
        
    }
    
}
