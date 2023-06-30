using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseBase;
    [SerializeField]
    private GameObject _dungeonPauseBase;

    private bool _isPauseActive = false;

    private void Update()
    {
        TryPause();
    }

    public void OnSaveBtn()
    {
        Save_Load.Instance.SaveData();
        ClosePause();
    }

    public void OnLoadBtn()
    {
        Time.timeScale = 1;

        if (File.Exists(Application.dataPath + "/Saves/" + "PlayerData.txt"))
            SceneManager.LoadScene("Load_GamePage");
    }

    public void OnExitBtn()
    {
        Time.timeScale = 1;
        Save_Load.Instance.SaveData();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnDungeonExitBtn()
    {
        Time.timeScale = 1;

        if (File.Exists(Application.dataPath + "/Saves/" + "PlayerData.txt"))
            SceneManager.LoadScene("Load_GamePage");
    }

    private void TryPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPauseActive = !_isPauseActive;

            if (_isPauseActive == true)
                OpenPause();
            else
                ClosePause();
        }
    }

    private void OpenPause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        GameManager.Instance._Pause = true;

        if (GameManager.Instance._IsDungeon == false)
            _pauseBase.SetActive(true);
        else
            _dungeonPauseBase.SetActive(true);
    }

    private void ClosePause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        GameManager.Instance._Pause = false;

        if (GameManager.Instance._IsDungeon == false)
            _pauseBase.SetActive(false);
        else
            _dungeonPauseBase.SetActive(false);
    }
}
