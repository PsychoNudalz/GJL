using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHandler : MonoBehaviour
{
    [SerializeField] ItemSpawn blowTorch;
    [SerializeField] ItemSpawn fertiliser;

    public void NextGas(int i)
    {
        if (FindObjectOfType<ToxicGasController>().ActivateNextGas())
        {
            print("Spawn");
            if (i == 1)
            {
                fertiliser.Spawn();
            }
            else
            {
                blowTorch.Spawn();
            }
        }
    }
}
