using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoaderScript : MonoBehaviour
{
    [SerializeField] private int sceneIndexToLoad = 0;
    [SerializeField] private bool isReload;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            try
            {
                if (isReload)
                {
                    SceneManager.LoadScene(
                        PlayerHandler.PlayerInstance.GetComponent<CheckpointScript>().ReloadCheckpoint()
                        );
                }
                else
                {
                    SceneManager.LoadScene(sceneIndexToLoad);
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
