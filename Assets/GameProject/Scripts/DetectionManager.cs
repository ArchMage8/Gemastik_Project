using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    [Header ("Detection Objects")]
    public GameObject Northeast;
    public GameObject Southeast;
    public GameObject Southwest;
    public GameObject Northwest;

    [Header("Detection States")]
    public bool NortheastDetected;
    public bool SoutheastDetected;
    public bool SouthwestDetected;
    public bool NorthwestDetected;

    void Update()
    {
        NortheastDetected = Northeast.GetComponent<DetectionSystem>().detect;
        SoutheastDetected = Southeast.GetComponent<DetectionSystem>().detect;
        SouthwestDetected = Southwest.GetComponent<DetectionSystem>().detect;
        NorthwestDetected = Northwest.GetComponent<DetectionSystem>().detect;
    }
}
