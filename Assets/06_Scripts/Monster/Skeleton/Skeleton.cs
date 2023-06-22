using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

// 새로운 스켈레톤 생성시 공통적인 요소인 스테이터스 부분과 데이터 부분은
// 부모 클래스인 MonsterCtrl에 Protected로 선언해서 정리하고
// 공격 순찰 대기 등 각 상태에 따른 기능들은 오버라이드해서 따로 구현

// 여유가 있으면 각 비슷한 요소들끼리 컴포넌트 별로 관리해서 작업

// 여유가 없으면 우선은 MonsterCtrl에서 상속받는 방식으로 사용
public class Skeleton : MonsterCtrl 
{    
    [SerializeField, Header("스켈레톤 데이터")]
    private SkeletonData _skeletonData;
 

    [SerializeField, Header("인식 필요요소")]
    private SphereCollider _detectForWakeUp;

    // 공격 인정거리
    [SerializeField]
    private float _validAttackDist;

    private bool _isAttack = false;
    private bool _isReturn = false;
    private bool _canMove = true;
    private Vector3 _originPos;
    private Transform _targetPos;

    // 필요한 컴포넌트
    private NavMeshAgent  _navAgent;    
    private MonsterDetect _monsterDetect;
    private MonsterPatrol _monsterPatrol;

    private void Awake()
    {
        _navAgent      = GetComponent<NavMeshAgent>();
        _skeletonAnim  = GetComponent<SkeletonAnim>();
        _monsterPatrol = GetComponent<MonsterPatrol>();
        _monsterDetect = GetComponent<MonsterDetect>();              

        _navAgent.enabled = false;

        InitState(this, FSM_IdleState._Inst);
    }

    private void Start()
    {
        // 데이터 인스턴스
        _name  = _skeletonData.name;
        _hp    = _skeletonData._hp;
        _dmg   = _skeletonData._dmg;
        _def   = _skeletonData._def;
        _speed = _skeletonData._speed;

        _navAgent.speed = _speed;        
        _originPos = transform.position;
    }    
     
    IEnumerator CRT_DeathDelay(float deathTime)
    {
        _skeletonAnim.DeathAnim();

        yield return new WaitForSeconds(deathTime);

        QuestManager.Instance.InceaseQuestCondition(DungeonManager.Instance._questId, 1);
        DungeonManager.Instance.MonsterCount -= 1;

        if (DungeonManager.Instance.MonsterCount <= 0)
            DungeonManager.Instance.DungeonClear = true;

        gameObject.DestroyAPS();

        yield break;
    }

    public void OnAttackEnd()
    {
        _isAttack = false;        
        _skeletonAnim.AttackAnim(0);
        //_navAgent.enabled = false;
        //transform.LookAt(_targetPos);
        //_navAgent.enabled = true;
    }

    public override void Death()
    {
        StartCoroutine(CRT_DeathDelay(2.7f));
        // 아이템 뿌리는거 구현
    }

    public override void Attack()
    {
        if (_hp <= 0)
        {
            ChangeState(FSM_DeathState._Inst);
            return;
        }

        bool validDist = _monsterDetect.View(ref _targetPos);


        if (_isAttack == false)
        {
            if (validDist == true && (_targetPos.position - transform.position).sqrMagnitude <= Mathf.Pow(_validAttackDist, 2))
            {
                _isAttack = true;                
                _skeletonAnim.AttackAnim(Random.Range(1, 5));
            }
            else if (validDist == true && (_targetPos.position - transform.position).sqrMagnitude > Mathf.Pow(_validAttackDist, 2))
                ChangeState(FSM_ChaseState._Inst);
        }        
    }

    public override void Chase()
    {
        if (_hp <= 0)
        {
            ChangeState(FSM_DeathState._Inst);
            return;
        }

        _monsterDetect._viewAngle = 360;

        _skeletonAnim.ChaseAnim(_canMove);
        bool findPlayer = _monsterDetect.View(ref _targetPos);

        if (_isReturn == false)
        {
            if ((_originPos - transform.position).sqrMagnitude >= Mathf.Pow(10f, 2))
            {
                findPlayer = false;
                _isReturn = true;
                return;
            }

            if((_targetPos.position - transform.position).sqrMagnitude <= Mathf.Pow(_validAttackDist, 2))
            {
                ChangeState(FSM_AttackState._Inst);
            }

            _monsterPatrol.SetDestination(_navAgent, _targetPos.position);
        }
        if(_isReturn == true || findPlayer == false)
        {
            bool resetSuccess = _monsterPatrol.ResetPosition(_navAgent, _originPos);

            if (resetSuccess == true)
            {
                _isReturn = false;
                ChangeState(FSM_PatrolState._Inst);
            }
        }
    }

    public override void Patrol()
    {
        if (_hp <= 0)
        {
            ChangeState(FSM_DeathState._Inst);
            return;
        }

        _monsterDetect._viewAngle = 120;

        _monsterPatrol.MoveDestination(_navAgent, out _canMove);
        _skeletonAnim.ChaseAnim(_canMove);

        if(_monsterDetect.View(ref _targetPos) == true)
        {
            _canMove = true;

            ChangeState(FSM_ChaseState._Inst);
        }
    }

    public override void Idle()
    {                
        if(_monsterDetect.DetectForWakeUp(transform.position, _detectForWakeUp.radius) == true)
        {            
            _skeletonAnim.IdleAnim();
            _navAgent.enabled = true;
            ChangeState(FSM_PatrolState._Inst);
        }        
    }

    public override void PatrolStart()
    {
        _monsterPatrol.PatrolStart(_navAgent);
    }
}
