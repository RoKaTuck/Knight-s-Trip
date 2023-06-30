using System.IO;
using UnityEngine;

public partial class Save_Load
{
    private TownQuestData _townQuestData = new TownQuestData();

    private string SAVE_TOWNQUESTNAME = "TownQuest.txt";
    private string INIT_TOWNQUESTNAME = "TownQuest_Original.txt";
    
    public void InitializeTownQuest()
    {
        string initJsonTownQuest = File.ReadAllText(INIT_DATA_DIRECTORY + INIT_TOWNQUESTNAME);
        _townQuestData = JsonUtility.FromJson<TownQuestData>(initJsonTownQuest);

        string jsonTown = JsonUtility.ToJson(_townQuestData, true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_TOWNQUESTNAME, jsonTown); // 경로 + 파일 이름
    }

    public void SaveTownQuestData()
    {
        QuestNpc questNpc = FindObjectOfType<QuestNpc>();

        _townQuestData._npcQuestData = questNpc._quest;

        string jsonTownQuest = JsonUtility.ToJson(_townQuestData, true);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_TOWNQUESTNAME, jsonTownQuest);
    }

    public void LoadTownQuestData()
    {
        string loadJsonTownQuest = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_TOWNQUESTNAME);
        _townQuestData = JsonUtility.FromJson<TownQuestData>(loadJsonTownQuest);

        QuestNpc questNpc = FindObjectOfType<QuestNpc>();
        questNpc._quest = _townQuestData._npcQuestData;
    }

    public void LoadTownQuestOriginalData()
    {
        string loadJsonTownQuest = File.ReadAllText(INIT_DATA_DIRECTORY + INIT_TOWNQUESTNAME);
        _townQuestData = JsonUtility.FromJson<TownQuestData>(loadJsonTownQuest);
    }
}
