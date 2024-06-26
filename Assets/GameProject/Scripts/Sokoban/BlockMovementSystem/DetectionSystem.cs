using UnityEngine;

public class DetectionSystem : MonoBehaviour
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
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle") && hit.collider.gameObject != parentObject)
            {
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

        Debug.DrawRay(transform.position, direction * rayLength, color);
    }
}
