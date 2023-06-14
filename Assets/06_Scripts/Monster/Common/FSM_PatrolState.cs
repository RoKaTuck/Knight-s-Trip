using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_PatrolState : FSMSingleton<FSM_PatrolState>, IFSMState<MonsterCtrl>
{
    private bool _patrolStart = false;

    public void Enter(MonsterCtrl e)
    {        
        if ((object)e.PreviousState == FSM_IdleState._Inst)
            StartCoroutine(CRT_StartPatrolDelay());
        else if (e.PreviousState == FSM_AttackState._Inst) { }        
    }

    public void Execute(MonsterCtrl e)
    {
        if(_patrolStart == true)
            Debug.Log("¼øÂû ½ÃÀÛ");
    }

    public void Exit(MonsterCtrl e)
    {
        _patrolStart = false;
    }

    IEnumerator CRT_StartPatrolDelay()
    {
        yield return new WaitForSeconds(3f);
        _patrolStart = true;

        yield break;
    }
}
