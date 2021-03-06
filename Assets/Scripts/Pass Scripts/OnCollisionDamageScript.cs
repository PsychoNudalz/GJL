using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anson:
/// Extends Damage Script
/// Deals damage on collisionm or on trigger
/// requires collider
/// </summary>
public class OnCollisionDamageScript : DamageScript
{
    [Header("Collision Behaviour")]
    public bool onTrigger;
    public bool onEnter = true;
    //public bool onStay;
    public float tickIntervals = 0.5f;
    float lastDamageTick;
    public float damage;
    [SerializeField] DamageType damageType = DamageType.None;

    //public bool onExit;
    //public bool addToTargetsOnEnter = true;
    //public bool removeFromTargetsOnExit = true;

    private void FixedUpdate()
    {
        if (tickIntervals == 0)
        {
            return;
        }
        else if (Time.time - lastDamageTick > tickIntervals)
        {
            DealDamageToTargets();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onTrigger && onEnter && tagList.Contains(other.gameObject.tag) && LifeSystemScript.GetLifeSystemScript(other.gameObject) != null)
        {
            AddTargetToList(other.gameObject.GetComponentInParent<LifeSystemScript>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (onEnter && tagList.Contains(collision.gameObject.tag))
        {
            LifeSystemScript ls = LifeSystemScript.GetLifeSystemScript(collision.gameObject);
            if (!ls)
            {
                return;
            }
            print("Collision: " + ls.name);
            AddTargetToList(ls);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!onTrigger && onEnter && tagList.Contains(collision.gameObject.tag) && LifeSystemScript.GetLifeSystemScript(collision.gameObject) != null)
        {
            LifeSystemScript ls = LifeSystemScript.GetLifeSystemScript(collision.gameObject);
            if (attackedTargets.Contains(ls))
            {
                attackedTargets.Remove(ls);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (onTrigger && onEnter && tagList.Contains(other.gameObject.tag) && LifeSystemScript.GetLifeSystemScript(other.gameObject) != null)
        {
            LifeSystemScript ls = LifeSystemScript.GetLifeSystemScript(other.gameObject);
            if (attackedTargets.Contains(ls))
            {
                attackedTargets.Remove(ls);
            }
        }
    }



    public void AddTargetToList(LifeSystemScript ls)
    {
        print("Add: " + ls);
        if (attackedTargets.Contains(ls))
        {
            return;
        }
        attackedTargets.Add(ls);
        dealDamageToTarget(ls, damage);
    }

    void DealDamageToTargets()
    {
        foreach (LifeSystemScript ls in attackedTargets)
        {
            if (ls != null)
            {
                dealDamageToTarget(ls, damage * ((Time.time - lastDamageTick) / tickIntervals));
            }
        }
        while (attackedTargets.Contains(null))
        {

            attackedTargets.Remove(null);
        }
        lastDamageTick = Time.time;
    }

    public override void dealDamageToTarget(LifeSystemScript ls, float dmg)
    {
        if (damageType.Equals(DamageType.None))
        {
            base.dealDamageToTarget(ls, dmg);

        }
        else
        {
            base.dealDamageToTarget(ls, dmg, damageType);
        }
    }






}
