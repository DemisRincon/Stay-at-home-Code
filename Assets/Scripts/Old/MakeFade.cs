using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeFade : MonoBehaviour
{
    public Animator animator;
    // Update is called once per frame


    public Actions playerActions;


    public void ApplyTransition()
    {
        animator.SetTrigger("Fade");
    }
  

}
