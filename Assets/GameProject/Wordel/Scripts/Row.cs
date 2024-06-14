using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Row : MonoBehaviour
{
    public Box[] tiles { get; private set; }

    private void Awake()
    {
        tiles = GetComponentsInChildren<Box>();
    }
}
