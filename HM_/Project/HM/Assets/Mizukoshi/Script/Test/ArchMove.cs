using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchMove : MonoBehaviour
{
    public GameObject weapon;

    private bool moveFlag=false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveFlag)
        {
            weapon.transform.Translate(Vector3.forward);
        }
    }

    public void TestMover()
    {
        moveFlag = true;
    }
}
