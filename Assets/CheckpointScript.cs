using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour
{
    private List<Tools> lastItemCheckpoint;
    private int lastSceneIndexCheckpoint;

    private PlayerInventory inventoryScript;
    // Start is called before the first frame update
    void Start()
    {
        inventoryScript = GetComponent<PlayerInventory>();
        lastItemCheckpoint = new List<Tools>();
    }
    
    private void SaveInventoryCheckpoint()
    {
        if(inventoryScript == null){return;}
        lastItemCheckpoint = new List<Tools>(inventoryScript.Items);
    }

    private void ReloadInventoryCheckpoint()
    {
        inventoryScript.Items = new List<Tools>();
        foreach (Tools tool in lastItemCheckpoint)
        {
            inventoryScript.AddItem(tool);
        }
    }

    public void SaveCheckpoint()
    {
        SaveInventoryCheckpoint();
        lastSceneIndexCheckpoint = SceneManager.GetActiveScene().buildIndex;
    }

    public int ReloadCheckpoint()
    {
        ReloadInventoryCheckpoint();
        return lastSceneIndexCheckpoint;
    }
}
