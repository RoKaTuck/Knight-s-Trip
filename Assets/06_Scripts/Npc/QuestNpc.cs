using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNpc : NpcCtrl
{
    [SerializeField]
    private TextMeshProUGUI _descriptionText;

    public Quest[] quest; // NPC�� �ִ� ����Ʈ
    public QuestManager questManager; // ����Ʈ ������ ��ũ��Ʈ ����

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
            // �÷��̾�� �浹�� ������ ���� ���� �ۼ�
        }
    }

    public override void ShowInteractionUi()
    {
        base.ShowInteractionUi();
        _descriptionText.text = quest[_currentQuestIdx].questDescription;       
    }    
}
