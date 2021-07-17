using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterExitTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent enterEvent;
    [SerializeField] private UnityEvent exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        enterEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        exitEvent.Invoke();
    }



}
