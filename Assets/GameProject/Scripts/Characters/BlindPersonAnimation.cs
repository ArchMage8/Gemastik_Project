using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindPersonAnimation : MonoBehaviour
{
    public  SimulationManager simulationManager;
    private float directionFloat;
    private Animator animator;
    private bool isMoving;

    private float rotateDelay = 1f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        directionFloat = simulationManager.directionFloat;
        isMoving = simulationManager.animationGo;

        animator.SetBool("IsMoving", isMoving);

        StartCoroutine(RotateSprite());
    }

    private IEnumerator RotateSprite()
    {
        if (directionFloat == 5f)   //NorthEast
        {
            yield return new WaitForSeconds(rotateDelay);
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Horizontal", 1);
        }

        else if (directionFloat == 6f)  //NorthWest
        {
            yield return new WaitForSeconds(rotateDelay);
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Horizontal", 1);
        }

        else if (directionFloat == 7f)  //SouthEast
        {
            yield return new WaitForSeconds(rotateDelay);
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Horizontal", -1);
        }

        else if (directionFloat == 8f)  //SouthWest
        {
            yield return new WaitForSeconds(rotateDelay);
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Horizontal", 1);
        }
    }
}

