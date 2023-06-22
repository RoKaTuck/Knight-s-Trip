using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCtrl : MonoBehaviour
{    
    // �ʿ��� ������Ʈ
    [SerializeField]
    private SwordCtrl _swordCtrl;
    [SerializeField]
    private PlayerMoveCtrl _moveCtrl;
    [SerializeField]
    private SphereCollider _norAttackArea;

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
        if(Inventory._inventoryActivated == false && UiManager._isUiActivated == false && NpcCtrl._isInteracting == false)
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
            monster._hp -= 100 - monster._def;

            Debug.Log($"�̸� : {monster._name} �ǰ�!");
        }
    }

    public void OnSwordEffect()
    {
        
    }

    
}
