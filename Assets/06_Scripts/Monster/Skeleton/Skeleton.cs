using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonsterCtrl 
{    
    [SerializeField, Header("Ω∫ƒÃ∑π≈Ê µ•¿Ã≈Õ")]
    private SkeletonData _skeletonData;

    [SerializeField, Header("Ω∫ƒÃ∑π≈Ê Ω∫≈◊¿Ã≈ÕΩ∫")]
    private string _name;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _dmg;
    [SerializeField]
    private int _def;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private bool _inSight = false;    

    private void Awake()
    {
        _skeletonAnim = GetComponent<SkeletonAnim>();
        InitState(this, FSM_IdleState._Inst);
    }

    private void Start()
    {
        // µ•¿Ã≈Õ ¿ŒΩ∫≈œΩ∫
        _name  = _skeletonData.name;
        _hp    = _skeletonData._hp;
        _dmg   = _skeletonData._dmg;
        _def   = _skeletonData._def;
        _speed = _skeletonData._speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            _inSight = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            _inSight = false;
    }
        
    public override void Attack()
    {
        Debug.Log("Ω∫ƒÃ∑π≈Ê æÓ≈√");
    }

    public override void Patrol()
    {
        Debug.Log("Ω∫ƒÃ∑π≈Ê º¯¬˚");
    }

    public override void Idle()
    {      
        if(_inSight == true)
        {            
            _skeletonAnim.IdleAnim();
            ChangeState(FSM_PatrolState._Inst);
        }        
    }   
}
