using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    void Awake()
    {
        if (PlayerHandler.PlayerInstance != null)
        {
            //Destroy(PlayerHandler.PlayerInstance.gameObject);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayButton()
    {
        PlayerPrefs.SetInt("AlreadyPlayed",1);
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit(0);
    }
}
