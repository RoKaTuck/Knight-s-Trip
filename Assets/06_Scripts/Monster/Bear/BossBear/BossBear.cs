using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBear : MonsterCtrl
{
    [SerializeField, Header("BossBear 데이터")]
    private BearData _bossBearData;
    [SerializeField]
    private bool _canMove = true;
    [SerializeField]
    private float _validAttackDist;
    [SerializeField]
    private float _chaseStartDelay = 3f;
    [SerializeField]
    private GameObject[] _dropItems;

    [SerializeField, Header("인식 필요요소")]
    private SphereCollider _detectForWakeUp;
    [SerializeField]
    private SphereCollider _normalAttackArea;

    // 필요한 컴포넌트
    [SerializeField]
    private Transform _particlePos;
    private MonsterDetect _monsterDetect;
    private MonsterPatrol _monsterPatrol;
    private NavMeshAgent _navAgent;
    private ObjectPoolingSystem _poolingSystem;

    private Vector3 _originPos;
    private Transform _targetPos;
    private bool _isReturn = false;
    private bool _isChase;
    private bool _isAttack;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();        
        _bossBearAnim = GetComponent<BossBearAnim>();
        _monsterPatrol = GetComponent<MonsterPatrol>();
        _monsterDetect = GetComponent<MonsterDetect>();
        _poolingSystem = ObjectPoolingSystem._instance;

        _originPos = transform.position;

        InitState(this, FSM_IdleState._Inst);
    }

    private void Start()
    {        
        _name = _bossBearData.name;
        _hp = _bossBearData._hp;
        _dmg = _bossBearData._dmg;
        _def = _bossBearData._def;
        _speed = _bossBearData._speed;
        _exp = _bossBearData._exp;
        _navAgent.speed = _speed;        
    }    

    private IEnumerator CRT_DeathDelay(float delay)
    {
        _bossBearAnim.DeathAnim();

        yield return new WaitForSeconds(delay);

        DungeonManager.Instance.DungeonClear = true;
        QuestManager.Instance.InceaseQuestCondition(DungeonManager.Instance._questId, 1);
        GameManager.Instance.IncreaseExp(_exp);
        DropItem();

        Destroy(gameObject);

        yield break;
    }

    private IEnumerator CRT_ChaseStartDelay(float delay)
    {
        _isChase = true;

        _bossBearAnim.AwakeAnim();

        yield return new WaitForSeconds(delay);

        _monsterDetect._inSight = false;
        _bossBearAnim.ResetHowling();
        ChangeState(FSM_ChaseState._Inst);        

        yield break;
    }

    public override void Death()
    {
        StartCoroutine(CRT_DeathDelay(3.5f));
        // 아이템 뿌리는거 구현
    }

    public override void Attack()
    {
        if (_hp <= 0)
        {
            ChangeState(FSM_DeathState._Inst);
            return;
        }

        if ((_targetPos.position - transform.position).sqrMagnitude <= Mathf.Pow(_validAttackDist, 2))
            _isAttack = true;
        else
        {
            //_isAttack = false;
            _bossBearAnim.NormalAttacAnim(0);            
            if(_isAttack == false)
                ChangeState(FSM_ChaseState._Inst);
            return;
        }

        if (_isAttack == true)
        {
            _navAgent.enabled = false;
            transform.LookAt(_targetPos);
            _bossBearAnim.NormalAttacAnim(Random.Range(1, 6));
            _navAgent.enabled = true;
        }              
    }

    public override void Chase()
    {
        if (_hp <= 0)
        {
            ChangeState(FSM_DeathState._Inst);
            return;
        }
        
        if (_isReturn == true)
        {
            _canMove = true;
            _bossBearAnim.ChaseAnim(_canMove);
            bool resetSuccess = _monsterPatrol.ResetPosition(_navAgent, _originPos);

            if (resetSuccess == true)
            {
                _isReturn = false;
                _isChase = false;
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                _bossBearAnim.SleepAnim();
                ChangeState(FSM_IdleState._Inst);

                return;
            }
        }

        if ((_originPos - transform.position).sqrMagnitude >= Mathf.Pow(_detectForWakeUp.radius, 2))
        {
            _isReturn = true;
            return;
        }

        if (_isReturn == false)
        {
            bool findPlayer = _monsterDetect.View(ref _targetPos);

            if (findPlayer == true)
            {
                _canMove = true;
                _monsterPatrol.SetDestination(_navAgent, _targetPos.position);
                _bossBearAnim.ChaseAnim(_canMove);
            }
            else
            {
                _canMove = false;
                _bossBearAnim.ChaseAnim(_canMove);
            }

            if (_targetPos != null)
            {
                if ((_targetPos.position - transform.position).sqrMagnitude <= Mathf.Pow(_validAttackDist, 2))
                {
                    _isAttack = true;
                    ChangeState(FSM_AttackState._Inst);
                }
            }
        }        
    }

    public override void Idle()
    {
        if (_monsterDetect.DetectForWakeUp(_originPos, _detectForWakeUp.radius) == true)
        {
            if (_isChase == false)
            {
                StartCoroutine(CRT_ChaseStartDelay(_chaseStartDelay));
                return;
            }
        }
    }

    public override void Hit(int damage)
    {
        _hp -= damage - _def;
        var hitEffect = _poolingSystem.InstantiateAPS("Particle_Hit", transform.position,
                                                      transform.rotation, Vector3.one,
                                                      transform.gameObject);
        hitEffect.transform.localPosition = _particlePos.position;
        hitEffect.transform.localEulerAngles = new Vector3(0, 180, 0);
    }

    private void DropItem()
    {
        int ran = Random.Range(1, 5);

        for(int i = 0; i < ran; i++)
        {
            int randomItem = Random.Range(0, _dropItems.Length);
            Instantiate(_dropItems[randomItem], transform.position, Quaternion.identity);
        }
    }

    private void OnDamageToPlayer()
    {
        Collider[] players = new Collider[3];
        Physics.OverlapSphereNonAlloc(_normalAttackArea.transform.position, 
                                      _normalAttackArea.radius, players, 1 << 6);

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null)
                break;

            if (players[i] != null)
            {
                PlayerCtrl player = players[i].GetComponent<PlayerCtrl>();

                player.Hit(_dmg - player._Def);
            }
        }
    }

    private void OnCheckAfterAttackEnd()
    {
        if ((_targetPos.position - transform.position).sqrMagnitude >= Mathf.Pow(_validAttackDist, 2))
        {            
            _bossBearAnim.NormalAttacAnim(0);
            ChangeState(FSM_ChaseState._Inst);
        }
    }

}
