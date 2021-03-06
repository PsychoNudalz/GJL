using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text deathText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject firstButton;

    public void ShowGameOverScreen(int delay)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Invoke(nameof(ShowGameOverAfterDelay),delay);
    }

    void ShowGameOverAfterDelay()
    {
        deathText.text = PlayerHandler.PlayerInstance.GetComponent<PlayerHandler>().GetDeathString();
        PlayerHandler.handler.InputLock(true);
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void ReloadButton()
    {
        CheckpointScript checkpoint = PlayerHandler.PlayerInstance.GetComponent<CheckpointScript>();
        if (checkpoint != null)
        {
            gameOverPanel.SetActive(false);
            PlayerHandler.handler.ResetPlayer();
            int sceneId = checkpoint.ReloadCheckpoint();
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene(sceneId);
        }
    }

    public void MainMenuButton()
    {
        gameOverPanel.SetActive(false);
        PlayerHandler.PlayerInstance.GetComponent<PlayerHandler>().ResetPlayer();
        Destroy(PlayerHandler.PlayerInstance.gameObject);
        SceneManager.LoadScene(0);
    }
}
