using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antiRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0);
    }
}
