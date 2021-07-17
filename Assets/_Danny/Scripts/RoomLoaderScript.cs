using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoaderScript : MonoBehaviour
{
    
    [SerializeField] private bool isReload;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            try
            {
                PlayerHandler.PlayerInstance.GetComponent<PlayerHandler>().ResetPlayer();
                if (isReload)
                {
                    SceneManager.LoadScene(
                        PlayerHandler.PlayerInstance.GetComponent<CheckpointScript>().ReloadCheckpoint()
                        );
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        GetComponent<Collider>().enabled = false;
    }
}
