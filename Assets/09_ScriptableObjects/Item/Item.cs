using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string _itemName; // 아이템의 이름
    [TextArea]
    public string _itemDesc; // 아이템의 설명
    public int _sellPrice; // 판매 가격
    public int _buyPrice; // 구매 가격
    public int _reinforceLevel = 0; // 강화 레벨
    public int _dmg; // 무기 공격력
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
