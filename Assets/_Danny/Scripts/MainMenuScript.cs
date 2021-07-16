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
            Destroy(PlayerHandler.PlayerInstance.gameObject);
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
}
