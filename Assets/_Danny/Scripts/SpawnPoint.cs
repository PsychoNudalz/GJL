using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private bool SaveCheckpoint;

    // Start is called before the first frame update
    void Awake()
    {
        MovePlayerToSpawnPoint();
        PlayerHandler.PlayerInstance.GetComponent<FirstPersonController>().ResetGravity();
        if (SaveCheckpoint)
        {
            PlayerHandler.PlayerInstance.GetComponent<CheckpointScript>().SaveCheckpoint();
        }
    }

    private void MovePlayerToSpawnPoint()
    {
        GameObject player = PlayerHandler.PlayerInstance;
        player.GetComponent<PlayerHandler>().SetPlayerPosition(transform.position,transform.rotation);
        
        
    }
}
