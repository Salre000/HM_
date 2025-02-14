using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPlayer : MonoBehaviour
{

    NavMeshAgent nav;
    private GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        nav=GetComponent<NavMeshAgent>();
        g = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        nav.destination = g.transform.position;
    }
}
