using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_AttackState : FSMSingleton<FSM_AttackState>, IFSMState<MonsterCtrl>
{
    public void Enter(MonsterCtrl e)
    {
        Debug.Log("Attack State ����");
    }

    public void Execute(MonsterCtrl e)
    {
        
    }

    public void Exit(MonsterCtrl e)
    {
        
    }
    
}
