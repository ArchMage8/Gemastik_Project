using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindPersonAnimation : MonoBehaviour
{
    public  SimulationManager simulationManager;
    private float directionFloat;
    private bool isMoving;

    private void Update()
    {
        simulationManager.directionFloat = directionFloat;
        simulationManager.isMoving = isMoving;

        if (directionFloat == 5f)   //NorthEast
        {

        }

        else if (directionFloat == 6f)  //NorthWest
        {

        }

        else if (directionFloat == 7f)  //SouthEast
        {

        }

        else if (directionFloat == 8f)  //SouthWest
        {

        }
    }
}
