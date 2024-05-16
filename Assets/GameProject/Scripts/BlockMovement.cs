using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public GameObject blockToMove;
    
    private float directionFloat;
    private DetectionManager detectionManager;
    private PlayerDetectManager playerDetectManager ;

    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;
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
            MoveCheck();
            if (!isMoving && blockToMove != null && canMove && inRay)
            {

                SetTargetPosition();
                isMoving = true;
            }
        }

        if (isMoving)
        {
            
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
        blockToMove.transform.position = Vector3.Lerp(blockToMove.transform.position, targetPosition, step);

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

    private float NormalizeAngle(float angle)
    {
        while (angle > 180) angle -= 360;
        while (angle < -180) angle += 360;
        return angle;
    }
}

