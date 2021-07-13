using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoaderScript : MonoBehaviour
{
    [SerializeField] private int sceneIndexToLoad = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print($"Loading Next Room - {sceneIndexToLoad}");
            try
            {
                SceneManager.LoadScene(sceneIndexToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}
