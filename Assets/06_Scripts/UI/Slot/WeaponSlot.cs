using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Slot Attribute")]
    public Item _item; // 획득한 아이템

    [SerializeField]
    private Image _itemImage; // 아이템의 이미지

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

    // 마우스가 슬롯에서 빠져나갈 때 작동.
    public void OnPointerExit(PointerEventData eventData)
    {
        _itemEffectDatabase.HideToolTip();
    }
}
