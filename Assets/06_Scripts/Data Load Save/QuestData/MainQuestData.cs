using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public int _questID; // 퀘스트 아이디
    public string _questName; // 퀘스트 이름    
    public string _questDescription; // 퀘스트 설명
    public int _questReward; // 보상
    public string _questConditionString;
    public int _questConditionCurCount = 0; // 현재 퀘스트 수집 혹은 사냥 수 
    public int _questConditionMax; // 퀘스트 수집 혹은 사냥 최대 수
    public bool _isCompleted; // 퀘스트 완료 여부
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

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME, jsonQuest); // 경로 + 파일 이름

        Debug.Log("초기화 완료");
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
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_QUESTNAME, jsonQuest); // 경로 + 파일 이름

        Debug.Log("퀘스트 저장");
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

            Debug.Log("퀘스트 로드 성공");
        }
    }
}
