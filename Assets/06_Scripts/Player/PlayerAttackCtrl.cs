using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCtrl : MonoBehaviour
{
    public int _skillDamage = 0;

    // 필요한 컴포넌트
    [SerializeField]
    private SwordCtrl _swordCtrl;
    [SerializeField]
    private PlayerMoveCtrl _moveCtrl;
    [SerializeField]
    private SphereCollider _norAttackArea;
    private PlayerCtrl _playerCtrl;
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
        _playerCtrl = GetComponent<PlayerCtrl>();
    }    

    public void Attack()
    {
        _swordCtrl.WeaponAttack(_weaponAttack);
    }

    public void OnAttackEnd()
    {
        _moveCtrl._Move = true;
    }

    public void OnSwordAttackEnd()
    {
        Collider[] monsters = new Collider[20];
        Physics.OverlapSphereNonAlloc(transform.position, _norAttackArea.radius, monsters, 1 << 8);

        for(int i = 0; i < monsters.Length; i++)
        {
            if (monsters[i] == null)
                break;

            MonsterCtrl monster = monsters[i].GetComponent<MonsterCtrl>();
            monster.Hit(_playerCtrl._Atk + _skillDamage);

            Debug.Log($"이름 : {monster._name} 피격!");

            if (monster._hp <= 0)
            {
                Debug.Log($"경험치 : {monster._exp} 획득");
            }

            _skillDamage = 0;
        }
    }        
}
