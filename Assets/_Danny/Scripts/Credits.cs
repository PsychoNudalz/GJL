using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private bool exitToMenu;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void StopButton()
    {
        print("Close credits");
        if(exitToMenu)
        {
            PlayerHandler.PlayerInstance.GetComponent<PlayerHandler>().ResetPlayer();
            Destroy(PlayerHandler.PlayerInstance.gameObject);
            print("Press to main menu");
            SceneManager.LoadScene(0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
