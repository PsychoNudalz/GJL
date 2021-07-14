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
    public bool Interact(ToolType t)
    {
        if (interactionLock)
        {
            return false;
        }
        print($"Interacting with {gameObject.name}");
        InteractableEvent e = GetEventByToolEnum(t);
        if (e != null)
        {
            PlayEvent(e);

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

    private void PlayEvent(InteractableEvent e)
    {
        if (e.InteractDelay <= 0)
        {

            e.InteractEvent.Invoke();
        }
        else
        {
            StartCoroutine(DelayPlayEvent(e));
        }
        animator.Play(e.InteractAnimation);
    }

    public void Preview(ToolType t)
    {
        if (interactionLock)
        {
            return;
        }
        InteractableEvent e = GetEventByToolEnum(t);
        if (e != null)
        {
            animator.Play(e.PreviewAnimation);

            return;
        }
        else
        {
            Debug.Log("Invalid tool to use");
        }
        return;
    }



    InteractableEvent GetEventByToolEnum(ToolType t)
    {
        foreach (InteractableEvent ie in interactEvents)
        {
            if (ie.Tool.Equals(t))
            {
                return ie;
            }
        }
        return null;
    }

    public List<ToolType> GetTools()
    {
        List<ToolType> temp = new List<ToolType>();
        foreach (InteractableEvent i in interactEvents)
        {
            temp.Add(i.Tool);
        }
        return temp;
    }

    IEnumerator DelayPlayEvent(InteractableEvent e)
    {
        yield return new WaitForSeconds(e.InteractDelay);
        e.InteractEvent.Invoke();

    }
}
