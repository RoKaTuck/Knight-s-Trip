using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_DeathState : FSMSingleton<FSM_DeathState>, IFSMState<MonsterCtrl>
{
    public void Enter(MonsterCtrl e)
    {
        Debug.Log("Death State µπ¿‘");
        e.Death();
    }

    public void Execute(MonsterCtrl e)
    {
        
    }

    public void Exit(MonsterCtrl e)
    {
        
    }
}
