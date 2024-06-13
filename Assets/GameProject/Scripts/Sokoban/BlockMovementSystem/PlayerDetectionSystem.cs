using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionSystem : MonoBehaviour
{
    public enum RayColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }

    public RayColor rayColor;
    public float directionAngle = 0f;
    public float rayLength = 5f;
    public GameObject parentObject;

    [HideInInspector] public GameObject PlayerObject;

    public bool detect = false;

    void Update()
    {
        CastRay();
    }

    void CastRay()
    {
        Vector2 direction = new Vector2(Mathf.Cos(directionAngle * Mathf.Deg2Rad), Mathf.Sin(directionAngle * Mathf.Deg2Rad));

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, rayLength);

        detect = false; // Reset detect

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Player") && hit.collider.gameObject != parentObject)
            {
                PlayerObject = hit.collider.gameObject;
                detect = true;
                break;
            }
        }

        // Draw the ray
        DrawRay(direction);
    }

    void DrawRay(Vector2 direction)
    {
        Color color = Color.white;

        switch (rayColor)
        {
            case RayColor.Red:
                color = Color.red;
                break;
            case RayColor.Green:
                color = Color.green;
                break;
            case RayColor.Blue:
                color = Color.blue;
                break;
            case RayColor.Yellow:
                color = Color.yellow;
                break;
        }

       
    }
}
