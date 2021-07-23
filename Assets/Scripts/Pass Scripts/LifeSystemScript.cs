﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

/// <summary>
/// Anson:
/// base Life system super class, handles heealth, taking damage, healing
/// </summary>

public class LifeSystemScript : MonoBehaviour
{

    [Header("States")]
    [SerializeField] protected int health_Current;
    [SerializeField] protected int health_Max = 100;
    [SerializeField] bool isDead = false;
    [SerializeField] protected Transform centreOfMass;
    [SerializeField] List<DamageType> damageImmunity;

    [Header("On Death")]
    public GameObject deathGameObject;
    public bool disableOnDeath = false;
    public bool destroyOnDeath;
    public float delayDeath = 0;
    [SerializeField] GameObject detactchGameObject;
    [SerializeField] float detactchedDestroyTime = 5;
    [SerializeField] UnityEvent deathEvent;

    Coroutine deathCoroutine;

    Vector3 popUpLocation;
    Vector3 particleLocation;


    public int Health_Current { get => health_Current; }
    public int Health_Max { get => health_Max; }
    public bool IsDead { get => isDead; }
    public List<DamageType> DamageImmunity { get => damageImmunity; set => damageImmunity = value; }

    public static LifeSystemScript GetLifeSystemScript(GameObject go, bool tryParent = false)
    {
        LifeSystemScript ls = go.GetComponentInChildren<LifeSystemScript>();
        if (ls == null && tryParent)
        {
            ls = go.GetComponentInParent<LifeSystemScript>();
        }
        return ls;
    }

    protected void Awake()
    {

        AwakeBehaviour();
    }

    protected virtual void AwakeBehaviour()
    {
        health_Current = health_Max;
        try
        {
            // updateHealthBar();
        }
        catch (System.Exception)
        {
            print("LifeSystemScript error - ");
        }
    }

    private void FixedUpdate()
    {
        //StartCoroutine(TickDebuffs());
    }


    public void OverrideHealth(int hp)
    {
        health_Current = hp;
        health_Max = hp;
    }

    public virtual void ResetSystem()
    {
        health_Current = health_Max;
        isDead = false;
        deathCoroutine = null;
        damageImmunity = new List<DamageType>();
    }

    /// <summary>
    /// deal damage to the gameobject
    /// damage rounded to the closest integer
    /// triggers death event if health reaches 0
    /// </summary>
    /// <param name="dmg"></param>
    /// <returns> health remaining </returns>
    public virtual int takeDamage(float dmg, bool displayTakeDamageEffect = true)
    {

        health_Current -= Mathf.RoundToInt(dmg);
        if (!isDead)
        {
            // print(name + " take damage: " + dmg);
            //updateHealthBar();
            if (displayTakeDamageEffect)
            {

                PlayTakeDamageEffect();
            }
        }

        CheckDead();
        return health_Current;

    }

    /// <summary>
    /// deal critical damage to the gameobject
    /// damage rounded to the closest integer
    /// triggers death event if health reaches 0
    /// </summary>
    /// <param name="dmg"></param>
    /// <returns> health remaining </returns>
    public virtual int takeDamageCritical(float dmg, float multiplier = 1, bool displayTakeDamageEffect = true)
    {

        health_Current -= Mathf.RoundToInt(dmg * multiplier);
        if (!isDead)
        {
            //updateHealthBar();
            if (displayTakeDamageEffect)
            {

                PlayTakeDamageEffect();
            }
        }

        CheckDead();
        return health_Current;

    }
    /// <summary>
    /// heal gameobject
    /// amount rounded to the closest integer
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns> health remaining</returns>
    public virtual int healHealth(float amount)
    {
        if (!isDead)
        {
            health_Current += Mathf.RoundToInt(amount);
            //print(name + " heal damage: " + amount);
            if (health_Current > health_Max)
            {
                health_Current = health_Max;
            }
            //updateHealthBar();
        }
        return health_Current;
    }


    /// <summary>
    /// heal gameobject
    /// amount based on maximum health
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns> health remaining</returns>
    public virtual int healHealth_Percentage(float amount)
    {
        amount = Mathf.Clamp(amount, 0f, 1f);
        if (!isDead)
        {
            healHealth(amount * health_Max);

        }
        return health_Current;
    }
    /// <summary>
    /// heal gameobject
    /// amount based on missing health
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns> health remaining</returns>
    public virtual int healHealth_PercentageMissing(float amount)
    {
        amount = Mathf.Clamp(amount, 0f, 1f);
        if (!isDead)
        {
            healHealth(Mathf.RoundToInt(amount * (health_Max - health_Current)));

        }
        return health_Current;
    }


    /// <summary>
    /// check if the gameobject is dead
    /// plays death event when health reaches 0
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckDead()
    {
        if (health_Current <= 0)
        {
            isDead = true;
            health_Current = 0;
            if (deathCoroutine == null)
            {
            deathCoroutine = StartCoroutine(delayDeathRoutine());
            }
        }
        return isDead;
    }



    public virtual void PlayTakeDamageEffect()
    {

    }

    /*
    protected void updateHealthBar()
    {
        if (healthBarController != null)
        {
            healthBarController.SetMaxHealth(health_Max);
            healthBarController.SetHealth((float)health_Current);
        }
    }
    */


    /// <summary>
    /// how the game object behave when killed
    /// </summary>
    public virtual void DeathBehaviour()
    {
        if (deathGameObject != null)
        {
            Instantiate(deathGameObject, deathGameObject.transform.position, deathGameObject.transform.rotation).SetActive(true);
        }


        if (disableOnDeath)
        {

            gameObject.SetActive(false);
        }
        else if (destroyOnDeath)
        {

            Destroy(gameObject);
        }

        deathEvent.Invoke();

    }

    /// <summary>
    /// delay death behaviour by a certain time
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator delayDeathRoutine()
    {
        yield return new WaitForSeconds(delayDeath);
        DeathBehaviour();
    }

    public virtual IEnumerator reattach()
    {

        yield return new WaitForSeconds(3f);

    }




    private void OnEnable()
    {
        try
        {
        }
        catch (System.Exception)
        {

        }

    }



    public float GetPercentageHealth()
    {
        return Mathf.Clamp((float)health_Current / (float)health_Max, 0f, 1f);
    }

    public float DrainMaxHealth(int amount)
    {

        health_Max -= amount;
        if (health_Max < 1)
        {
            health_Max = 1;

        }
        health_Current = Mathf.RoundToInt(Mathf.Clamp(health_Current, 0f, health_Max));
        return health_Max;

    }

    public virtual Vector3 GetEffectCenter()
    {
        return transform.position;
    }

    public Transform GetCentreOfMass()
    {
        if (centreOfMass == null)
        {
            return transform;
        }
        else
        {
            return centreOfMass.transform;
        }

    }


    public void AddImmunity(DamageType damageType)
    {
        if (!damageImmunity.Contains(damageType))
        {
            damageImmunity.Add(damageType);
        }
    }

    public void RemoveImmunity(DamageType damageType)
    {
        if (damageImmunity.Contains(damageType))
        {
            damageImmunity.Remove(damageType);
        }
    }
}
