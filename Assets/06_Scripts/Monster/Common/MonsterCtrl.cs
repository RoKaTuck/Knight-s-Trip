using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : FSM<MonsterCtrl>, IMonsterBase
{
    protected SkeletonAnim _skeletonAnim;

    private void Update()
    {
        FSMUpdate();
    }

    public virtual void Attack(){}

    public virtual void Patrol(){}

    public virtual void Idle(){}
}
