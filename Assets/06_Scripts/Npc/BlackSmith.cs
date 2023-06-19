using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmith : NpcCtrl
{
    [SerializeField, Header("BlackSmith Attribute")]
    private GameObject _talkPanel;
    [SerializeField]
    private GameObject _reinforcePanel;
    [SerializeField]
    private GameObject _createPanel;
    [SerializeField]
    private GameObject _devidePanel;

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject _inventory;
    [SerializeField]
    private Button _ownInventoryCancelBtn;

    public void OnClickReinforceBtn()
    {
        Inventory._inventoryActivated = true;
        _inventory.SetActive(true);
        _talkPanel.SetActive(false);
        _reinforcePanel.SetActive(true);
    }

    public void OnClickReinforceCancelBtn()
    {
        Inventory._inventoryActivated = false;
        _inventory.SetActive(false);
        _reinforcePanel.SetActive(false);
        _talkPanel.SetActive(true);
    }

    public void OnClickCreateBtn()
    {
        _talkPanel.SetActive(false);
        _createPanel.SetActive(true);
    }

    public void OnClickCreateCancelBtn()
    {
        _createPanel.SetActive(false);
        _talkPanel.SetActive(true);
    }

    public void OnClickDevideBtn()
    {
        Inventory._inventoryActivated = true;
        _inventory.SetActive(true);
        _talkPanel.SetActive(false);
        _devidePanel.SetActive(true);
    }

    public void OnClickDevideCancelBtn()
    {
        Inventory._inventoryActivated = false;
        _inventory.SetActive(false);
        _devidePanel.SetActive(false);
        _talkPanel.SetActive(true);
    }
}
