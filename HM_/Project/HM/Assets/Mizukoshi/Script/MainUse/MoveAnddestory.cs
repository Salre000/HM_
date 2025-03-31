using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnddestory : MonoBehaviour
{
    private float t;
    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            t += Time.deltaTime;
            if (t > 5.0f)
            {
                t = 0;
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            t = 0;
        }
    }
  
}
