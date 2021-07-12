using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObjectScript : MonoBehaviour
{
    [SerializeField] private List<InteractableEvent> interactEvents;

    [Header("Interaction Lock")]
    [SerializeField] bool lockAfterInteract = true; 
    [SerializeField] bool interactionLock;
    public bool Interact(Tools t)
    {
        if (interactionLock)
        {
            return false;
        }
        print($"Interacting with {gameObject.name}");
        UnityEvent e = GetEventByToolEnum(t);
        if (e != null)
        {
            e.Invoke();
            if (lockAfterInteract)
            {
                interactionLock = true;
            }
            return true;
        }
        else
        {
            Debug.Log("Invalid tool to use");
        }
        return false;
    }

    UnityEvent GetEventByToolEnum(Tools t)
    {
        foreach(InteractableEvent ie in interactEvents)
        {
            if (ie.Tool.Equals(t))
            {
                return ie.UEvent;
            }
        }
        return null;
    }
}
