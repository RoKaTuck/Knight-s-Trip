using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string _itemName; // 아이템의 이름
    public eItemType _itemType; // 아이템의 유형                             
    public Sprite _itemImage; // 아이템의 이미지
    public GameObject _itemPrefab; // 아이템의 프리팹;

    public string _weaponType; // 무기 유형    

    public enum eItemType
    {
        Weapon,
        Armor,
        Used,
        Ingredient,
        Etc
    }
    
}
