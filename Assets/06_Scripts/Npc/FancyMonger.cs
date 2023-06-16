using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyMonger : NpcCtrl
{    
    [SerializeField, Header("FancyMonger Attribute")]
    private GameObject _buyPanel;
    [SerializeField]
    private GameObject _talkPanel;

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject _inventory;

    public void OnClickBuyBtn()
    {
        _talkPanel.SetActive(false);
        _buyPanel.SetActive(true);
    }

    public void OnClickBuyCancelBtn()
    {
        _buyPanel.SetActive(false);
        _talkPanel.SetActive(true);
        _inventory.SetActive(false);
    }

    public void OnClickSellBtn()
    {        
        _talkPanel.SetActive(false);
        _inventory.SetActive(true);
        _buyPanel.SetActive(true);
    }   
}
