using System.Collections;
using System.Collections.Generic;
using System.IO;
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

public class MainQuestData : MonoBehaviour
{
    private QuestData _questData = new QuestData();
    private PlayerData _playerData = new PlayerData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "SaveFile.txt";
    private string SAVE_QUESTNAME = "QuestSave.txt";

    private void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }
    }

    public void Initialize()
    {
        _questData._isCompleted = false;
        _questData._questConditionCurCount = 0;
        _questData._questConditionMax = 0;
        _questData._questConditionString = string.Empty;
        _questData._questDescription = string.Empty;
        _questData._questID = 0;
        _questData._questName = string.Empty;
        _questData._questReward = 0;

        string jsonQuest = JsonUtility.ToJson(_questData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME, jsonQuest); // ��� + ���� �̸�

        Debug.Log("�ʱ�ȭ �Ϸ�");
    }

    public void SaveQuestData()
    {
        _playerData._questDataList.Clear();

        foreach (Quest quest in QuestManager.Instance._quests)
        {
            _questData._questID = quest._questID;
            _questData._questName = quest._questName;
            _questData._questDescription = quest._questDescription;
            _questData._questConditionCurCount = quest._questConditionCurCount;
            _questData._questConditionMax = quest._questConditionMax;
            _questData._questConditionString = quest._questConditionString;
            _questData._questReward = quest._questReward;

            _playerData._questDataList.Add(_questData);
        }

        string jsonPlayer = JsonUtility.ToJson(_playerData, true);
        string jsonQuest = JsonUtility.ToJson(_questData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, jsonPlayer);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME, jsonQuest); // ��� + ���� �̸�

        Debug.Log("����Ʈ ����");
    }

    public void LoadQuestData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME))
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

            Debug.Log("����Ʈ �ε� ����");
        }
    }
}
