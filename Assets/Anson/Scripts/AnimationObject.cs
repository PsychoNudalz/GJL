using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObject : MonoBehaviour
{

    [SerializeField] Animator animator;

    private void Awake()
    {
        if (animator== null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void Play_Trigger(string s)
    {
        animator.SetTrigger(s);
    }
    
    public void Play_Int(string s)
    {
        string[] temp = s.Split('/');
        animator.SetInteger(temp[0], int.Parse(temp[1]));
    }
    public void Play_Bool(string s)
    {
        string[] temp = s.Split('/');
        if (int.Parse(temp[1]) == 0)
        {
            animator.SetBool(temp[0], false);
        }
        else
        {
            animator.SetBool(temp[0], true);
        }
    }
    public void Play_Float(string s)
    {
        string[] temp = s.Split('/');
        animator.SetFloat(temp[0], float.Parse(temp[1]));
    }

}
