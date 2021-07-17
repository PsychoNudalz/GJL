using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHeadScript : MonoBehaviour
{
    private const float defaultLaunchForce = 300f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider headCollider;
    [SerializeField] float explodeDelay = 0.05f;
    [SerializeField] UnityEvent onExplode;
    [SerializeField] Transform parent;
    [SerializeField] GameObject toolGO;


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
        parent = transform.parent;
        Reset();
    }

    public void Reset()
    {
        rb.isKinematic = true;
        headCollider.enabled = false;
        transform.SetParent(parent);
        transform.position = parent.position;
        transform.rotation = parent.rotation;
        toolGO.SetActive(true);
    }

    public void Explode(Vector3 force)
    {
        StartCoroutine( DelayExplode(force));
    }

    private void ExplodeBehaviour(Vector3 force)
    {
        if (force.magnitude.Equals(0))
        {
            force = (- transform.forward).normalized * defaultLaunchForce;
        }
        UnlockHead();
        rb.AddForce(force * rb.mass);
        rb.AddTorque(Vector3.right * defaultLaunchForce / 3f);
        onExplode.Invoke();
        toolGO.SetActive(false);

        return;
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
    IEnumerator DelayExplode(Vector3 force)
    {
        yield return new WaitForSeconds(explodeDelay);
        ExplodeBehaviour(force);

    }

}
