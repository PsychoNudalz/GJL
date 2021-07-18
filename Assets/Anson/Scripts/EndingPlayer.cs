using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndingPlayer : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void PlayEnding(string endingRing)
    {
        animator.Play($"Ending_{endingRing}");
    }
}
