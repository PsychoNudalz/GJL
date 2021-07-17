using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player")]
    [SerializeField] PlayerControllerScript playerControllerScript;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] PlayerVolumnController playerVolumnController;
    [SerializeField] PlayerLifeSystemScript lifeSystemScript;

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
            UI_Handler instanceUIHandler = PlayerInstance.GetComponent<UI_Handler>();
            if (instanceUIHandler == null)
            {
                instanceUIHandler = FindObjectOfType<UI_Handler>();
            }
            Destroy(gameObject);
        }
        else
        {
            gameObject.name = "Player instance";
            PlayerInstance = gameObject;
            DontDestroyOnLoad(gameObject);
            Initialise();
        }
    }

    private void Initialise()
    {
        playerControllerScript = GetComponent<PlayerControllerScript>();
        playerInventory = GetComponent<PlayerInventory>();
        playerControllerScript.PlayerInventory = playerInventory;

        if (!uI_Handler)
        {
            uI_Handler = FindObjectOfType<UI_Handler>();
        }
        if (!playerVolumnController)
        {
            playerVolumnController = GetComponent<PlayerVolumnController>();
        }
        if (!lifeSystemScript)
        {
            lifeSystemScript = GetComponent<PlayerLifeSystemScript>();
        }
        lifeSystemScript.PlayerVolumnController = playerVolumnController;
        playerInventory.UI_Inventory = UI_Handler.UI_Inventory;
        if (GetDeathString().Equals(""))
        {
            SetDeathString("You died!");
        }
    }

    public void SetPlayerPosition(Vector3 newPosition, Quaternion newRotation)
    {
        print($"Moving Player to {newPosition}");
        GetComponent<CharacterController>().enabled = false;
        PlayerInstance.transform.SetPositionAndRotation(newPosition, newRotation);
        GetComponent<CharacterController>().enabled = true;
    }

    public void ResetPlayer()
    {
        lifeSystemScript.ResetSystem();
    }
    public string SetDeathString(string s)
    {
        Debug.Log("set death string to: " + s);
        lifeSystemScript.DeathString = s;
        return s;
    }
    public string GetDeathString()
    {
        return lifeSystemScript.DeathString ;
    }

}
