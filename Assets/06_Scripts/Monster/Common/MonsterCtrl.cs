using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : FSM<MonsterCtrl>, IMonsterBase
{
    [SerializeField, Header("몬스터 스테이터스")]
    public string _name;
    [SerializeField]
    public int _hp;
    [SerializeField]
    public int _dmg;
    [SerializeField]
    public int _def;
    [SerializeField]
    public float _speed;

    public bool _patrolStart = false;
    protected SkeletonAnim _skeletonAnim;

    private void Update()
    {
        FSMUpdate();
    }

    public virtual void Death() { }
    public virtual void Attack(){}
    public virtual void Chase(){ }

    public virtual void Patrol(){}

    public virtual void Idle(){}

    public virtual void PatrolStart() { }
}
