using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySight : MonoBehaviour
{
    public float sightRange = 10f; // 視認距離
    public float fieldOfViewAngle = 110f; // 視界角度
    public LayerMask playerLayer; // プレイヤーのレイヤー

    private Transform player; // プレイヤーのTransform



    void Start()
    {
        // プレイヤーのTransformを取得
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // 視界内かどうかをチェック
        if (IsPlayerInSight())
        {
            Debug.Log("プレイヤーを発見！");
        }
    }

    bool IsPlayerInSight()
    {
        // プレイヤーとの方向ベクトルを計算
        Vector3 directionToPlayer = player.position - transform.position;

        // プレイヤーが視界角度内にいるかをチェック
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
        if (angleToPlayer < fieldOfViewAngle / 2)
        {
            // プレイヤーが視界角度内にいる場合、レイを飛ばして遮蔽物がないか確認
            float distanceToPlayer = directionToPlayer.magnitude;
            if (distanceToPlayer <= sightRange)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out hit, sightRange, playerLayer))
                {
                    // レイがプレイヤーに当たった場合、視界内にプレイヤーがいる

                    return true;
                }
            }
        }
        return false;
    }
}
