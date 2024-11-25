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
    public float speed;
    public LayerMask playerLayer; // プレイヤーのレイヤー
    public float searchRadius; // きょろきょろの視線の範囲
    public float attackDistance;
    public Transform player; // プレイヤーのTransform
    private bool _enemyInsight=false;
    private bool _attackNow=false;

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
            Run();
            Search();
        }

        //if (agent.isStopped)
        //{
            
        //}


        // 敵は攻撃中?
        //if (EnemyAttackNow()&&!_enemyInsight)
        //{
        //   // 敵の前にいるかどうかを判断
           
        //}


        // 距離はd以内?
        if (PlayerToDistance() <= attackDistance)
        {
            // 攻撃アニメーションを流す
            Attack();
        }

        Debug.Log(agent.destination);
        
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
        _attackNow = true;
    }

    void Search()
    {
        if (IsPlayerInSight())
        {
            _state= State.Chase;
            _enemyInsight = true;
        }
       
        if(Vector3.Distance(transform.position,agent.destination) < 1f)
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
        //animationState.normalizedTime >= 0.75f && animationState.IsName("ataka1")
        if (inform.normalizedTime >= 0.75f && inform.IsName("ataka1"))
        {
            AttackAnimationEnd();
        }

        if (agent.isStopped)
        {

        }
    }

    public void AttackAnimationEnd()
    {
        _animator.SetBool("Attack", false);
        _animator.SetBool("AttackFinish", true);
        _attackNow=false;
    }

    public void RunAnimationEnd()
    {
        _animator.SetBool("Walk", false);
        _animator.SetBool("WalkFinish", true);
    }

    public void Run()
    {
        _animator.SetBool("Walk", true);
        _animator.SetBool("WalkFinish", false);
    }

    public void Idle()
    {

    }

    public bool GetAttackState()
    {
        return true;
    }

}
