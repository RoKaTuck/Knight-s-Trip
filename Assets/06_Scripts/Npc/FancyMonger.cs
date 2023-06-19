using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FancyMonger : NpcCtrl
{    
    [SerializeField, Header("FancyMonger Attribute")]
    private GameObject _buyPanel;
    [SerializeField]
    private GameObject _talkPanel;

    public bool _selling = false;
    public bool _Selling { get { return _selling; } set { _selling = value; } }

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject _inventory;
    [SerializeField]
    private Button _ownInventoryCancelBtn;

    public void OnClickBuyBtn()
    {        
        _talkPanel.SetActive(false);
        _buyPanel.SetActive(true);
    }

    public void OnClickBuyCancelBtn()
    {        
        Inventory._inventoryActivated = false;
        _buyPanel.SetActive(false);
        _talkPanel.SetActive(true);
        _inventory.SetActive(false);
    }

    public void OnClickSellBtn()
    {
        _Selling = true;
        _ownInventoryCancelBtn.gameObject.SetActive(true);
        _talkPanel.SetActive(false);
        _inventory.SetActive(true);        
    }   

    public void OnClickSellCancelBtn()
    {
        Inventory._inventoryActivated = false;
        _ownInventoryCancelBtn.gameObject.SetActive(false);
        _talkPanel.SetActive(true);
        _inventory.SetActive(false);
    }
}
