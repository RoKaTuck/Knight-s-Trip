using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public int _questID; // ����Ʈ ���̵�
    public string _questName; // ����Ʈ �̸�    
    public string _questDescription; // ����Ʈ ����
    public int _questReward; // ����
    public string _questConditionString;
    public int _questConditionCurCount = 0; // ���� ����Ʈ ���� Ȥ�� ��� �� 
    public int _questConditionMax; // ����Ʈ ���� Ȥ�� ��� �ִ� ��
    public bool _isCompleted; // ����Ʈ �Ϸ� ����

}

public partial class Save_Load 
{
    private QuestData _questData = new QuestData();

    private string SAVE_QUESTNAME = "QuestSave.txt";
    private string INIT_QUESTNAME = "QuestSave_Original.txt";

    public void InitializeQuest()
    {
        string initJsonQuestData = File.ReadAllText(INIT_DATA_DIRECTORY + INIT_QUESTNAME);
        _questData = JsonUtility.FromJson<QuestData>(initJsonQuestData);

        string jsonQuest = JsonUtility.ToJson(_questData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME, jsonQuest); // ��� + ���� �̸�
    }

    public void SaveQuestData()
    {
        _playerData._questDataList.Clear();

        foreach (Quest quest in QuestManager.Instance._quests)
        {
            _questData._questID = quest._questID;
            _questData._questName = quest._questName;
            _questData._questReward = quest._questReward;
            _questData._questDescription = quest._questDescription;
            _questData._questConditionMax = quest._questConditionMax;
            _questData._questConditionString = quest._questConditionString;
            _questData._questConditionCurCount = quest._questConditionCurCount;

            _playerData._questDataList.Add(_questData);
        }

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);
        string jsonQuest = JsonUtility.ToJson(_questData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_PLAYERDATA, jsonPlayer);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME, jsonQuest); // ��� + ���� �̸�
    }

    public void LoadQuestData()
    {
        string loadJsonQuest = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME);
        _questData = JsonUtility.FromJson<QuestData>(loadJsonQuest);

        QuestManager.Instance._quests.Clear();

        foreach (QuestData questData in _playerData._questDataList)
        {
            Quest quest = new Quest(questData._questID, questData._questName, questData._questDescription,
                                    questData._questReward, questData._questConditionString, questData._questConditionCurCount,
                                    questData._questConditionMax);
            quest._IsCompleted = questData._isCompleted;

            QuestManager.Instance._quests.Add(quest);
        }
    }
}
