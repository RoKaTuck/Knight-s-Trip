using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCtrl : MonoBehaviour
{
    [SerializeField]
    private Sword _currentSword;
    private float _currentSwordSpeed;

    [SerializeField]
    ParticleSystem _swordEffect;

    private PlayerAnimCtrl _animCtrl;
    private PlayerMoveCtrl _moveCtrl;

    public enum eAttackType
    {
        Sword,
        LongSword
    }

    public eAttackType _attackType = eAttackType.Sword;

    private void Start()
    {
        _animCtrl = GetComponent<PlayerAnimCtrl>();
        _moveCtrl = GetComponent<PlayerMoveCtrl>();

        WeaponManager._currentWeapon = _currentSword.GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {        
        TryAttack(_attackType);
    }

    public void SwordChange(Sword sword)
    {        
        if(WeaponManager._currentWeapon != null)
            WeaponManager._currentWeapon.gameObject.SetActive(false);

        _currentSword = sword;
        _attackType = (eAttackType)sword._swordType;
        WeaponManager._currentWeapon = _currentSword.GetComponent<Transform>();        

        _currentSword.transform.localPosition = Vector3.zero;
        _currentSword.gameObject.SetActive(true);
    }

    public void OnAttackEnd()
    {
        _moveCtrl._Move = true;
    }

    public void OnSwordEffect()
    {
        //ParticleSystem swordEffect = Instantiate(_swordEffect, )
    }

    private void TryAttack(eAttackType attackType)
    {
        if (Input.GetMouseButtonDown(0) && Inventory._inventoryActivated == false)
        {
            switch (attackType)
            {
                case eAttackType.Sword:
                    WeaponSpeedCalc(_currentSword._swordSpeed);
                    SwordAttack(_currentSwordSpeed);
                    break;
                case eAttackType.LongSword:
                    WeaponSpeedCalc(_currentSword._swordSpeed);
                    LongSwordAttack(_currentSwordSpeed);
                    break;
            }
        }
    }

    private void SwordAttack(float currentSwordSpeed)
    {        
        _animCtrl.SwordAttackAnim(_currentSwordSpeed);
        _moveCtrl._Move = false;
    }

    private void LongSwordAttack(float currentSwordSpeed)
    {
        _animCtrl.LongSwordAttackAnim(currentSwordSpeed);
        _moveCtrl._Move = false;
    }

    private float WeaponSpeedCalc(float weaponSpeed)
    {
        _currentSwordSpeed = 1 * weaponSpeed;
        return _currentSwordSpeed;
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
