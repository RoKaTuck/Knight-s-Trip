using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNpc : NpcCtrl
{
    [SerializeField]
    private TextMeshProUGUI _descriptionText;

    public Quest[] quest; // NPC가 주는 퀘스트
    public QuestManager questManager; // 퀘스트 관리자 스크립트 참조

    private int _currentQuestIdx = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for(int i = 0; i < quest.Length; i++)
            {
                if(quest[i].IsCompleted == false)
                {
                    _currentQuestIdx = i;
                    break;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어와 충돌이 끝났을 때의 로직 작성
        }
    }

    public override void ShowInteractionUi()
    {
        base.ShowInteractionUi();
        _descriptionText.text = quest[_currentQuestIdx].questDescription;       
    }    
}
