using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private bool SaveCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        
        PlayerHandler.PlayerInstance.transform.position = transform.position;
        if (SaveCheckpoint)
        {
            PlayerHandler.PlayerInstance.GetComponent<CheckpointScript>().SaveCheckpoint();
        }
    }
    
}
