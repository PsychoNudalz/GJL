using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystemScript : LifeSystemScript
{
    [Header("Player")]
    [SerializeField] PlayerVolumnController playerVolumnController;
    [SerializeField] PlayerHeadScript playerHeadScript;
    [SerializeField] string deathString;
    public PlayerVolumnController PlayerVolumnController { get => playerVolumnController; set => playerVolumnController = value; }
    public string DeathString { get => deathString; set => deathString = value; }

    public override void PlayTakeDamageEffect()
    {
        playerVolumnController.SetBloodVignette(true, 1-GetPercentageHealth());
        base.PlayTakeDamageEffect();

    }

    public override void ResetSystem()
    {
        base.ResetSystem();
        playerVolumnController.SetBloodVignette(true, 1-GetPercentageHealth());
        playerHeadScript.Reset();
    }
}
