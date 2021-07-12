using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObjectScript : MonoBehaviour
{
    [SerializeField] private UnityEvent interactEvent;
    
    public void Interact()
    {
        interactEvent.Invoke();
    }
}
