using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{

    Collider co;
    // Start is called before the first frame update
    void Start()
    {
        co = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            co.enabled = false;
        }
    }
}
