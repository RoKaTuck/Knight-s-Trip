using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    public static bool _questUiActivated = false;

    [SerializeField, Header("PlayerQuest Attribute")]
    private GameObject _questBase;



    private void Update()
    {
        //if(GameManager.Instance._Pause == false)
            TryOpenQuest();
    }

    private void TryOpenQuest()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            _questUiActivated = !_questUiActivated;

            if (_questUiActivated == true)
                OpenQuestUi();
            else
                CloseQuestUi();
        }
    }

    private void OpenQuestUi()
    {
        _questBase.SetActive(true);
    }

    private void CloseQuestUi()
    {
        _questBase.SetActive(false);
    }
}
