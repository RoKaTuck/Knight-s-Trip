using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static bool _isUiActivated = false;

    [SerializeField, Header("UiManager Attribute")]
    private GameObject _cemetryDugeonObj;

    // 필요한 컴포넌트
    [SerializeField]
    private Teleport _telePort;

    private GameObject _curShowDungeonUi;

    public void ShowCemetryDungeonUI()
    {
        _isUiActivated = true;
        _curShowDungeonUi = _cemetryDugeonObj;
        _cemetryDugeonObj.SetActive(true);
    }

    public void CloseDugeonUI()
    {
        _isUiActivated = false;
        _curShowDungeonUi.SetActive(false);
    }    
}
