using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHeadScript : MonoBehaviour
{
    private const float defaultLaunchForce = 500f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider headCollider;
    [SerializeField] UnityEvent onExplode;

    private void Awake()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (!headCollider)
        {
            headCollider = GetComponent<Collider>();
        }
        rb.isKinematic = true;
        headCollider.enabled = false;
    }

    public void Explode(Vector3 force)
    {
        if (force.magnitude.Equals(0))
        {
            force = (Vector3.up- transform.forward)* defaultLaunchForce;
        }
        UnlockHead();
        rb.AddForce(force * rb.mass);
        rb.AddTorque(Vector3.up * defaultLaunchForce / 2f);
        onExplode.Invoke();
    }

    public void Explode()
    {
        Explode(new Vector3());
    }

    public void UnlockHead()
    {
        rb.isKinematic = false;
        headCollider.enabled = true;
        transform.parent = null;
    }
}
