using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassScript : MonoBehaviour
{
    [SerializeField] GameObject originalGO;
    [SerializeField] GameObject glassShatterGO;
    [SerializeField] Rigidbody[] glassShatterPieces;
    [SerializeField] Transform explodePoint;
    [SerializeField] float explodeForce;
    [SerializeField] AnimationCurve forceApplyCurve;
    [SerializeField] float maxObjectSize;

    private void Awake()
    {
        originalGO.SetActive(true);
        glassShatterGO.SetActive(false);
        glassShatterPieces = glassShatterGO.GetComponentsInChildren<Rigidbody>();
    }

    public void Explode()
    {
        originalGO.SetActive(false);
        glassShatterGO.SetActive(true);
        foreach (Rigidbody rb in glassShatterPieces)
        {
            float strength = forceApplyCurve.Evaluate(Vector3.Distance(rb.position, explodePoint.position) / maxObjectSize);
            rb.AddForce(explodePoint.forward * strength * explodeForce);
        }
    }


}
