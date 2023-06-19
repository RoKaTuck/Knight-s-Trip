using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    // 필요한 컴포넌트
    [SerializeField]
    private GameObject _goBase;

    [SerializeField]
    private TextMeshProUGUI _itemName;
    [SerializeField]
    private TextMeshProUGUI _itemDesc;
    [SerializeField]
    private TextMeshProUGUI _itemHowtoUsed;

    public void ShowToolTip(Item item, Vector3 pos)
    {
        _goBase.SetActive(true);
        pos += new Vector3(_goBase.GetComponent<RectTransform>().rect.width * 0.5f, - _goBase.GetComponent<RectTransform>().rect.height * 0.5f, 0f);
        _goBase.transform.position = pos;

        _itemName.text = item._itemName;
        _itemDesc.text = item._itemDesc;

        if (item._itemType == Item.eItemType.Weapon && item._itemType == Item.eItemType.Armor)
            _itemHowtoUsed.text = "우클릭 - 장착";
        else if (item._itemType == Item.eItemType.Used)
            _itemHowtoUsed.text = "우클릭 - 소모";
        else
            _itemHowtoUsed.text = "";
    }

    public void HideToolTip()
    {
        _goBase.SetActive(false);
    }
}
