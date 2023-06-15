using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : FSM<MonsterCtrl>, IMonsterBase
{
    public bool _patrolStart = false;
    protected SkeletonAnim _skeletonAnim;

    private void Update()
    {
        FSMUpdate();
    }

    public virtual void Attack(){}
    public virtual void Chase(){ }

    public virtual void Patrol(){}

    public virtual void Idle(){}

    public virtual void PatrolStart() { }
}
