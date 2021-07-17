using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    None,
    Toxic
}


/// <summary>
/// Anson:
/// super class for handling dealing damage to life system
/// </summary>
public class DamageScript : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected List<string> tagList;
    [Header("Debug")]
    [SerializeField] protected List<LifeSystemScript> attackedTargets = new List<LifeSystemScript>();


    /// <summary>
    /// deals damage to a single target that has a LifeSystemScript
    /// </summary>
    /// <param name="ls"></param>
    public virtual void dealDamageToTarget(LifeSystemScript ls, float dmg)
    {
        try
        {
            ls.takeDamage(dmg);

        }
        catch (System.NullReferenceException e)
        {
            Debug.LogError(e.StackTrace);
        }
    }

    /// <summary>
    /// deals damage to a single target that has a LifeSystemScript
    /// </summary>
    /// <param name="ls"></param>
    public virtual void dealDamageToTarget(LifeSystemScript ls, float dmg, DamageType damageType)
    {
        try
        {
            if (!ls.DamageImmunity.Contains(damageType))
            {
                ls.takeDamage(dmg);
            }
            else
            {
                //Debug.Log("Damage to " + ls.name + " Immune");
            }

        }
        catch (System.NullReferenceException e)
        {
            Debug.LogError(e.StackTrace);
        }
    }

    /// <summary>
    /// deals damage to a single target that has a LifeSystemScript
    /// </summary>
    /// <param name="ls"></param>
    public virtual void dealCriticalDamageToTarget(LifeSystemScript ls, float dmg, float multiplier)
    {
        ls.takeDamageCritical(dmg, multiplier);
    }

    public string SetPlayerDeathString(string s)
    {
        return PlayerHandler.PlayerInstance.GetComponent<PlayerHandler>().SetDeathString(s);
    }


}
