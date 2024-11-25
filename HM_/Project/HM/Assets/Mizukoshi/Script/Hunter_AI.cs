using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunter_AI : MonoBehaviour
{
    // ① 距離が30以上あるならばナビメッシュによる移動

    // ② 距離が30以下ならばゆっくり移動

    // ③ 敵が攻撃を5回してきたらまたは体力が20以下なら一度離れる

    // ④ 距離が10以下ならば攻撃する。

    public int attackDistance = 5;

    public float speed = 3.0f;

    // モンスターとの距離
    float distance = 0;

    // 攻撃してきた回数
    int attackNum = 0;

    // モンスターのオブジェクト 
    private GameObject _monster;

    //エージェントとなるオブジェクトのNavMeshAgent格納用 
    private NavMeshAgent agent;

    // 待つ時間
    float waitTime = 0;

    private Animator _animator;

    public bool attackNow = false;

    // Start is called before the first frame update
    void Start()
    {
        // モンスターのタグ取得
        _monster = GameObject.FindGameObjectWithTag("Player");

        // ナビの取得
        agent=GetComponent<NavMeshAgent>();

        agent.speed = speed;

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animationState = _animator.GetCurrentAnimatorStateInfo(0);
        // モンスターと自分の距離を測る
        distance =Vector3.Distance(this.transform.position,_monster.transform.position);

        // モンスターと自分の距離が20以上であればナビメッシュによる移動を行う
        if (distance>attackDistance)
        {
            agent.isStopped=false;
            agent.destination = _monster.transform.position;
            waitTime = 0;
        }
        else
        {
            agent.isStopped=true;
            waitTime = 1;
        }

        if (waitTime >= 1)
        {

            if (agent.isStopped)
            {
                // 攻撃のアニメーションを流す。
                _animator.SetBool("Attack", true);
                _animator.SetBool("AttackFinish", false);
            }
        }
        if (animationState.normalizedTime >= 0.01f && animationState.IsName("ataka1"))
        {
            attackNow = true;
        }

        if (animationState.normalizedTime >= 0.75f && animationState.IsName("ataka1"))
        {
            AttackAnimationEnd();
            attackNow = false;
        }
        if (!agent.isStopped)
        {
            // 走るアニメーションを再生する
            _animator.SetBool("Walk",true );
            _animator.SetBool("WalkFinish",false );
        }
        else
        {
            // 走るアニメーションを止める
            _animator.SetBool("Walk", false);
            _animator.SetBool("WalkFinish", true);
        }
    }

    public void AttackAnimationEnd()
    {
        _animator.SetBool("Attack", false);
        _animator.SetBool("AttackFinish",true );
    }
    
    public bool GetAttackState()
    {
        return attackNow;
    }
}
