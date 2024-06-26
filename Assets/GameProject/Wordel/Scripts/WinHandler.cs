using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    //What happens when we win the wordle

    [Space(10)]
    [Header("Animator: ")]
    private Animator animator;
    private bool StartBool = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Complete()
    {
        StartBool = true;
    }

    public void Bus()
    {
        if (StartBool)
        {
            animator.SetTrigger("Bus"); //Bus Arrives
        }
    }

    public void Doors()
    {
        animator.SetTrigger("Doors"); //Open Doors
    }

    public void Peoples()
    {
        animator.SetTrigger("People"); //Walk Cycles
    }

    
}
