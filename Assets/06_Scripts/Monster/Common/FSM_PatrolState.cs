using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_PatrolState : FSMSingleton<FSM_PatrolState>, IFSMState<MonsterCtrl>
{    
    public void Enter(MonsterCtrl e)
    {
        Debug.Log("Patrol State µπ¿‘");
        if ((object)e.PreviousState == FSM_IdleState._Inst)
            StartCoroutine(CRT_StartPatrolDelay(e));
        else if ((object)e.PreviousState == FSM_AttackState._Inst || (object)e.PreviousState == FSM_ChaseState._Inst)
        {
            e._patrolStart = true;
            e.PatrolStart();
        }
    }

    public void Execute(MonsterCtrl e)
    {
        if (e._patrolStart == true)
            e.Patrol();
    }

    public void Exit(MonsterCtrl e)
    {
        e._patrolStart = false;
    }

    IEnumerator CRT_StartPatrolDelay(MonsterCtrl e)
    {
        yield return new WaitForSeconds(3f);
        e._patrolStart = true;

        e.PatrolStart();
        yield break;
    }
}
