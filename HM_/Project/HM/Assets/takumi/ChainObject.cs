using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainObject : MonoBehaviour
{
    [SerializeField] GameObject StartChain;
    [SerializeField] GameObject EndChain;

    public void SetUp(Vector3 start,Vector3 end) 
    {
        StartChain.transform.position = start;
        EndChain.transform.position = end;


    }
}
