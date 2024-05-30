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

    [HideInInspector] public GameObject playerObject;

    void Update()
    {
        GetPlayer();

        NortheastDetected = Northeast.GetComponent<PlayerDetectionSystem>().detect;
        SoutheastDetected = Southeast.GetComponent<PlayerDetectionSystem>().detect;
        SouthwestDetected = Southwest.GetComponent<PlayerDetectionSystem>().detect;
        NorthwestDetected = Northwest.GetComponent<PlayerDetectionSystem>().detect;
    }

    private void GetPlayer()
    {
        if (NortheastDetected)
        {
            playerObject = Northeast.GetComponent<PlayerDetectionSystem>().PlayerObject;
        }

        else if (NorthwestDetected)
        {
            playerObject = Northwest.GetComponent<PlayerDetectionSystem>().PlayerObject;
        }

        else if (SoutheastDetected)
        {
            playerObject = Southeast.GetComponent<PlayerDetectionSystem>().PlayerObject;
        }

        else if (SouthwestDetected)
        {
            playerObject = Southwest.GetComponent<PlayerDetectionSystem>().PlayerObject;
        }
    }
}
