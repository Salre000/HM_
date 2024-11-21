using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{
    Vector3 MoveVec;

    float StartUpVec = 0.05f;
    float DVec = -0.001f;

    public void SetMoveVec(Vector3 Vec) { MoveVec = Vec; } 

    // Start is called before the first frame update
    void Start()
    {
       Destroy(gameObject,10);
    }

    private void FixedUpdate()
    {
        this.transform.position+=MoveVec/20;

        this.transform.position += new Vector3(0,StartUpVec,0);

        StartUpVec += DVec;
        
    }

}
