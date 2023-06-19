using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questName; // 퀘스트 이름
    [TextArea]
    public string questDescription; // 퀘스트 설명
    public int questReward; // 보상
    public bool IsCompleted; // 퀘스트 완료 여부

    // 퀘스트 생성자
    public Quest(string name, string description, int reward)
    {
        questName = name;
        questDescription = description;
        questReward = reward;
        IsCompleted = false;
    }
}
