using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObjectScript : MonoBehaviour
{
    [SerializeField] private List<InteractableEvent> interactEvents;
    
    public void Interact(Tools t)
    {
        print($"Interacting with {gameObject.name}");
        UnityEvent e = GetEventByToolEnum(t);
        if (e != null)
        {
            e.Invoke();
        }
        else
        {
            Debug.LogError("Failed to envoke event");
        }
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
