using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_ChaseState : FSMSingleton<FSM_ChaseState>, IFSMState<MonsterCtrl>
{
    public void Enter(MonsterCtrl e)
    {
        Debug.Log("ChaseState µπ¿‘");
    }

    public void Execute(MonsterCtrl e)
    {
        e.Chase();
    }

    public void Exit(MonsterCtrl e)
    {
        
    }
}
