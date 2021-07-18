using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToxicGasController : MonoBehaviour
{

    [SerializeField] ExplosiveKillZone[] killZones;
    [SerializeField] int count = 0;
    [Header("Fan")]
    [SerializeField] int threshold;
    [SerializeField] UnityEvent unityEvent;

    public bool ActivateNextGas()
    {
        if (count >= killZones.Length)
        {
            Debug.LogWarning("Max count reached");
            return true;
        }
        killZones[count].Activate();
        count++;
        if (count == threshold)
        {
            unityEvent.Invoke();
            return false;
        }
        return true;
    }
}
