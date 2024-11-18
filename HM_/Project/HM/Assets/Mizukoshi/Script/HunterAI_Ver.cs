using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAI_Ver : MonoBehaviour
{

    public float sightRange = 10f; // 視認距離
    public float fieldOfViewAngle = 110f; // 視界角度
    public LayerMask playerLayer; // プレイヤーのレイヤー

    private Transform player; // プレイヤーのTransform


    private enum State
    {
        Idle=0,
        Search,
        Chase,
        Fighting,
     }

    private State _state;

    private void Start()
    {
        _state = State.Search;
    }


    private void Update()
    {

        // 敵の攻撃をうけた?


        // 敵を見つけている?


        // 敵は攻撃中?

       
        // 距離はd以内?


        // 



        
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Search:

                break;
            case State.Chase:
                break;
            case State.Fighting:
                break;
        }



    }



    bool HitEnemyAttack()
    {
        return false;
    }

    bool EnemyAttackNow()
    {
        return false ;
    }

    // 敵の攻撃の前にいるか
    bool In_Front_Of_EnemyAttack(Vector3 dir)
    {
        return true ;
    }

    // このままだと敵の攻撃が命中するかどうか
    bool CheckHitIntheFuture()
    {
        return false;
    }

    void Rolling()
    {

    }

    void Walk()
    {

    }

    void Attack()
    {

    }

    void Search()
    {

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

    bool IsPlayerInSight(Vector3 []targetPosition)
    {
        for (int i = 0; i < targetPosition.Length; i++)
        {
            // プレイヤーとの方向ベクトルを計算
            Vector3 directionToPlayer = targetPosition[i] - transform.position;

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
        }

        return false;
    }




}
