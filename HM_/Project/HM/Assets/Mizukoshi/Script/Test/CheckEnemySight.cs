using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemySight : MonoBehaviour
{
    public float viewAngle = 60f; // 視野角度（円錐の角度）
    public float viewDistance = 10f; // 視野の距離
    public Transform player; // プレイヤーのTransform

    void Update()
    {
        OnDrawGizmos();
        if (IsPlayerInSight())
        {
            Debug.Log("Player detected!");
        }
    }

    // 視野内にプレイヤーがいるかチェックする関数
    bool IsPlayerInSight()
    {
        Vector3 toPlayer = player.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude;

        // プレイヤーが視野の範囲内か、距離が許容されているかチェック
        if (distanceToPlayer <= viewDistance)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, toPlayer);

            // プレイヤーが視野角度内にいるかどうか
            if (angleToPlayer <= viewAngle / 2f)
            {
                // プレイヤーが視野内にいる場合は、さらにRaycastで障害物がないかをチェック
                RaycastHit hit;
                if (Physics.Raycast(transform.position, toPlayer.normalized, out hit, viewDistance))
                {
                    if (hit.transform == player)
                    {
                        return true; // プレイヤーが視界内にいる
                    }
                }
            }
        }

        return false; // プレイヤーが視界外にいる
    }
    void OnDrawGizmos()
    {
        // 視野の範囲を円錐として描画
        Gizmos.color = Color.green;
        Vector3 forward = transform.forward * viewDistance;
        Vector3 left = Quaternion.Euler(0, -viewAngle / 2f, 0) * forward;
        Vector3 right = Quaternion.Euler(0, viewAngle / 2f, 0) * forward;

        Gizmos.DrawLine(transform.position, transform.position + left);
        Gizmos.DrawLine(transform.position, transform.position + right);
        Gizmos.DrawLine(transform.position + left, transform.position + right);
    }
}
