using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCtrl : MonoBehaviour
{
    [SerializeField]
    private float _range; // ���� ������ �ִ� �Ÿ�

    private bool _pickUpActivated = false; // ���� ������ �� true

    private RaycastHit _hitInfo; // �浹ü ���� ����.

    // ������ ���̾�� �����ϵ��� ���̾� ����ũ�� ����.
    [SerializeField]
    private LayerMask _layerMask;

    // �ʿ��� ������Ʈ.
    [SerializeField]
    private TMPro.TextMeshProUGUI _actionText;
    [SerializeField]
    private Inventory _inventory;

    private void Update()
    {        
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if(_pickUpActivated == true)
        {
            if(_hitInfo.transform != null)
            {
                Debug.Log(_hitInfo.transform.GetComponent<ItemPickUp>()._item._itemName + " ȹ���߽��ϴ�.");
                _inventory.AcquireItem(_hitInfo.transform.GetComponent<ItemPickUp>()._item);
                Destroy(_hitInfo.transform.gameObject); // ���߿� ������Ʈ Ǯ ���� ���� ���
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hitInfo, _range, _layerMask))
        {
            if (_hitInfo.transform.CompareTag("Item"))
            {
                ItemInfoAppear();
            }
        }
        else
            InfoDisappear();
    }

    private void ItemInfoAppear()
    {
        _pickUpActivated = true;
        _actionText.gameObject.SetActive(true);
        _actionText.text = _hitInfo.transform.GetComponent<ItemPickUp>()._item._itemName + " ȹ�� " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void InfoDisappear()
    {
        _pickUpActivated = false;
        _actionText.gameObject.SetActive(false);
    }
}
