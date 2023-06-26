using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Slot Attribute")]
    public Item _item; // ȹ���� ������

    [SerializeField]
    private Image _itemImage; // �������� �̹���

    [SerializeField]
    private ItemEffectDatabase _itemEffectDatabase;

    public void SetSlot(Item item)
    {
        _item = item;
        _itemImage.sprite = item._itemImage;
        SetColor(1);
    }

    private void SetColor(float alpha)
    {
        Color color = _itemImage.color;
        color.a = alpha;
        _itemImage.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_item != null)
            _itemEffectDatabase.ShowToolTip(_item, transform.position);
    }

    // ���콺�� ���Կ��� �������� �� �۵�.
    public void OnPointerExit(PointerEventData eventData)
    {
        _itemEffectDatabase.HideToolTip();
    }
}
