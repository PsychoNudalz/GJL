using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
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
        if (!TryGetComponent(out ObjectHighlighter o))
        {
            gameObject.AddComponent<Outline>().enabled = false;
            gameObject.AddComponent<ObjectHighlighter>();
        }
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
            Debug.Log(name + " interact with " + t.ToString());
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
        if (animator)
        {
            animator.Play(e.InteractAnimation);
        }
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
            //Debug.Log("Invalid tool to use");
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

    public void FreeLock(float t)
    {
        StartCoroutine(DelayFreeLock(t));
    }

    IEnumerator DelayPlayEvent(InteractableEvent e)
    {
        yield return new WaitForSeconds(e.InteractDelay);
        e.InteractEvent.Invoke();

    }
    IEnumerator DelayFreeLock(float t)
    {
        yield return new WaitForSeconds(t);
        interactionLock = false;
    }

    public void GivePlayerImmunity(int damageType)
    {
        FindObjectOfType<PlayerLifeSystemScript>().AddImmunity((DamageType)damageType);
    }

    public void GivePlayerMask(int i)
    {
        PlayerHandler.handler.SetMask(i);
    }
    public void SetPlayerDeathString(string s)
    {
        PlayerHandler.handler.SetDeathString(s);
    }
    public void ProgressEnding(int ending)
    {
        FindObjectOfType<EndingHandler>().IncrementEnding(ending);
    }
}
