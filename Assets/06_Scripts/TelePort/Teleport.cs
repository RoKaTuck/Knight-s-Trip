using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CemetryTeleport : BaseTeleport
{
    private string _sceneName = "Load_CemetryPage";

    public override void TransferDestination()
    {        
        SceneManager.LoadSceneAsync(_sceneName);
    }
}

public class TownTeleport : BaseTeleport
{
    private string _sceneName = "Load_GamePage";

    public override void TransferDestination()
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }
}

public class BossStageTeleport : BaseTeleport
{
    private string _sceneName = "Load_BossPage";

    public override void TransferDestination()
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }
}

public class Teleport : MonoBehaviour
{
   public enum eTeleportType
    {
        Town,
        Cemetry,
        BossStage
    }

    public eTeleportType _teleportType = eTeleportType.Cemetry;

    [SerializeField]
    private bool _isDungeon;

    // 필요한 컴포넌트
    [SerializeField]
    EntranceDungeonUi _uiManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _isDungeon == false)
        {
            switch(_teleportType)
            {
                case eTeleportType.Cemetry:
                    _uiManager.ShowCemetryDungeonUI();
                    break;
                case eTeleportType.BossStage:
                    _uiManager.ShowBossStageDungeonUI();
                    break;
            }            
        }
        else if (other.CompareTag("Player") && _isDungeon == true)
            Transfer();
    }

    public void Transfer()
    {
        BaseTeleport baseTeleport;

        EntranceDungeonUi._isUiActivated = false;

        switch(_teleportType)
        {
            case eTeleportType.Town:                
                baseTeleport = new TownTeleport();
                Save_Load.Instance.SavePlayerStatData();
                Save_Load.Instance.SaveInventoryData();
                Save_Load.Instance.SaveQuestData();                
                GameManager.Instance._IsDungeon = false;
                baseTeleport.TransferDestination();
                break;
            case eTeleportType.Cemetry:                
                baseTeleport = new CemetryTeleport();
                Save_Load.Instance.SaveData();
                baseTeleport.TransferDestination();
                break;
            case eTeleportType.BossStage:
                baseTeleport = new BossStageTeleport();
                Save_Load.Instance.SaveData();
                baseTeleport.TransferDestination();
                break;
        }        
    }
}
