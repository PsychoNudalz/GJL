using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeScript : MonoBehaviour
{
    public void ChangePlayerGravity(float value,float duration = 10f)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().SetGravity(value,duration);
    }
    public void ChangePlayerGravity(float value)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().SetGravity(value);
    }
    public void ResetPlayerGravity()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().ResetGravity();
    }
}
