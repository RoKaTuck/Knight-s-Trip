using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string _questName; // ����Ʈ �̸�
    [TextArea]
    public string _questDescription; // ����Ʈ ����
    public int _questReward; // ����
    public string _questConditionString;
    public int _questConditionCurCount = 0; // ���� ����Ʈ ���� Ȥ�� ��� �� 
    public int _questConditionMax; // ����Ʈ ���� Ȥ�� ��� �ִ� ��
    public bool _IsCompleted; // ����Ʈ �Ϸ� ����

    // ����Ʈ ������
    public Quest(string name, string description, int reward)
    {
        _questName = name;
        _questDescription = description;
        _questReward = reward;
        _IsCompleted = false;
    }
}
