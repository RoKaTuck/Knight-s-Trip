using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestNpc : NpcCtrl
{
    [SerializeField]
    private TextMeshProUGUI _descriptionText;

    public Quest[] _quest; // NPC�� �ִ� ����Ʈ

    // �ʿ��� ������Ʈ
    [SerializeField]
    private GameObject _questPrefab;
    [SerializeField]
    private GameObject _questParent;
    [SerializeField]
    private TextMeshProUGUI _questSideTitleTxt;
    [SerializeField]
    private TextMeshProUGUI _questSideCondition;
    [SerializeField]
    private GameObject _consentRefuseBtn;
    [SerializeField]
    private GameObject _clearBtn;

    private int _currentQuestIdx = 0;  

    public void OnClickClearBtn()
    {
        if (QuestManager.Instance._quests[_quest[_currentQuestIdx]._questName]._questConditionCurCount == QuestManager.Instance._quests[_quest[_currentQuestIdx]._questName]._questConditionMax)
        {
            QuestManager.Instance.CompleteQuest(_quest[_currentQuestIdx]);
            _clearBtn.SetActive(false);
            _consentRefuseBtn.SetActive(true);
            CloseInteractionUi();
        }
        else
            Debug.Log("������ �ٽ� Ȯ���ϼ���");
    }

    public void QuestConsentBtn()
    {
        QuestManager.Instance.StartQuest(_quest[_currentQuestIdx]);
        QuestManager.Instance._questName.Add(_quest[_currentQuestIdx]._questName);
        QuestManager.Instance._quests.Add(_quest[_currentQuestIdx]._questName, _quest[_currentQuestIdx]);
        QuestManager.Instance.AddQuestPanel(_quest[_currentQuestIdx], _questPrefab, _questParent);
        QuestManager.Instance.AddSideQuest(_quest[_currentQuestIdx]._questName, _questSideTitleTxt, _questSideCondition);
        _consentRefuseBtn.SetActive(false);
        _clearBtn.SetActive(true);
        CloseInteractionUi();
    }

    public void QuestRefuseBtn()
    {
        CloseInteractionUi();
    }

    public override void ShowInteractionUi()
    {
        base.ShowInteractionUi();
        _descriptionText.text = _quest[_currentQuestIdx]._questDescription;       
    }    
}