using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPlayer : MonoBehaviour
{

    NavMeshAgent nav;
    [SerializeField]private GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        nav=GetComponent<NavMeshAgent>();
       // g = GameObject.Find("Cubes");

        //g = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(g.transform.position);
    }
}
