using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>(); // 퀘스트 목록

    // 퀘스트를 시작하는 함수
    public void StartQuest(Quest quest)
    {
        if (!quest.IsCompleted)
        {
            Debug.Log("퀘스트를 시작합니다: " + quest.questName);
            // 퀘스트 시작 로직 작성
        }
        else
        {
            Debug.Log("이미 완료한 퀘스트입니다: " + quest.questName);
        }
    }

    // 퀘스트를 완료하는 함수
    public void CompleteQuest(Quest quest)
    {
        if (!quest.IsCompleted)
        {
            Debug.Log("퀘스트를 완료합니다: " + quest.questName);
            quest.IsCompleted = true;
            // 보상 지급 및 퀘스트 완료 후 로직 작성
        }
        else
        {
            Debug.Log("이미 완료한 퀘스트입니다: " + quest.questName);
        }
    }
}
