using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NotEyeSightMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;  // 目的地（ランダムに移動するための位置）
    public float searchRadius = 10f; // きょろきょろの視線の範囲
    private int currentWaypoint = 0;

    void Start()
    {
        // 初期目的地を設定
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    void Update()
    {
        // 移動先に到達したら次の目的地へ
        if (Vector3.Distance(transform.position, agent.destination) < 1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

  
}
