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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Invoke(nameof(ShowGameOverAfterDelay),delay);
    }

    void ShowGameOverAfterDelay()
    {
        deathText.text = PlayerHandler.PlayerInstance.GetComponent<PlayerHandler>().GetDeathString();
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
        PlayerHandler.PlayerInstance.GetComponent<PlayerHandler>().ResetPlayer();
        Destroy(PlayerHandler.PlayerInstance.gameObject);
        SceneManager.LoadScene(0);
    }
}
