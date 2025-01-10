using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHitTestAll : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="aaa")
        Debug.Log("Hit");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hane");
    }
}
