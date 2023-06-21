using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int _questID;
    public string _questName; // ����Ʈ �̸�
    [TextArea]
    public string _questDescription; // ����Ʈ ����
    public int _questReward; // ����
    public string _questConditionString;
    public int _questConditionCurCount = 0; // ���� ����Ʈ ���� Ȥ�� ��� �� 
    public int _questConditionMax; // ����Ʈ ���� Ȥ�� ��� �ִ� ��
    public bool _IsCompleted; // ����Ʈ �Ϸ� ����

    // ����Ʈ ������
    public Quest(int questId, string name, string description, int reward, string questCondition, int curCount, int max)
    {
        _questID = questId;
        _questName = name;
        _questDescription = description;
        _questReward = reward;
        _questConditionString = questCondition;
        _questConditionCurCount = curCount;
        _questConditionMax = max;        
        _IsCompleted = false;
    }
}
