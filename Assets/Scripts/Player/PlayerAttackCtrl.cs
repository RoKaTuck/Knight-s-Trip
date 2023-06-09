using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCtrl : MonoBehaviour
{    
    public enum eAttackType
    {
        Warrior,
        LongSword
    }

    public eAttackType _attackType = eAttackType.Warrior;

    private PlayerAnimCtrl _animCtrl;
    private PlayerMoveCtrl _moveCtrl;

    private void Awake()
    {
        _animCtrl = GetComponent<PlayerAnimCtrl>();
        _moveCtrl = GetComponent<PlayerMoveCtrl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (_attackType)
            {
                case eAttackType.Warrior:
                    WarriorNormalAttack();
                    break;
                case eAttackType.LongSword:
                    break;
            }
        }        
            
    }

    public void OnAttackEnd()
    {
        _moveCtrl._Move = true;
    }

    void WarriorNormalAttack()
    {
        _animCtrl.WarriorNormalAttackAnim();
        _moveCtrl._Move = false;
    }

    void WarriorChargingAttack()
    {

    }

    void LongSwordNormalAttack()
    {

    }

    void LongSwordChargingAttack()
    {

    }
}
