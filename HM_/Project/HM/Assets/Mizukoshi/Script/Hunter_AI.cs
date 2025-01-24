using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class Hunter_AI : MonoBehaviour
{


    // ① 距離が30以上あるならばナビメッシュによる移動

    // ② 距離が30以下ならばゆっくり移動

    // ③ 敵が攻撃を5回してきたらまたは体力が20以下なら一度離れる

    // ④ 距離が10以下ならば攻撃する。

     int attackDistance = 2;

    public float speed = 3.0f;

    public float AttackCoolTime;

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

    private bool _fight=false;

    private bool readyAttack=false;

    private bool _deathAnimationNow=false;

    public bool deathAnimationFinish=false;

    private bool restrainingFlag = false;

    AnimatorStateInfo animationState;

    // Start is called before the first frame update
    void Start()
    {
        // モンスターのタグ取得
        _monster = GameObject.FindGameObjectWithTag("Player");

        // ナビの取得
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animationState = _animator.GetCurrentAnimatorStateInfo(0);
        // モンスターと自分の距離を測る
        distance = Vector3.Distance(this.transform.position, _monster.transform.position);

        if (CheckDie())
        {
            _deathAnimationNow = true;
            AttackAnimationEnd();
            // 走るアニメーションを止める
            _animator.SetBool("Walk", false);
            _animator.SetBool("WalkFinish", true);
            _animator.SetBool("isDeadFinish", false);
            _animator.SetBool("isDead",true);
        }

        if (_deathAnimationNow)
        {
            if (animationState.normalizedTime >= 0.75f&&animationState.IsName("death2"))
            {
                // 終了検知
                deathAnimationFinish = true;
                _deathAnimationNow = false;
                _animator.SetBool("isDead", false);
                _animator.SetBool("isDeadFinish",true);
            }
            return;
        }

        // モンスターと自分の距離が20以上であればナビメッシュによる移動を行う
        if (distance > attackDistance)
        {
            // 走るアニメーションを再生する
            _animator.SetBool("Walk", true);
            _animator.SetBool("WalkFinish", false);
            agent.isStopped = false;
            agent.destination = _monster.transform.position;
            _fight = true;
            return;
        }
        else
        {
            agent.isStopped = true;
            _fight=true;
        }

        if (_fight)
        {

            if (agent.isStopped)
            {
                if (!attackNow)
                {
                    waitTime += Time.deltaTime;
                }
                if (waitTime > AttackCoolTime) 
                {
                    // 攻撃のアニメーションを流す。
                    _animator.SetBool("Attack", true);
                    _animator.SetBool("AttackFinish", false);
                    waitTime = 0;
                }
            }
        }
        if (animationState.normalizedTime >= 0.01f && animationState.IsName("ataka1"))
        {
            attackNow = true;
        }

        if (animationState.normalizedTime >= 0.75f && animationState.IsName("ataka1"))
        {
            AttackAnimationEnd();
        }
        if (!animationState.IsName("ataka1")) attackNow = false;
        if (!agent.isStopped)
        {
            // 走るアニメーションを再生する
            _animator.SetBool("Walk", true);
            _animator.SetBool("WalkFinish", false);
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
        _animator.SetBool("AttackFinish", true);
    }
    
    public bool GetAttackState()
    {
        return attackNow;
    }

    bool CheckDie()
    {
        return this.gameObject.GetComponent<HunterHPManager>().GetHP()<=0;
    }

    public AnimatorStateInfo GetAnimState()
    {
        return animationState;
    }

    // 拘束状態の開始 アニメーションの開始
    public void StartRestraining()
    {
        //_animator.SetBool("RestrainFlag", true);
    }

    // 拘束状態の終了　アニメーションの終了
    public void StopRestraining()
    {
        //_animator.SetBool("RestraingFlag", false);
    }
    
    // 攻撃関数
    public void Attack()
    {

    }

    // 走る関数
    public void Run()
    {

    }
    
    // 死亡関数
    public void Death()
    {

    }

    /// <summary>
    /// 探索関数
    /// </summary>
    /// <param name="list"></param> 巡回する位置の配列
    public void Search(Vector3[] list)
    {

    }

    /// <summary>
    /// 距離検知関数
    /// </summary>
    /// <param name="acceptDistance"></param>
    /// <returns></returns>
    public bool CheckAttackDistance(float acceptDistance)
    {
        return false;
    }



}
