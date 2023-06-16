using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CemetryTeleport : BaseTeleport
{
    private string _sceneName = "Load_CemetryPage";

    public override void TransferDungeon()
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }
}


public class Teleport : MonoBehaviour
{
   public enum eTeleportType
    {
        Cemetry
    }

    public eTeleportType _teleportType = eTeleportType.Cemetry;

    // 필요한 컴포넌트
    [SerializeField]
    UiManager _uiManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _uiManager.ShowCemetryDungeonUI();
        }
    }

    public void Transfer()
    {
        BaseTeleport baseTeleport;

        UiManager._isUiActivated = false;

        switch(_teleportType)
        {
            case eTeleportType.Cemetry:
                baseTeleport = new CemetryTeleport();
                baseTeleport.TransferDungeon();
                break;
        }        
    }
}
