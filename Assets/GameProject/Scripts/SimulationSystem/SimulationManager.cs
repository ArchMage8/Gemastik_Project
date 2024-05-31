using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public float directionFloat;
    private GameObject currObject;
    private GameObject nextObject;
    public bool failed = false;
    public bool completed = false;
    public bool isMoving = false;

    [HideInInspector] public bool animationGo = false;
    public bool isSimulating = false;

    public CurrDetector currDetector;
    public NextDetector nextDetector;
    public float movementSpeed = 5f;

    public void Update()
    {
        if (isSimulating)
        {
            Debug.Log("Start Simulation");
            StartCoroutine(Simulation());  
        }

        if (!isMoving)
        {
            nextObject = nextDetector.targetObject;
            currObject = currDetector.targetObject;
        }

        //Debug.Log("next object : " + nextObject.name);
        //Debug.Log("curr object : " + currObject.name);
    }

    private IEnumerator Simulation()
    {
        while (!completed && !failed)
        {
            yield return new WaitForSeconds(3f);
            ObjectCheck();

            animationGo = true;
        }
        
        animationGo = false;
    }

    void ObjectCheck()
    {
       
        if (nextObject != null && !isMoving)
        {
           

            if (nextObject.CompareTag("Finish"))
            {
                Debug.Log("Scenario 1");    
                StartCoroutine(MoveObjectToPosition(this.transform, nextObject.transform.position, movementSpeed));
                completed = true;
            }
            
            else if(nextObject.CompareTag(currObject.tag) && !nextObject.CompareTag("Rotation"))  //The same block type is next
            {
                Debug.Log("Scenario 2");
                StartCoroutine(MoveObjectToPosition(this.transform, nextObject.transform.position, movementSpeed));
            }

            else if (!nextObject.CompareTag(currObject.tag) && currObject.CompareTag("Rotation")) //Checking the block after the rotation block
            {
                string currTargetType =  currObject.GetComponent<DirectionHolder>().currentTargetType.ToString();

                if(currTargetType == nextObject.tag)
                {
                    Debug.Log("Scenario 3");
                    StartCoroutine(MoveObjectToPosition(this.transform, nextObject.transform.position, movementSpeed));
                }

                else
                {
                    Debug.Log("Scenario Fail 1");
                    failed = true;
                }
            }
            
            else
            {
                if (nextObject.CompareTag("Rotation"))  //Checking the rotation block
                {
                    Debug.Log("Scenario 4");
                    StartCoroutine(MoveObjectToPosition(this.transform, nextObject.transform.position, movementSpeed));
                    directionFloat = nextObject.GetComponent<DirectionHolder>().direction;
                }

                else
                {
                    Debug.Log("Scenario fail 2");
                    failed = true;
                }
            }
        }
        else if(nextObject == null)
        {
            Debug.Log("Scenario fail 3-1");
            failed = true;
        }
    }

    private IEnumerator MoveObjectToPosition(Transform objectToMove, Vector3 targetPosition, float movementSpeed)
    {
        targetPosition.y += 0.3f;
        

        do
        {
            float distanceToTarget = Vector3.Distance(objectToMove.position, targetPosition);

            if (distanceToTarget > 0.001f)
            {
                isMoving = true;
                Vector3 newPosition = Vector3.Lerp(objectToMove.position, targetPosition, movementSpeed * Time.deltaTime / distanceToTarget);
                objectToMove.position = newPosition;
            }


            else
            {
                
                isMoving = false;

            }
            
            
            yield return null;
        } while (isMoving);
        
    }
}
