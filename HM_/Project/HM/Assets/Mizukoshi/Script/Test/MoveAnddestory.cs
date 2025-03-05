using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnddestory : MonoBehaviour
{
    private void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
    }
}
