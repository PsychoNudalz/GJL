using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeScript : MonoBehaviour
{
    public void ChangePlayerGravity(float value,float duration = 300f)
    {
        FindObjectOfType< FirstPersonController>().SetGravity(value,duration);
    }
    public void ChangePlayerGravity(float value)
    {
        FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>().SetGravity(value);
    }
    public void ResetPlayerGravity()
    {
        FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>().ResetGravity();
    }
}
