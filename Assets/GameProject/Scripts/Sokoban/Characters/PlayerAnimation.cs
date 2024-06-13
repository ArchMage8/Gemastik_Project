using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerController controller;
    public int Direction;
    private bool isMoving;
    public bool isPushing;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isPushing)
        {
            Direction = controller.DirectionalInt;
        }
        isMoving = controller.isMoving;

        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsPushing", isPushing);

        if(Direction == 1)      //North
        {
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Horizontal", 1);
        }

        else if(Direction == 2) //South
        {
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Horizontal", -1);
        }

        else if (Direction == 3) //East
        {
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Horizontal", 0);
        }

        else if (Direction == 4) //West
        {
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Horizontal", 0);
        }

        else if (Direction == 5) //NorthEast
        {
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Horizontal", 1);
        }

        else if (Direction == 6) //NorthWest
        {
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Horizontal", 1);
        }

        else if (Direction == 7) //SouthEast
        {
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Horizontal", -1);
        }

        else if (Direction == 8) //SouthWest
        {
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Horizontal", -1);
        }
    }
}
