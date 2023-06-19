using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    public static DragSlot instance;

    public Slot _dragSlot;

    [SerializeField]
    private Image _itemImage;

    private void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image itemImage)
    {
        _itemImage.sprite = itemImage.sprite;
        SetColor(1);

    }

    public void SetColor(float alpha)
    {
        Color color = _itemImage.color;
        color.a = alpha;
        _itemImage.color = color;
    }   
   
}
