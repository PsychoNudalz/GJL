using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text deathText;
    [SerializeField] private GameObject gameOverPanel;

    public void ShowGameOverScreen(int delay)
    {
        print($"Game over - delay {delay}");
        Invoke(nameof(ShowGameOverAfterDelay),delay);
    }

    void ShowGameOverAfterDelay()
    {
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReloadButton()
    {
        CheckpointScript checkpoint = PlayerHandler.PlayerInstance.GetComponent<CheckpointScript>();
        PlayerHandler handler = PlayerHandler.PlayerInstance.GetComponent<PlayerHandler>();
        if (checkpoint != null)
        {
            gameOverPanel.SetActive(false);
            handler.ResetPlayer();
            int sceneId = checkpoint.ReloadCheckpoint();
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene(sceneId);
        }
    }

    public void MainMenuButton()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
