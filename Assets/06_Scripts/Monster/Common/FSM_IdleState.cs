using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_IdleState : FSMSingleton<FSM_IdleState>, IFSMState<MonsterCtrl>
{
    public void Enter(MonsterCtrl e)
    {
        
    }

    public void Execute(MonsterCtrl e)
    {
        e.Idle();
    }

    public void Exit(MonsterCtrl e)
    {
        
    }
}
