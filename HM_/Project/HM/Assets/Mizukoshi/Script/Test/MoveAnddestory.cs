using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnddestory : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject,5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
