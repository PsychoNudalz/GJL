using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerControllerScript playerControllerScript;
    [SerializeField] PlayerInventory playerInventory;
    void Awake()
    {
        playerControllerScript = GetComponent<PlayerControllerScript>();
        playerInventory = GetComponent<PlayerInventory>();
        playerControllerScript.PlayerInventory = playerInventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
