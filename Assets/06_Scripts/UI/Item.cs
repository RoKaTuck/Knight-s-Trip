using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string _itemName; // �������� �̸�
    public eItemType _itemType; // �������� ����                             
    public Sprite _itemImage; // �������� �̹���
    public GameObject _itemPrefab; // �������� ������;

    public string _weaponType; // ���� ����    

    public enum eItemType
    {
        Weapon,
        Armor,
        Used,
        Ingredient,
        Etc
    }
    
}
