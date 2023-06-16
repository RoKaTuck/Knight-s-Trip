using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcCtrl : MonoBehaviour
{
    public static bool _isInteracting = false;

    [SerializeField, Header("Npc Attribute")]
    protected GameObject _ownInteractionUi;
    [SerializeField]
    private GameObject _uiParent;

    public void ShowInteractionUi()
    {
        _ownInteractionUi.SetActive(true);
        _isInteracting = true;
    }

    public void CloseInteractionUi()
    {
        _ownInteractionUi.SetActive(false);
        _isInteracting = false;
    }
}
