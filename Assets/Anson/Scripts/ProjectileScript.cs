using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        print("Hit");
        Destroy(gameObject);
    }
}
