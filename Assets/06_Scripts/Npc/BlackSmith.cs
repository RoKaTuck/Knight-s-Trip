using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void OnClickReinforceBtn()
    {
        _talkPanel.SetActive(false);
        _reinforcePanel.SetActive(true);
    }

    public void OnClickReinforceCancelBtn()
    {
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
        _talkPanel.SetActive(false);
        _devidePanel.SetActive(true);
    }

    public void OnClickDevideCancelBtn()
    {
        _devidePanel.SetActive(false);
        _talkPanel.SetActive(true);
    }
}
