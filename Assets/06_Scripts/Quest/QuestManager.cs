using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{       
    public Dictionary<string, Quest> _quests = new Dictionary<string, Quest>(); // ����Ʈ ��� / Ű �� : ����, ����Ʈ
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

    // ����Ʈ�� �����ϴ� �Լ�
    public void StartQuest(Quest quest)
    {
        if (!quest._IsCompleted)
        {
            Debug.Log("����Ʈ�� �����մϴ�: " + quest._questName);
            // ����Ʈ ���� ���� �ۼ�

        }
        else
        {
            Debug.Log("�̹� �Ϸ��� ����Ʈ�Դϴ�: " + quest._questName);
        }
    }

    // ����Ʈ�� �Ϸ��ϴ� �Լ�
    public void CompleteQuest(Quest quest)
    {
        if (!quest._IsCompleted)
        {
            quest._IsCompleted = true;
            CompleteRewardGold(quest._questReward);
            Debug.Log("����Ʈ�� �Ϸ��մϴ�: " + quest._questName);
            // ���� ���� �� ����Ʈ �Ϸ� �� ���� �ۼ�
        }
        else
        {
            Debug.Log("�̹� �Ϸ��� ����Ʈ�Դϴ�: " + quest._questName);
        }
    }

    public void AddQuestPanel(Quest quest, GameObject questPrefab, GameObject questParent)
    {
        GameObject questTxt = Instantiate(questPrefab, Vector3.zero, Quaternion.identity, questParent.transform); 
        questTxt.GetComponent<TextMeshProUGUI>().text = $"���� : {quest._questName} \n ���� : {quest._questConditionString} {quest._questConditionCurCount} / {quest._questConditionMax}";
    }

    public void AddSideQuest(string questName, TextMeshProUGUI title, TextMeshProUGUI content)
    {
        title.text = _quests[questName]._questName;
        content.text = $"���� : {_quests[questName]._questConditionString} {_quests[questName]._questConditionCurCount} / {_quests[questName]._questConditionMax}";
    }

    public void CompleteRewardGold(int gold)
    {
        GameManager.Instance._Gold += gold;
    }
}
