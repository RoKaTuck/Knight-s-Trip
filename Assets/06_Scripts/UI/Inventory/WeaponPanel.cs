using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    [SerializeField]
    private WeaponSlot[] _weaponSlots;
    [SerializeField]
    private GameObject _slotParent;

    private const string HELM = "HELM", CHEST = "CHEST", SHOES = "SOHES", ACCESSORY = "ACCESSORY";
    private const int HELMIDX = 0, CHESTIDX = 1, SHOESIDX = 2, WEAPONIDX = 3, ACCESSORYIDX = 4;
    
    public List<Item> GetItem() { return _itemList; }

    [SerializeField] private List<Item> _itemList = new List<Item>();
    [SerializeField] private Item[] _items;

    private void Awake()
    {
        _weaponSlots = _slotParent.GetComponentsInChildren<WeaponSlot>();
        Save_Load.Instance.LoadEquipData();
    }

    public void LoadToEquip(int itemId)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i]._itemID == itemId)
                SetWeaponSlotImage(_items[i]);              
        }
    }

    public void SetWeaponSlotImage(Item item)
    {
        if (item._itemType == Item.eItemType.Weapon)
        {                        
            if (item._weaponType == "SWORD")
                _weaponSlots[WEAPONIDX].SetSlot(item);
            else if (item._weaponType == "LONGSWORD")
                _weaponSlots[WEAPONIDX].SetSlot(item);

            if (_itemList.Count > 0 &&_itemList.Find(e => e._itemType == item._itemType))
            {
                Item outItem = _itemList.Find(e => e._itemType == item._itemType);
                _itemList.Remove(outItem);
            }

            _itemList.Add(item);
        }
        else if (item._itemType == Item.eItemType.Armor)
        {
            if (item._weaponType == HELM)
                _weaponSlots[HELMIDX].SetSlot(item);
            else if (item._weaponType == CHEST)
                _weaponSlots[CHESTIDX].SetSlot(item);
            else if (item._weaponType == SHOES)
                _weaponSlots[SHOESIDX].SetSlot(item);
            else if (item._weaponType == ACCESSORY)
                _weaponSlots[ACCESSORYIDX].SetSlot(item);

            if (_itemList.Count > 0 && _itemList.Find(e => e._weaponType == item._weaponType))
            {
                Item outItem = _itemList.Find(e => e._itemType == item._itemType);
                _itemList.Remove(outItem);
            }

            _itemList.Add(item);
        }
    }
}
