using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindPersonAnimation : MonoBehaviour
{
    public  SimulationManager simulationManager;
    private float directionFloat;
    private Animator animator;
    private bool isMoving;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        simulationManager.directionFloat = directionFloat;
        simulationManager.isMoving = isMoving;

        animator.SetBool("IsMoving", isMoving);

        if (directionFloat == 5f)   //NorthEast
        {
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Horizontal", 1);
        }

        else if (directionFloat == 6f)  //NorthWest
        {
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Horizontal", 1);
        }

        else if (directionFloat == 7f)  //SouthEast
        {
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Horizontal", -1);
        }

        else if (directionFloat == 8f)  //SouthWest
        {
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Horizontal", 1);
        }
    }
}
