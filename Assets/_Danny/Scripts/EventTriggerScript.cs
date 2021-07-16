using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerScript : MonoBehaviour
{
    [SerializeField] private UnityEvent[] eventsToTrigger;
    [SerializeField] private UnityEvent[] eventsToTriggerOnExit;
    [SerializeField] private string[] tagsToCheck;

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in tagsToCheck)
        {
            if (other.gameObject.CompareTag(tag))
            {
                print($"Collision with - {other.gameObject}");
                foreach (UnityEvent unityEvent in eventsToTrigger)
                {
                    unityEvent.Invoke();
                }
                break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (string tag in tagsToCheck)
        {
            if (other.gameObject.CompareTag(tag))
            {
                print($"Collision with - {other.gameObject}");
                foreach (UnityEvent unityEvent in eventsToTriggerOnExit)
                {
                    unityEvent.Invoke();
                }
                break;
            }
        }
    }

}
