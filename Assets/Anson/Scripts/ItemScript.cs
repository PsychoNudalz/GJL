using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tools { Stick, Hammer };


public class ItemScript : MonoBehaviour
{

    [SerializeField] Tools toolType;
    [SerializeField] Rigidbody rb;
    [SerializeField] List<Collider> colliders;


    public void OnPickUp()
    {
        SetPhysics(false);
        Debug.Log("Picked Up: " + ToolEnumConvertor.ToName(toolType));
    }
    public void OnUse()
    {

    }
    public void OnDrop()
    {
        SetPhysics(true);

    }

    /// <summary>
    /// set the physics of the item
    /// True means activating phycis
    /// </summary>
    /// <param name="b"></param>
    void SetPhysics(bool b)
    {
        rb.isKinematic = !false;
        foreach (Collider c in colliders)
        {
            c.enabled = b;
        }
    }
}
