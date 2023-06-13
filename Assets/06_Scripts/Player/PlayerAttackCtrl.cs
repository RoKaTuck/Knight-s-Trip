using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCtrl : MonoBehaviour
{    
    // 필요한 컴포넌트
    [SerializeField]
    private SwordCtrl _swordCtrl;
    [SerializeField]
    private PlayerMoveCtrl _moveCtrl;

    [HideInInspector]
    public WeaponAttack _weaponAttack;

    public enum eAttackType
    {
        Sword,
        LongSword
    }

    public eAttackType _attackType = eAttackType.Sword;

    private void Start()
    {
        _weaponAttack = new WeaponSword();
    }

    void Update()
    {
        _swordCtrl.WeaponAttack(_weaponAttack);
    }   

    public void OnAttackEnd()
    {
        _moveCtrl._Move = true;
    }

    public void OnSwordEffect()
    {
        
    }

    
}
