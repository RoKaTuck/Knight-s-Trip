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
 

    [SerializeField, Header("�ν� �ʿ���")]
    private SphereCollider _detectForWakeUp;
    [SerializeField]
    private SphereCollider _attackArea;

    // ���� �����Ÿ�
    [SerializeField]
    private float _validAttackDist;

    private bool _isAttack = false;
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
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

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
        _exp   = _skeletonData._exp;

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

        EXP playerExp = _targetPos.gameObject.GetComponent<PlayerExp>();
        playerExp.IncreaseExp(_exp);

        gameObject.DestroyAPS();

        yield break;
    }

    public void OnAttackEnd()
    {
        _isAttack = false;        
        _skeletonAnim.AttackAnim(0);
    }

    public override void Death()
    {
        StartCoroutine(CRT_DeathDelay(2.7f));
        // ������ �Ѹ��°� ����
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

    private void OnDamageToPlayer()
    {
        Collider[] players = new Collider[3];
        Physics.OverlapSphereNonAlloc(_attackArea.transform.position,
                                      _attackArea.radius, players, 1 << 6);

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null)
                break;

            if (players[i] != null)
            {
                StatusCtrl hp = FindObjectOfType<StatusCtrl>();
                PlayerCtrl player = players[i].GetComponent<PlayerCtrl>();

                hp.DecreaseHp(_dmg - player._Def);

                if(hp.GetCurrentHp() <= 0)
                {
                    player.Death();
                }
            }
        }
    }
}
