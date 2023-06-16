using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : WeaponAttack
{
    public override void Attack(PlayerAnimCtrl animCtrl, PlayerMoveCtrl moveCtrl, float currentSwordSpeed)
    {
        if (Input.GetMouseButtonDown(0) && Inventory._inventoryActivated == false)
        {
            moveCtrl._Move = false;            
            animCtrl.SwordAttackAnim(currentSwordSpeed);
        }
    }
}

public class WeaponLongSword : WeaponAttack
{
    public override void Attack(PlayerAnimCtrl animCtrl, PlayerMoveCtrl moveCtrl, float currentSwordSpeed)
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveCtrl._Move = false;
            animCtrl.LongSwordAttackAnim(currentSwordSpeed);
        }
    }
}

public class SwordCtrl : MonoBehaviour
{
    //[SerializeField]
    public Sword _currentSword;
    private float _currentSwordSpeed;

    [SerializeField]
    ParticleSystem _swordEffect;

    // 필요한 컴포넌트
    [SerializeField]
    private PlayerAnimCtrl _animCtrl;
    [SerializeField]
    private PlayerMoveCtrl _moveCtrl;
    [SerializeField]
    private PlayerAttackCtrl _attackCtrl;    

    private void Start()
    {        
        WeaponManager._currentWeapon = _currentSword.GetComponent<Transform>();        
    }    

    public void WeaponAttack(WeaponAttack attackType)
    {
        _currentSwordSpeed = WeaponSpeedCalc(_currentSword._swordSpeed);
        attackType.Attack(_animCtrl, _moveCtrl, _currentSwordSpeed);
    }

    public void SetWeaponType(WeaponAttack attackType)
    {
        _attackCtrl._weaponAttack = attackType;
    }

    public void SwordChange(Sword sword)
    {
        if (WeaponManager._currentWeapon != null)
            WeaponManager._currentWeapon.gameObject.SetActive(false);

        _currentSword = sword;
        _attackCtrl._attackType = (PlayerAttackCtrl.eAttackType)sword._swordType;
        
        WeaponManager._currentWeapon = _currentSword.GetComponent<Transform>();

        _currentSword.transform.localPosition = Vector3.zero;
        _currentSword.gameObject.SetActive(true);
    }    
    

    private float WeaponSpeedCalc(float weaponSpeed)
    {
        _currentSwordSpeed = 1 * weaponSpeed;
        return _currentSwordSpeed;
    }   
}
