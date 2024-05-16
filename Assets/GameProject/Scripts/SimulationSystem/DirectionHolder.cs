using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionHolder : MonoBehaviour
{
    public float direction;
    public enum Type 
    { Horizontal, 
      Vertical,
      Null
    }

    public Type currentTargetType;
}
