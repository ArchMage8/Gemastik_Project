using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public Camera Camera;
    public bool isPreviewing = false;
    public float XOffset = 5f;
    [HideInInspector] public Vector3 camInitial;
    public float targetZoom = 3.6f;
    public float Zoomrate = 0.5f;
    [HideInInspector] public float initialZoom;

    private void Start()
    {
        camInitial = Camera.transform.position;
        initialZoom = Camera.orthographicSize;
    }
}
