using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrDetector : MonoBehaviour
{
    public GameObject targetObject;
    private int detectLayer;

    private void Awake()
    {
        detectLayer = LayerMask.NameToLayer("Interactable");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == detectLayer)
        {
            targetObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == detectLayer)
        {
            targetObject = null;
        }
    }
}
