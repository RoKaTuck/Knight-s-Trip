using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string _itemName; // �������� �̸� (Ű��)
    [Tooltip("HP, MP, SP�� �����մϴ�.")]
    public string[] _part; // ����
    public int[] _num; // ��ġ

}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] _itemEffects;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private WeaponManager _weaponManager;
    [SerializeField]
    private StatusCtrl _statusCtrl;
    [SerializeField]
    private SlotToolTip _slotToolTip;

    private const string HP = "HP", MP = "MP", SP = "SP";

    public void ShowToolTip(Item item, Vector3 pos)
    {
        _slotToolTip.ShowToolTip(item, pos);
    }

    public void HideToolTip()
    {
        _slotToolTip.HideToolTip();
    }

    public void UseItem(Item _item)
    {
        if (_item._itemType == Item.eItemType.Weapon)
        {
            // ����
            WeaponAttack weaponAttack = new WeaponSword();
            if (_item._weaponType == "SWORD")
                weaponAttack = new WeaponSword();
            else if (_item._weaponType == "LONGSWORD")
                weaponAttack = new WeaponLongSword();

            if (weaponAttack != null)
                StartCoroutine(_weaponManager.CRT_ChangeWeapon(weaponAttack, _item._weaponType, _item._itemName));
        }
        else if (_item._itemType == Item.eItemType.Used)
        {
            for(int i = 0; i < _itemEffects.Length; i++)
            {
                if (_itemEffects[i]._itemName == _item._itemName)
                {
                    for(int j = 0; j < _itemEffects[j]._part.Length; j++)
                    {
                        switch (_itemEffects[i]._part[j])
                        {
                            case HP:
                                _statusCtrl.IncreaseHp(_itemEffects[i]._num[j]);
                                break;
                            case MP:
                                _statusCtrl.IncreaseMp(_itemEffects[i]._num[j]);
                                break;
                            case SP:
                                _statusCtrl.IncreaseSp(_itemEffects[i]._num[j]);
                                break;
                            default:
                                Debug.Log("�߸��� Status ����. HP, MP, SP�� �����մϴ�.");
                                break;
                        }
                        Debug.Log(_item._itemName + _itemEffects[i]._num[j] + " �� ����߽��ϴ�.");
                    }
                    return;
                }                
            }
            Debug.Log("ItemEffectDatabase�� ��ġ�ϴ� ItemName �����ϴ�.");
        }
    }
}
