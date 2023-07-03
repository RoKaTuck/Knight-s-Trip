using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static bool _inventoryActivated = false;    

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject _inventoryBase;
    [SerializeField]
    private GameObject _slotsParent;
    [SerializeField]
    private TextMeshProUGUI _ownGold;

    // 슬롯들
    private Slot[] _slots;

    public Slot[] GetSlot() { return _slots; }

    [SerializeField] private Item[] _items;

    public void LoadToInven(int arrayNum, string itemName, int itemNum)
    {
        for(int i = 0; i < _items.Length; i++)
        {
            if (_items[i]._itemName == itemName)
                _slots[arrayNum].AddItem(_items[i], itemNum);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _slots = _slotsParent.GetComponentsInChildren<Slot>();
        Save_Load.Instance.LoadInventoryData();
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.Instance._Pause == false)
            TryOpenInventory();
        UpdateCurrentGold();        
    }    

    public void AcquireItem(Item item, int count = 1)
    {
        if (Item.eItemType.Weapon != item._itemType && Item.eItemType.Armor != item._itemType)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i]._item != null)
                {
                    if (_slots[i]._item._itemName == item._itemName)
                    {
                        _slots[i].SetSlotCount(count);
                        return;
                    }
                }
            }
        }                

        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i]._item == null)
            {
                _slots[i].AddItem(item, count);
                return;
            }
        }
    }

    private void UpdateCurrentGold()
    {
        _ownGold.text = "현재 금액 : " + GameManager.Instance._Gold + "Gold";
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            _inventoryActivated = !_inventoryActivated;

            if (_inventoryActivated == true)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        _inventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        _inventoryBase?.SetActive(false);
    }
}
