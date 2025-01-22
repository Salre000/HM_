using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavimeshSimple : MonoBehaviour
{

    //エージェントとなるオブジェクトのNavMeshAgent格納用 
    private NavMeshAgent agent;

    public float speed = 5.0f;

    public float attackCoolTime = 5.0f;

    public float attackDistance = 20.0f;

    private bool _checkPatroll = false;

    private bool _attakReady = false;

    private GameObject _monster;

    // Start is called before the first frame update
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        agent.speed = speed;
        _monster = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
         agent.destination = _monster.transform.position;
    }


    bool CheckIfEnemyIsPatrolling()
    {
        return _checkPatroll;
    }

    bool CheckAttackDistance()
    {
        if (Vector3.Distance(transform.position, _monster.transform.position) > attackDistance)
        {
            return true;
        }
        return false;
    }

    bool CheckAttackReady()
    {
        return _attakReady;
    }
}
