using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _townPortal;    
    [SerializeField]
    private TextMeshProUGUI _sideQuestCondition;

    public int _questId;
    public int _mosnterCount;    
    public bool _dungeonClear = false;

    public int MonsterCount { get { return _mosnterCount; } set { _mosnterCount = value; } }    
    public bool DungeonClear { get { return _dungeonClear; } set { _dungeonClear = value; } }

    #region Singleton
    private static DungeonManager _instance;
    
    public static DungeonManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }
    #endregion

    private void Start()
    {
        GameManager.Instance._IsDungeon = true;
    }  

    public void UpdateSideQuest()
    {
        QuestManager.Instance.UpdateQuestProgress(_questId, _sideQuestCondition);
    }

    public void ActivePotal()
    {
        if(DungeonClear == true)
        {            
            _townPortal.SetActive(true);
            DungeonClear = false;
        }
    }
}
