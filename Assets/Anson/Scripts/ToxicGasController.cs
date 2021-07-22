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
    [SerializeField] UnityEvent onBurn;
    [SerializeField] UnityEvent onThresshold;

    public bool ActivateNextGas()
    {
        if (count >= killZones.Length)
        {
            Debug.LogWarning("Max count reached");
            return true;
        }
        killZones[count].Activate();
        onBurn.Invoke();
        count++;
        if (count == threshold)
        {
            onThresshold.Invoke();
            PlayerHandler.handler.ShakeCamera(10f);
            return false;
        }
        return true;
    }
}
