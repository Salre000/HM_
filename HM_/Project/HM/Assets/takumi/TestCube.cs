using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestCube : MonoBehaviour
{
    private NavMeshAgent _agent;

    [SerializeField ]GameObject _gameObject;

    public void Start()
    {
        _agent=this.gameObject.AddComponent<NavMeshAgent>();
        _agent.speed = 0.5f;


    }

    public void FixedUpdate()
    {

        _agent.destination = _gameObject.transform.position;

    }


}
