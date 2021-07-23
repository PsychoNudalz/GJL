using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ToxicGasController : MonoBehaviour
{

    [SerializeField] ExplosiveKillZone[] killZones;
    [SerializeField] int count = 0;
    [Header("Fan")]
    [SerializeField] int threshold;
    [SerializeField] UnityEvent onBurn;
    [SerializeField] UnityEvent onThresshold;
    [Header("Gas Volumn")]
    [SerializeField] Volume volume;

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
        volume.weight = (float)count / (float)threshold;
        if (count == threshold)
        {
            onThresshold.Invoke();
            PlayerHandler.handler.ShakeCamera(10f);
            return false;
        }
        return true;
    }
}
