using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectManager : MonoBehaviour
{
    [Header("Detection Objects")]
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
        NortheastDetected = Northeast.GetComponent<PlayerDetectionSystem>().detect;
        SoutheastDetected = Southeast.GetComponent<PlayerDetectionSystem>().detect;
        SouthwestDetected = Southwest.GetComponent<PlayerDetectionSystem>().detect;
        NorthwestDetected = Northwest.GetComponent<PlayerDetectionSystem>().detect;
    }
}
