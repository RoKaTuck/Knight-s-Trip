using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questName; // ����Ʈ �̸�
    [TextArea]
    public string questDescription; // ����Ʈ ����
    public int questReward; // ����
    public bool IsCompleted; // ����Ʈ �Ϸ� ����

    // ����Ʈ ������
    public Quest(string name, string description, int reward)
    {
        questName = name;
        questDescription = description;
        questReward = reward;
        IsCompleted = false;
    }
}
