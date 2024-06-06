using System.Collections;
//using UnityEditor.Media;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    private GameObject blockToMove;
    
    [HideInInspector] public float directionFloat;
    private DetectionManager detectionManager;
    private PlayerDetectManager playerDetectManager ;

    [Header("System: ")]
    public float moveSpeed = 5f;
    [SerializeField] private AudioSource blockMoveSound;

    private Vector3 targetPosition;
    [HideInInspector] public bool isMoving = false;
    private bool canMove = false;
    [HideInInspector] public bool playerCollide = false;
    [HideInInspector] public bool inRay = false;

    [Space(10)]
    [Header("Smoke System: ")]
    [SerializeField] private GameObject NW;
    [SerializeField] private GameObject SW;
    [SerializeField] private GameObject SE;
    [SerializeField] private GameObject NE;
    [Space(5)]
    [SerializeField] private float DeactivateDelay = 0.5f;
    

    private PlayerController playerController;
    private bool canSmoke = true;

    private void Start()
    {
        blockToMove = this.gameObject;

        detectionManager = GetComponentInChildren<DetectionManager>();
        playerDetectManager = GetComponentInChildren<PlayerDetectManager>();

        NW.SetActive(false);
        SW.SetActive(false);
        SE.SetActive(false);
        NE.SetActive(false);

    }

    private void Update()
    {
        Transparency();

        if (Input.GetKeyDown(KeyCode.Space) && playerCollide)
        {
            
            GetDirection();
            ForceDirection();
            MoveCheck();

            if (!isMoving && blockToMove != null && canMove && inRay)
            {
                StartCoroutine(blockSound());
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
        ActivateSmoke();
        canSmoke = false;

        if (Vector3.Distance(blockToMove.transform.position, targetPosition) < 0.001f)
        {
            blockToMove.transform.position = targetPosition;
            isMoving = false;
            canSmoke = true;
            StartCoroutine(DeactivateAllSmoke());
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

        if (playerDetectManager.playerObject != null)
        {

            GameObject player = playerDetectManager.playerObject.gameObject;
            PlayerAnimation temp = player.GetComponentInChildren<PlayerAnimation>();


            if (playerDetectManager.NortheastDetected) //NorthEast
            {

                temp.Direction = 8;
            }

            else if (playerDetectManager.NorthwestDetected) //NorthWest
            {

                temp.Direction = 7;
            }

            else if (playerDetectManager.SoutheastDetected) //SouthEast
            {

                temp.Direction = 6;
            }

            else if (playerDetectManager.SouthwestDetected) //SouthWest
            {

                temp.Direction = 5;
            }
        }
    }

    private IEnumerator blockSound()
    {
        blockMoveSound.Play();
      yield return null;
    }

    private void Transparency()
    {
        if (playerDetectManager.playerObject != null)
        {
            Color temp = gameObject.GetComponent<SpriteRenderer>().color;

            if (playerDetectManager.NorthwestDetected || playerDetectManager.NortheastDetected)
            {
               
                temp.a = 190f/255f;
                this.gameObject.GetComponent<SpriteRenderer>().color = temp;
            }
            else
            {
                temp.a = 255f/255f;
                this.gameObject.GetComponent<SpriteRenderer>().color = temp;
            }
        }
    }

    private void ActivateSmoke()
    {
        if (isMoving && canSmoke)
        {
            if (playerDetectManager.NortheastDetected) //NorthEast
            {
                SW.SetActive(true);
                Debug.Log("NE Smoke");
            }

            else if (playerDetectManager.NorthwestDetected) //NorthWest
            {
                SE.SetActive(true);
                Debug.Log("NW Smoke");
            }

            else if (playerDetectManager.SoutheastDetected) //SouthEast
            {
                NW.SetActive(true);
                Debug.Log("SE Smoke");
            }

            else if (playerDetectManager.SouthwestDetected) //SouthWest
            {
                NE.SetActive(true);
                Debug.Log("SW Smoke");
            }
        }

        else if (!isMoving)
        {
            Debug.Log("Deactivation");
            StartCoroutine(DeactivateAllSmoke());
        }
        
    }

    private IEnumerator DeactivateAllSmoke()
    {
       yield return new WaitForSeconds(DeactivateDelay);
        NW.SetActive(false);
        SW.SetActive(false);
        SE.SetActive(false);
        NE.SetActive(false);
    }
}

