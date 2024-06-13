using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDetector : MonoBehaviour
{
    public SimulationManager simulationManager;
    private float direction;
    private int detectLayer;

    public GameObject targetObject;

    private void Awake()
    {
        detectLayer = LayerMask.NameToLayer("Interactable");
    }

    public void Update()
    {
        direction = simulationManager.directionFloat;

        if(direction == 5f) //NorthEast
        {
            transform.localPosition = new Vector3(0.5f, 0.23f, 0);
        }

        else if(direction == 6f) //NorthWest
        {
            transform.localPosition = new Vector3(-0.5f, 0.23f, 0);
        }

        else if (direction == 7f) //SouthEast
        {
            transform.localPosition = new Vector3(0.5f, -0.3f, 0);
        }

        else if (direction == 8f) //SouthWest
        {
            transform.localPosition = new Vector3(-0.5f, -0.3f, 0);
        }

        else
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == detectLayer)
        {
            targetObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == detectLayer)
        {
            targetObject = null;
        }
    }
}
