using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonLoadData : MonoBehaviour
{
    [SerializeField, Header("Town Load Attribute")]
    private TextMeshProUGUI _sideQuestTitle;
    [SerializeField]
    private TextMeshProUGUI _sideQuestCondition;
    [SerializeField]
    private GameObject _questInstanceParent;
    [SerializeField]
    private GameObject _questPrefab;

    private void Start()
    {
        Save_Load.Instance.LoadPlayerStatData();
        Save_Load.Instance.LoadInventoryData();
        Save_Load.Instance.LoadEquipData();
        Save_Load.Instance.LoadQuestData();

        QuestUiUpdate();        
    }    

    private void QuestUiUpdate()
    {
        if (QuestManager.Instance._quests.Count > 0)
        {
            for (int i = 0; i < QuestManager.Instance._quests.Count; i++)
            {
                if (QuestManager.Instance._quests[i]._IsCompleted == false)
                {
                    _sideQuestTitle.text = QuestManager.Instance._quests[i]._questName;
                    _sideQuestCondition.text = QuestManager.Instance._quests[i]._questConditionString;
                    QuestManager.Instance.AddQuestPanel(QuestManager.Instance._quests[i],
                                                       _questPrefab, _questInstanceParent);

                    QuestManager.Instance.AddSideQuest(QuestManager.Instance._quests[i]._questID,
                                                      _sideQuestTitle, _sideQuestCondition);

                }
            }
        }
    }
}
