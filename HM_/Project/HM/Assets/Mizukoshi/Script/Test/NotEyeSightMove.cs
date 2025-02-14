using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NotEyeSightMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;  // �ړI�n�i�����_���Ɉړ����邽�߂̈ʒu�j
    public float searchRadius = 10f; // ����낫���̎����͈̔�
    private int currentWaypoint = 0;

    void Start()
    {
        // �����ړI�n��ݒ�
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    void Update()
    {
        // �ړ���ɓ��B�����玟�̖ړI�n��
        if (Vector3.Distance(transform.position, agent.destination) < 1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

  
}
