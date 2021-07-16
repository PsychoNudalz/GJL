using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicGasController : MonoBehaviour
{

    [SerializeField] ExplosiveKillZone[] killZones;
    [SerializeField] int count = 0;

    public void ActivateNextGas()
    {
        if (count >= killZones.Length)
        {
            Debug.LogWarning("Max count reached");
            return;
        }
        killZones[count].Activate();
        count++;
        return;
    }
}
