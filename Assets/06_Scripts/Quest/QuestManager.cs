using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{       
    public List<Quest> _quests = new List<Quest>();
    public List<string> _questName = new List<string>();

    private Dictionary<int, GameObject> _questBaseTextList = new Dictionary<int, GameObject>();
    private TextMeshProUGUI _questSideTitleText;
    private TextMeshProUGUI _questSideContentText;

    #region Singleton
    private static QuestManager instance;    

    public static QuestManager Instance
    {
        get 
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public void UpdateQuestProgress(int id, TextMeshProUGUI sideProgress)
    {
        Quest quest = _quests.Find(q => q._questID == id);

        if (quest != null)
        {
            sideProgress.text = quest._questConditionString + " " + quest._questConditionCurCount +
                                " / " + quest._questConditionMax;
        }
    }

    public void InceaseQuestCondition(int id, int count)
    {
        Quest quest = _quests.Find(q => q._questID == id);

        if(quest != null)
            quest._questConditionCurCount += count;
    }

    // 퀘스트를 시작하는 함수
    public void StartQuest(Quest quest)
    {
        if (!quest._IsCompleted)
        {
            Debug.Log("퀘스트를 시작합니다: " + quest._questName);
            // 퀘스트 시작 로직 작성

        }
        else
        {
            Debug.Log("이미 완료한 퀘스트입니다: " + quest._questName);
        }
    }

    // 퀘스트를 완료하는 함수
    public void CompleteQuest(Quest quest)
    {
        if (!quest._IsCompleted)
        {
            quest._IsCompleted = true;
            CompleteRewardGold(quest._questReward);
            Debug.Log("퀘스트를 완료합니다: " + quest._questName);
            // 보상 지급 및 퀘스트 완료 후 로직 작성

            _quests.Remove(_quests.Find(q => q._questID == 0));
        }
        else
        {
            Debug.Log("이미 완료한 퀘스트입니다: " + quest._questName);
        }
    }

    public void AddQuestPanel(Quest quest, GameObject questPrefab, GameObject questParent)
    {
        GameObject questTxt = Instantiate(questPrefab, Vector3.zero, Quaternion.identity, questParent.transform); 
        questTxt.GetComponent<TextMeshProUGUI>().text = $"제목 : {quest._questName} \n 조건 : {quest._questConditionString}";
        
        if(_questBaseTextList.ContainsKey(quest._questID) == false)
            _questBaseTextList.Add(quest._questID, questTxt);

        if(_questBaseTextList[quest._questID] == null)
        {
            _questBaseTextList.Remove(quest._questID);
            _questBaseTextList.Add(quest._questID, questTxt);
        }

    }

    public void AddSideQuest(int questId, TextMeshProUGUI title, TextMeshProUGUI content)
    {
        Quest quest = _quests.Find(q => q._questID == questId);

        title.text = quest._questName;
        content.text = $"조건 : {quest._questConditionString} {quest._questConditionCurCount} / {quest._questConditionMax}";
        _questSideTitleText = title;
        _questSideContentText = content;
    }

    public void OutQuestPanel(int id)
    {
        Destroy(_questBaseTextList[id]);
    }

    public void ClearSideQuestText()
    {
        _questSideTitleText.text = string.Empty;
        _questSideContentText.text = string.Empty;
    }

    public void CompleteRewardGold(int gold)
    {
        GameManager.Instance._Gold += gold;
    }    
}
