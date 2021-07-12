using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObjectScript : MonoBehaviour
{
    [SerializeField] private List<InteractableEvent> interactEvents;
    
    public void Interact()
    {
        print($"Interacting with {gameObject.name}");
        
    }
}
