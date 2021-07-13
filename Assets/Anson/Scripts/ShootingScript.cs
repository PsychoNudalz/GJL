using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{

    [Header("Weapon Stats")]
    [SerializeField] GameObject projectileObject;
    [SerializeField] float projectileForce;
    [SerializeField] float destroyTime = 5f;
    [SerializeField] float rpm;
    [SerializeField] int ammo;
    [Header("Variables")]
    [SerializeField] bool isFire;
    [SerializeField] float lastFireTime;
    [SerializeField] Transform launchPoint;


    private void Update()
    {
        if (CanFire())
        {
            Shoot();
        }
        lastFireTime += Time.deltaTime;
    }

    public bool CanFire()
    {
        if (isFire)
        {
            if (lastFireTime >= (60f / rpm))
            {
                if (ammo > 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void Shoot()
    {
        if (projectileObject)
        {
            GameObject temp = Instantiate(projectileObject, launchPoint.position, launchPoint.rotation);
            Destroy(temp, destroyTime);
            if (temp.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(launchPoint.forward * rb.mass * projectileForce);
            }
            lastFireTime = 0f;
        }
        else
        {
            Debug.LogWarning(name + " missing projectile");
        }
    }

    public void SetFire(bool b)
    {
        isFire = b;
    }

}
