using System.Collections;
//using UnityEditor.Media;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public GameObject blockToMove;
    
    public float directionFloat;
    private DetectionManager detectionManager;
    private PlayerDetectManager playerDetectManager ;

    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    public bool isMoving = false;
    private bool canMove = false;
    public bool playerCollide = false;
    public bool inRay = false;


    private PlayerController playerController;

    private void Start()
    {
        detectionManager = GetComponentInChildren<DetectionManager>();
        playerDetectManager = GetComponentInChildren<PlayerDetectManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerCollide)
        {
          
            GetDirection();
            ForceDirection();
            MoveCheck();

            if (!isMoving && blockToMove != null && canMove && inRay)
            {
                
                SetTargetPosition();
                isMoving = true;
            }
        }

        if (isMoving)
        {
           
            StartCoroutine(AnimationHandler());
            MoveBlock();
        }
    }

    void SetTargetPosition()
    {
        if (directionFloat == 5f)
        {
            targetPosition = blockToMove.transform.position + new Vector3(0.5f, 0.25f, 0); //NorthEast
        }
        else if (directionFloat == 6f)
        {
            targetPosition = blockToMove.transform.position + new Vector3(-0.5f, 0.25f, 0); //NorthWest
        }

        else if (directionFloat == 7f)
        {
            targetPosition = blockToMove.transform.position + new Vector3(0.5f, -0.25f, 0); //SouthEast
        }

        else if (directionFloat == 8f)
        {
            targetPosition = blockToMove.transform.position + new Vector3(-0.5f, -0.25f, 0); //SouthWest
        }
    }

    void MoveBlock()
    {
        float step = moveSpeed * Time.deltaTime;
        blockToMove.transform.position = Vector3.MoveTowards(blockToMove.transform.position, targetPosition, step);

        if (Vector3.Distance(blockToMove.transform.position, targetPosition) < 0.001f)
        {
            blockToMove.transform.position = targetPosition;
            isMoving = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerCollide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = null;
            playerCollide = false;
        }
    }

    void MoveCheck()
    {
        if(directionFloat == 5f && detectionManager.NortheastDetected)
        {
            canMove = false;
        }

        else if (directionFloat == 6f && detectionManager.NorthwestDetected)
        {
            canMove = false;
        }

        else if (directionFloat == 7f && detectionManager.SoutheastDetected)
        {
            canMove = false;
        }

        else if (directionFloat == 8f && detectionManager.SouthwestDetected)
        {
            canMove = false;
        }

        else
        {
            canMove = true;
        }
    }

    void GetDirection()
    {
        if(playerDetectManager != null)
        {
            if (playerDetectManager.NortheastDetected)
            {
                directionFloat = 8f;
                inRay = true;
            }

            else if (playerDetectManager.NorthwestDetected)
            {

                directionFloat = 7f;
                inRay = true;
            }
            else if (playerDetectManager.SoutheastDetected)
            {

                directionFloat = 6f;
                inRay = true;
            }
            else if (playerDetectManager.SouthwestDetected)
            {
                directionFloat = 5f;
                inRay = true;
            }

            else
            {

                directionFloat = 0f;
                inRay = false;
            }
        }
    }

    private IEnumerator AnimationHandler()
    {
        GameObject playerObject = playerDetectManager.playerObject;
        PlayerAnimation playerAnimations = playerObject.GetComponentInChildren<PlayerAnimation>();

        while (isMoving)
        {
           
            playerObject.transform.SetParent(transform);
            playerObject.GetComponent<PlayerController>().isMoving = false;
            playerObject.GetComponent<PlayerController>().enabled = false;
            playerAnimations.isPushing = true;


            yield return null;
        }
       playerObject.transform.SetParent(null);
     
       playerObject.GetComponent<PlayerController>().enabled = true;
       playerAnimations.isPushing = false;
    }

    private void ForceDirection()
    {
        GameObject player = playerDetectManager.playerObject.gameObject;
        PlayerAnimation temp = player.GetComponentInChildren<PlayerAnimation>();

        if (playerDetectManager.NortheastDetected) //NorthEast
        {
            Debug.Log("Test1");
            temp.Direction = 8;
        }

        else if (playerDetectManager.NorthwestDetected) //NorthWest
        {
            Debug.Log("Test2");
            temp.Direction = 7;
        }

        else if (playerDetectManager.SoutheastDetected) //SouthEast
        {
            Debug.Log("Test3");
            temp.Direction = 6;
        }

        else if (playerDetectManager.SouthwestDetected) //SouthWest
        {
            Debug.Log("Test4");
            temp.Direction = 5;
        }
    }
}

