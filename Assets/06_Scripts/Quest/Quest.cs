using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int _questID;
    public string _questName; // 퀘스트 이름
    [TextArea]
    public string _questDescription; // 퀘스트 설명
    public int _questReward; // 보상
    public string _questConditionString;
    public int _questConditionCurCount = 0; // 현재 퀘스트 수집 혹은 사냥 수 
    public int _questConditionMax; // 퀘스트 수집 혹은 사냥 최대 수
    public bool _IsCompleted; // 퀘스트 완료 여부

    // 퀘스트 생성자
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
