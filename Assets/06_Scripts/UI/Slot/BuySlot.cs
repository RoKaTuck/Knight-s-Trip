using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Header("BuySlot Attribute")]
    public Item _item;
    [SerializeField]
    Image _itemImage; // �������� �̹���

    // �ʿ��� ������Ʈ
    [SerializeField]
    Inventory _inventory;


    private void Start()
    {
        _itemImage = transform.GetChild(0).GetComponent<Image>();
        if (_item != null)
        {
            _itemImage.sprite = _item._itemImage;
            SetColor(1);
        }
    }

    private void BuyItem()
    {
        _inventory.AcquireItem(_item);
        GameManager.Instance._Gold -= _item._buyPrice;
        Debug.Log(_item._itemName + "�� �����ϼ̽��ϴ�!");
    }

    private void SetColor(float alpha)
    {
        Color color = _itemImage.color;
        color.a = alpha;
        _itemImage.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (_item != null )
            {
                BuyItem();
            }
        }
    }
}
