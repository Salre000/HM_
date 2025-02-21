using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoFor : MonoBehaviour
{
    private float rate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(rate*Vector3.forward*Time.deltaTime);
    }
}
