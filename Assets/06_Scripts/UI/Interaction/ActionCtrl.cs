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


    // Npc

    private bool _canInteraction = false;

    private void Update()
    {
        if (NpcCtrl._isInteracting == false)
        {
            if (_pickUpActivated == false)
                CheckNpc();
            if (_canInteraction == false)
                CheckItem();

            TryAction();
        }
    }    

    private void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {            
            CheckItem();
            CanPickUp();
            CheckNpc();
            CanInteraction();
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

    private void CanInteraction()
    {
        if(_canInteraction == true)
        {
            if (_hitInfo.transform != null)
            {
                NpcCtrl npc = _hitInfo.transform.GetComponent<NpcCtrl>();
                npc.ShowInteractionUi();
                NpcInfoDisAppear();
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

    private void CheckNpc()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward * 2.5f), out _hitInfo, _range, 1 << 9))
        {
            if (_hitInfo.transform.CompareTag("Npc"))
            {
                NpcInfoAppear();
            }
        }
        else
            NpcInfoDisAppear();
    }

    private void NpcInfoAppear()
    {
        _canInteraction = true;
        _actionText.gameObject.SetActive(true);
        _actionText.text = "��ȣ�ۿ�" + "<color=yellow>" + "(E)" + "</color>";
    }

    private void NpcInfoDisAppear()
    {
        _canInteraction = false;
        _actionText.gameObject.SetActive(false);
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
