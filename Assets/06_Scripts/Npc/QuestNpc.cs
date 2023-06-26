using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestNpc : NpcCtrl
{
    [SerializeField]
    private TextMeshProUGUI _descriptionText;
    
    public List<Quest> _quest = new List<Quest>(); // NPC가 주는 퀘스트

    // 필요한 컴포넌트
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
        if (QuestManager.Instance._quests[_quest[_currentQuestIdx]._questID]._questConditionCurCount >= QuestManager.Instance._quests[_quest[_currentQuestIdx]._questID]._questConditionMax)
        {
            QuestManager.Instance.OutQuestPanel(_quest[_currentQuestIdx]._questID);
            QuestManager.Instance.ClearSideQuestText();
            QuestManager.Instance.CompleteQuest(_quest[_currentQuestIdx]);
            _quest.RemoveAt(0);    
            _clearBtn.SetActive(false);
            _consentRefuseBtn.SetActive(true);
            Save_Load.Instance.SaveTownQuestData();
            CloseInteractionUi();
        }
        else
            Debug.Log("조건을 다시 확인하세요");
    }

    public void QuestConsentBtn()
    {
        QuestManager.Instance.StartQuest(_quest[_currentQuestIdx]);
        QuestManager.Instance._questName.Add(_quest[_currentQuestIdx]._questName);
        QuestManager.Instance._quests.Add(_quest[_currentQuestIdx]);
        QuestManager.Instance.AddQuestPanel(_quest[_currentQuestIdx], _questPrefab, _questParent);
        QuestManager.Instance.AddSideQuest(_quest[_currentQuestIdx]._questID, _questSideTitleTxt, _questSideCondition);
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

        if (QuestManager.Instance._quests.Count > 0 && QuestManager.Instance._quests[_currentQuestIdx]._IsCompleted == false)
        {
            _consentRefuseBtn.SetActive(false);
            _clearBtn.SetActive(true);
        }
        else if(QuestManager.Instance._quests.Count == 0 || QuestManager.Instance._quests[_currentQuestIdx]._IsCompleted == false)
        {
            _consentRefuseBtn.SetActive(true);
            _clearBtn.SetActive(false);
        }
        
    }    
}
