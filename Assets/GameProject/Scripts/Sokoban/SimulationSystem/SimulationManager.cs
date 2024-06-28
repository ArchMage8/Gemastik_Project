using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [Header("System :")]
    public float directionFloat;
    private GameObject currObject;
    private GameObject nextObject;
    public bool failed = false;
    public bool completed = false;
    public bool isMoving = false;
    public bool isSimulating = false;
    public float movementSpeed = 0.5f;

    [Space (10)]
    [Header ("Win/Lose :")] 
    public GameObject fail_indicator;
    public GameObject Win_Canvas;
    public GameObject Screen_Buttons;
    public GameObject RestartUI;

    [HideInInspector] public bool animationGo = false;
    

    [Space(10)]
    [Header("Detectors :")]
    public CurrDetector currDetector;
    public NextDetector nextDetector;
    

    public void Start()
    {
        RestartUI.SetActive (false);
        fail_indicator.SetActive(false);
        Win_Canvas.SetActive(false);
    }

    public void Update()
    {
        if (isSimulating)
        {
            //Debug.Log("Start Simulation");
            StartCoroutine(Simulation());  
        }

        if (!isMoving)
        {
            nextObject = nextDetector.targetObject;
            currObject = currDetector.targetObject;
        }

        if (failed == true && completed == false)
        {
            fail_indicator.SetActive(true);
            StartCoroutine(restartUI());
        }

        if(completed == true && failed == false)
        {
            Win_Canvas.SetActive(true);
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
                StartCoroutine(MoveObjectToPosition(this.transform, nextObject.transform.position, movementSpeed));
                
            }
            
            else if(nextObject.CompareTag(currObject.tag) && !nextObject.CompareTag("Rotation"))  //The same block type is next
            {
                //Debug.Log("Scenario 2");
                StartCoroutine(MoveObjectToPosition(this.transform, nextObject.transform.position, movementSpeed));
            }

            else if (!nextObject.CompareTag(currObject.tag) && currObject.CompareTag("Rotation")) //Checking the block after the rotation block
            {
                string currTargetType =  currObject.GetComponent<DirectionHolder>().currentTargetType.ToString();

                if(currTargetType == nextObject.tag)
                {
                    //Debug.Log("Scenario 3");
                    StartCoroutine(MoveObjectToPosition(this.transform, nextObject.transform.position, movementSpeed));
                }

                else
                {
                    //Debug.Log("Scenario Fail 1");
                    failed = true;
                }
            }
            
            else
            {
                if (nextObject.CompareTag("Rotation"))  //Checking the rotation block
                {
                    //Debug.Log("Scenario 4");
                    StartCoroutine(MoveObjectToPosition(this.transform, nextObject.transform.position, movementSpeed));
                    directionFloat = nextObject.GetComponent<DirectionHolder>().direction;
                }

                else
                {
                    //Debug.Log("Scenario fail 2");
                    failed = true;
                }
            }
        }
        else if(nextObject == null)
        {
            if (currObject.CompareTag("Finish"))
            {
                //Debug.Log("Scenario 1");
                Screen_Buttons.SetActive(false);
                completed = true;
            }

            else
            {
               //Debug.Log("Scenario fail 3-1");
                failed = true;
            }
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

    private IEnumerator restartUI()
    {
        yield return new WaitForSeconds(1.5f);
        RestartUI.SetActive(true);
    }
}
