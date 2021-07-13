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

    [Header("Animation")]
    [SerializeField] Animator animator;

    private void Awake()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }
    public bool Interact(Tools t)
    {
        if (interactionLock)
        {
            return false;
        }
        print($"Interacting with {gameObject.name}");
        InteractableEvent e = GetEventByToolEnum(t);
        if (e != null)
        {
            e.InteractEvent.Invoke();
            animator.Play(e.InteractAnimation);

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

    public void Preview(Tools t)
    {
        if (interactionLock)
        {
            return;
        }
        InteractableEvent e = GetEventByToolEnum(t);
        if (e != null)
        {
            animator.Play(e.PreviewAnimation);

            return ;
        }
        else
        {
            Debug.Log("Invalid tool to use");
        }
        return ;
    }



    InteractableEvent GetEventByToolEnum(Tools t)
    {
        foreach(InteractableEvent ie in interactEvents)
        {
            if (ie.Tool.Equals(t))
            {
                return ie;
            }
        }
        return null;
    }

    public List<Tools> GetTools()
    {
        List<Tools> temp = new List<Tools>();
        foreach (InteractableEvent i in interactEvents)
        {
            temp.Add(i.Tool);
        }
        return temp;
    }
}
