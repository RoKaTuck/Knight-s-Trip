using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReinforceSlot : MonoBehaviour, IDropHandler
{
    [Header("ReinforceSlot Attribute")]
    public Item _item; // ȹ���� ������    
    [SerializeField]
    Image _itemImage; // �������� �̹���

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Inventory _inventory;

    public void OnClickReinforceBtn()
    {
        if(_item != null)
        {
            
        }
    }

    public void AddItem(Item item)
    {
        _item = item;        
        _itemImage.sprite = item._itemImage;        

        SetColor(1);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance._dragSlot != null)
            ChageSlot();
    }

    private void SetColor(float alpha)
    {
        Color color = _itemImage.color;
        color.a = alpha;
        _itemImage.color = color;
    }

    private void ChageSlot()
    {
        Item _tempItem = _item;        

        AddItem(DragSlot.instance._dragSlot._item);

        if (_tempItem != null)
            DragSlot.instance._dragSlot.AddItem(_tempItem);
        
    }
}
