using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(GetComponentInChildren<Button>().gameObject);
    }


    public void StopButton()
    {
        if (PlayerHandler.handler != null)
        {
            PlayerHandler.handler.ResetPlayer();
            Destroy(PlayerHandler.PlayerInstance.gameObject);
        }
        SceneManager.LoadScene(0);
    }
}
