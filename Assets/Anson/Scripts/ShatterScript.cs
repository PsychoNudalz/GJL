using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterScript : MonoBehaviour
{
    [SerializeField] GameObject originalGO;
    [SerializeField] GameObject shatterGO;
    [SerializeField] Rigidbody[] shatterPieces;
    [SerializeField] Transform explodePoint;
    [SerializeField] float explodeForce;
    [SerializeField] AnimationCurve forceApplyCurve;
    [SerializeField] float maxObjectSize;
    [SerializeField] Sound sound;
    bool explodeFlag = false;

    private void Awake()
    {
        originalGO.SetActive(true);
        shatterGO.SetActive(false);
        shatterPieces = shatterGO.GetComponentsInChildren<Rigidbody>();
    }

    public void Explode()
    {
        if (explodeFlag)
        {
            return;
        }
        originalGO.SetActive(false);
        shatterGO.SetActive(true);
        foreach (Rigidbody rb in shatterPieces)
        {
            float strength = forceApplyCurve.Evaluate(Vector3.Distance(rb.position, explodePoint.position) / maxObjectSize);
            rb.AddForce(explodePoint.forward * strength * explodeForce);
        }
        if (sound)
        {
            sound.Play();
        }
    }


}
