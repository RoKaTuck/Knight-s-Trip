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

    [SerializeField, Header("스켈레톤 스테이터스")]
    private string _name;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _dmg;
    [SerializeField]
    private int _def;
    [SerializeField]
    private float _speed;

    [SerializeField, Header("인식 필요요소")]
    private SphereCollider _detectForWakeUp;

    // 공격 인정거리
    [SerializeField]
    private float _validAttackDist;

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
        
    public override void Attack()
    {
        Debug.Log("스켈레톤 어택");
    }

    public override void Chase()
    {
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
        if(_monsterDetect.DetectForWakeUp(_detectForWakeUp.radius) == true)
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
