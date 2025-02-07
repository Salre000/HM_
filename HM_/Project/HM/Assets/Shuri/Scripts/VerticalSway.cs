using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSway : MonoBehaviour
{
    float a = 0;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * Mathf.Sin(a+=0.1f);
    }
}
