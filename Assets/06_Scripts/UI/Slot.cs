using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{    
    [Header("Slot Attribute")]
    public Item _item; // ȹ���� ������
    [SerializeField]
    int _itemCount; // ȹ�尣 ������ ����
    [SerializeField]
    Image _itemImage; // �������� �̹���

    // �ʿ��� ������Ʈ 
    [SerializeField]
    private TMPro.TextMeshProUGUI _textCount;
    [SerializeField]
    private GameObject _CountImage;
    private WeaponManager _weaponManager;

    private void Start()
    {        
        _weaponManager = FindObjectOfType<WeaponManager>();
    }

    // ������ ȹ��
    public void AddItem(Item item, int count = 1)
    {
        _item = item;        
        _itemCount = count;
        _itemImage.sprite = item._itemImage;

        if(item._itemType != Item.eItemType.Equipment)
        {
            _CountImage.SetActive(true);
            _textCount.text = count.ToString();
        }
        else
        {
            _textCount.text = "0";
            _CountImage.SetActive(false);
        }

        SetColor(1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(_item != null)
            {
                if(_item._itemType == Item.eItemType.Equipment)
                {
                    // ����
                    StartCoroutine(_weaponManager.CRT_ChangeWeapon(_item._weaponType, _item._itemName));
                }
                else
                {
                    // �Ҹ�
                    Debug.Log(_item._itemName + " �� ����߽��ϴ�.");
                    SetSlotCount(-1);
                }
            }
        }
    }

    // ������ ���� ����
    public void SetSlotCount(int count)
    {
        _itemCount += count;
        _textCount.text = _itemCount.ToString();

        if(_itemCount <= 0)
            ClearSlot();
    }

    // ������ �ʱ�ȭ
    private void ClearSlot()
    {
        _item = null;
        _itemCount = 0;
        _itemImage.sprite = null;
        SetColor(0);

        _textCount.text = "0";
        _CountImage.SetActive(false);
    }

    // �̹����� ���� ����
    private void SetColor(float alpha)
    {
        Color color = _itemImage.color;
        color.a = alpha;
        _itemImage.color = color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        if (_item != null)
        {
            DragSlot.instance._dragSlot = this;
            DragSlot.instance.DragSetImage(_itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_item != null)
            DragSlot.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance._dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance._dragSlot != null)
            ChageSlot();
    }

    private void ChageSlot() 
    {
        Item _tempItem = _item;
        int _tempItemCount = _itemCount;

        AddItem(DragSlot.instance._dragSlot._item, DragSlot.instance._dragSlot._itemCount);

        if(_tempItem != null)
            DragSlot.instance._dragSlot.AddItem(_tempItem, _tempItemCount);            
        else
            DragSlot.instance._dragSlot.ClearSlot();
    }
}
