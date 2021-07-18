using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveKillZone : MonoBehaviour
{
    [SerializeField] GameObject[] damageZones;
    [SerializeField] ParticleSystem[] particleEffects;
    [SerializeField] Sound[] sounds;

    public void Activate()
    {
        gameObject.SetActive(true);
        foreach(GameObject g in damageZones)
        {
            g.SetActive(true);
        }
        foreach(ParticleSystem ps in particleEffects)
        {
            ps.Play();
        }
        foreach(Sound s in sounds)
        {
            s.Play();
        }
    }
}
