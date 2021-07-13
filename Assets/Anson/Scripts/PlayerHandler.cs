using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player")]
    [SerializeField] PlayerControllerScript playerControllerScript;
    [SerializeField] PlayerInventory playerInventory;

    [Header("UI")]
    [SerializeField] UI_Handler uI_Handler;

    public static GameObject PlayerInstance;

    public PlayerControllerScript PlayerControllerScript { get => playerControllerScript;}
    public PlayerInventory PlayerInventory { get => playerInventory;}
    public UI_Handler UI_Handler { get => uI_Handler;}

    void Awake()
    {
        if(PlayerInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            PlayerInstance = gameObject;
            DontDestroyOnLoad(gameObject);
            playerControllerScript = GetComponent<PlayerControllerScript>();
            playerInventory = GetComponent<PlayerInventory>();
            playerControllerScript.PlayerInventory = playerInventory;

            if (!uI_Handler)
            {
                uI_Handler = FindObjectOfType<UI_Handler>();
            }
            playerInventory.UI_Inventory = UI_Handler.UI_Inventory;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
