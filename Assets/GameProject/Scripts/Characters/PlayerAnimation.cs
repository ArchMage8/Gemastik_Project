using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerController controller;
    private int Direction;
    private bool isMoving;
    public bool isPushing;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Direction = controller.DirectionalInt;
        isMoving = controller.isMoving;

        if(Direction == 1)
        {
            
        }

        else if(Direction == 2) 
        {
           
        }

        else if (Direction == 3)
        {
            
        }

        else if (Direction == 4)
        {
            
        }

        else if (Direction == 5)
        {
            
        }

        else if (Direction == 6)
        {
           
        }

        else if (Direction == 7)
        {
           
        }

        else if (Direction == 8)
        {
           
        }
    }
}
