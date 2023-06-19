using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>(); // ����Ʈ ���

    // ����Ʈ�� �����ϴ� �Լ�
    public void StartQuest(Quest quest)
    {
        if (!quest.IsCompleted)
        {
            Debug.Log("����Ʈ�� �����մϴ�: " + quest.questName);
            // ����Ʈ ���� ���� �ۼ�
        }
        else
        {
            Debug.Log("�̹� �Ϸ��� ����Ʈ�Դϴ�: " + quest.questName);
        }
    }

    // ����Ʈ�� �Ϸ��ϴ� �Լ�
    public void CompleteQuest(Quest quest)
    {
        if (!quest.IsCompleted)
        {
            Debug.Log("����Ʈ�� �Ϸ��մϴ�: " + quest.questName);
            quest.IsCompleted = true;
            // ���� ���� �� ����Ʈ �Ϸ� �� ���� �ۼ�
        }
        else
        {
            Debug.Log("�̹� �Ϸ��� ����Ʈ�Դϴ�: " + quest.questName);
        }
    }
}
