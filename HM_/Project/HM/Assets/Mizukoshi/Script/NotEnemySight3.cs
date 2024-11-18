using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NotEnemySight3 : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public float searchRadius = 10f;
    private int currentWaypoint = 0;

    private NotEnemySight2 headMovement; // 顔の動きを制御するスクリプト

    void Start()
    {
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
        }

        // HeadMovementスクリプトの参照を取得
        headMovement = GetComponent<NotEnemySight2>();
    }

    void Update()
    {
        // 目的地に到達したら次のウェイポイントに移動
        if (Vector3.Distance(transform.position, agent.destination) < 1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypoint].position);
        }

        // 索敵と顔をきょろきょろさせる
        //SearchForPlayer();

        // 顔の動きを更新
        headMovement.Update();
    }

    void SearchForPlayer()
    {
        // プレイヤーを検出する範囲を設定
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Debug.Log("Player detected!");
                break;
            }
        }
    }
}
