using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{       
    public Dictionary<string, Quest> _quests = new Dictionary<string, Quest>(); // 퀘스트 목록 / 키 값 : 제목, 퀘스트
    public List<string> _questName = new List<string>();

    private static QuestManager instance;

    public static QuestManager Instance
    {
        get 
        {
            if (instance == null)
                instance = new QuestManager();
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
        }
        else
        {
            Debug.Log("이미 완료한 퀘스트입니다: " + quest._questName);
        }
    }

    public void AddQuestPanel(Quest quest, GameObject questPrefab, GameObject questParent)
    {
        GameObject questTxt = Instantiate(questPrefab, Vector3.zero, Quaternion.identity, questParent.transform); 
        questTxt.GetComponent<TextMeshProUGUI>().text = $"제목 : {quest._questName} \n 조건 : {quest._questConditionString} {quest._questConditionCurCount} / {quest._questConditionMax}";
    }

    public void AddSideQuest(string questName, TextMeshProUGUI title, TextMeshProUGUI content)
    {
        title.text = _quests[questName]._questName;
        content.text = $"조건 : {_quests[questName]._questConditionString} {_quests[questName]._questConditionCurCount} / {_quests[questName]._questConditionMax}";
    }

    public void CompleteRewardGold(int gold)
    {
        GameManager.Instance._Gold += gold;
    }
}
