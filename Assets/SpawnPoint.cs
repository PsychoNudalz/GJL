using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private bool SaveCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        MovePlayerToSpawnPoint();
        if (SaveCheckpoint)
        {
            PlayerHandler.PlayerInstance.GetComponent<CheckpointScript>().SaveCheckpoint();
        }
    }

    private void MovePlayerToSpawnPoint()
    {
        GameObject player = PlayerHandler.PlayerInstance;
        //print($"Moving {player.name}");
        player.GetComponent<PlayerHandler>().SetPlayerPosition(transform.position);
        
        
    }
}
