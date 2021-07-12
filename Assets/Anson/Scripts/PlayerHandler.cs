using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerControllerScript playerControllerScript;
    [SerializeField] PlayerInteractionScript playerInteractionScript;
    void Awake()
    {
        playerControllerScript = GetComponent<PlayerControllerScript>();
        playerInteractionScript = GetComponent<PlayerInteractionScript>();
        playerControllerScript.PlayerInteractionScript = playerInteractionScript;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
