using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinpleMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            this.transform.Translate(0, 0, 1.0f / 50.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0, 0, -1.0f / 50.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-1.0f/50, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(1.0f/50, 0, 0);
        }
    }
}
