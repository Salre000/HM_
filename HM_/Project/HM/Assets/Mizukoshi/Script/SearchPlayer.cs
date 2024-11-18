using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    public float detectionRange = 1000f;  // 索敵範囲
    public float detectionAngle = 60f;  // 視界角度（例えば60度以内のプレイヤーを検出）
    public Transform player;            // プレイヤーのTransform

    void Update()
    {
        // プレイヤーとの距離を取得
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        

        if (distanceToPlayer <= detectionRange)
        {
            // 視界内にプレイヤーがいるか判定（視界角度を考慮）
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

          

            if (angle <= detectionAngle / 2)
            {
                Debug.Log("Player detected!");
            }
        }
    }
}
