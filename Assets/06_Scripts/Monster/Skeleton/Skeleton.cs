using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

// ���ο� ���̷��� ������ �������� ����� �������ͽ� �κа� ������ �κ���
// �θ� Ŭ������ MonsterCtrl�� Protected�� �����ؼ� �����ϰ�
// ���� ���� ��� �� �� ���¿� ���� ��ɵ��� �������̵��ؼ� ���� ����

// ������ ������ �� ����� ��ҵ鳢�� ������Ʈ ���� �����ؼ� �۾�

// ������ ������ �켱�� MonsterCtrl���� ��ӹ޴� ������� ���
public class Skeleton : MonsterCtrl 
{    
    [SerializeField, Header("���̷��� ������")]
    private SkeletonData _skeletonData;

    [SerializeField, Header("���̷��� �������ͽ�")]
    private string _name;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _dmg;
    [SerializeField]
    private int _def;
    [SerializeField]
    private float _speed;

    [SerializeField, Header("�ν� �ʿ���")]
    private SphereCollider _detectForWakeUp;

    // ���� �����Ÿ�
    [SerializeField]
    private float _validAttackDist;

    private bool _isReturn = false;
    private bool _canMove = true;
    private Vector3 _originPos;
    private Transform _targetPos;

    // �ʿ��� ������Ʈ
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
        // ������ �ν��Ͻ�
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
        Debug.Log("���̷��� ����");
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
