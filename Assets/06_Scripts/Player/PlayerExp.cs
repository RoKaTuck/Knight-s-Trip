using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerExp : EXP
{
    // �ʿ��� ������Ʈ
    [SerializeField]
    private TextMeshProUGUI _levelTxt;
    [SerializeField]
    private StatusCtrl _statusCtrl;

    private void Start()
    {
        _levelTxt.text = "Lv " + GameManager.Instance._Level.ToString();
    }

    public override void IncreaseExp(int count)
    {
        GameManager.Instance.IncreaseExp(count);

        _levelTxt.text = "Lv " + GameManager.Instance._Level.ToString();
        _statusCtrl.UpdateExp();
    }

    public override void DecreaseExp(int count)
    {
        GameManager.Instance.DecreaseExp(count);
        _statusCtrl.UpdateExp();
    }
}
