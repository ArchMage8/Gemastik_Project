using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    [HideInInspector]public float currAngle;

    public bool isMoving = false;
    [HideInInspector] public int DirectionalInt;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Checker();

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical") * 0.5f));
        moveVelocity = moveInput.normalized * moveSpeed;

        if (moveInput != Vector2.zero)
        {
            isMoving = true;
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            currAngle = angle;
        }

        else
        {
            isMoving= false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Checker()
    {
        int tempAngle = Mathf.CeilToInt(currAngle);

        if (tempAngle == 90)
        {
            DirectionalInt = 1;
        }

        else if (tempAngle == -90)
        {
            DirectionalInt = 2;
        }
        
        else if (tempAngle == 0)
        {
            DirectionalInt = 3;
        }

        else if (tempAngle == 180)
        {
            DirectionalInt = 4;
        }

        else if (tempAngle == 27)
        {
            DirectionalInt = 5;
        }

        else if (tempAngle == 154)
        {
            DirectionalInt = 6;
        }

        else if (tempAngle == -26)
        {
            DirectionalInt = 7;
        }

        else if (tempAngle == -153)
        {
            DirectionalInt = 8;
        }
    }
}
