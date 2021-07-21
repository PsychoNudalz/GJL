using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject firstButton;
    private bool isPaused;

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            PlayerHandler.handler.InputLock(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButton);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            pausePanel.SetActive(false);
            PlayerHandler.handler.InputLock(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ReloadButton()
    {
        CheckpointScript checkpoint = PlayerHandler.PlayerInstance.GetComponent<CheckpointScript>();
        if (checkpoint != null)
        {
            pausePanel.SetActive(false);
            PlayerHandler.handler.ResetPlayer();
            int sceneId = checkpoint.ReloadCheckpoint();
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene(sceneId);
        }
    }

    public void MainMenuButton()
    {
        pausePanel.SetActive(false);
        PlayerHandler.PlayerInstance.GetComponent<PlayerHandler>().ResetPlayer();
        Destroy(PlayerHandler.PlayerInstance.gameObject);
        SceneManager.LoadScene(0);
    }
}
