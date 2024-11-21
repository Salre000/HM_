using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterAI_Ver : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3[] distination=new Vector3[4];
    private int distinationNum = 0;
    public float sightRange = 10f; // 視認距離
    public float fieldOfViewAngle = 110f; // 視界角度
    public float speed = 5.0f;
    public LayerMask playerLayer; // プレイヤーのレイヤー
    public float searchRadius = 10f; // きょろきょろの視線の範囲
    public float attackDistance = 2.0f;
    private Transform player; // プレイヤーのTransform
    private bool _enemyInsight=false;
    private Animator _animator;

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
        _animator = GetComponent<Animator>();
        agent.destination=distination[0];
        agent.speed=speed;
    }


    private void Update()
    {
        AnimatorStateInfo animationState = _animator.GetCurrentAnimatorStateInfo(0);

        // 敵の攻撃をうけた?
        if (HitEnemyAttack())
        {
            agent.destination = player.position;
        }

        // 敵を見つけている?
        if (_enemyInsight)
        {
            agent.destination=player.position;
        }
        else
        {
            Search();
        }


        // 敵は攻撃中?
        if (EnemyAttackNow()&&!_enemyInsight)
        {
           // 敵の前にいるかどうかを判断
           
        }


        // 距離はd以内?
        if (PlayerToDistance() <= attackDistance)
        {
            // 攻撃アニメーションを流す
            Attack();
        }


        // 



        
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
        // 攻撃のアニメーションを流す。
        _animator.SetBool("Attack", true);
        _animator.SetBool("AttackFinish", false);
    }

    void Search()
    {
        if (IsPlayerInSight())
        {
            _state= State.Chase;
            return;
        }
       
        if(Vector3.Distance(transform.position, agent.destination) < 1f)
        {
            distinationNum++;
            agent.destination = distination[distinationNum];
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

    float PlayerToDistance()
    {
        float distance = 0;
        distance=Vector3.Distance(player.transform.position,this.transform.position);
        return distance;
    }

    void AnimationFinishInform(AnimatorStateInfo inform)
    {
         
    }

    public void AttackAnimationEnd()
    {
        _animator.SetBool("Attack", false);
        _animator.SetBool("AttackFinish", true);
    }

}
