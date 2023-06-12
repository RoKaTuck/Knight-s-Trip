using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCtrl : MonoBehaviour
{
    [SerializeField]
    private float _range; // 습득 가능한 최대 거리

    private bool _pickUpActivated = false; // 습득 가능할 시 true

    private RaycastHit _hitInfo; // 충돌체 정보 저장.

    // 아이템 레이어에만 반응하도록 레이어 마스크를 설정.
    [SerializeField]
    private LayerMask _layerMask;

    // 필요한 컴포넌트.
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
                Debug.Log(_hitInfo.transform.GetComponent<ItemPickUp>()._item._itemName + " 획득했습니다.");
                _inventory.AcquireItem(_hitInfo.transform.GetComponent<ItemPickUp>()._item);
                Destroy(_hitInfo.transform.gameObject); // 나중에 오브젝트 풀 쓸지 말지 고민
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
        _actionText.text = _hitInfo.transform.GetComponent<ItemPickUp>()._item._itemName + " 획득 " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void InfoDisappear()
    {
        _pickUpActivated = false;
        _actionText.gameObject.SetActive(false);
    }
}
