using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunter_AI_Base_Test : MonoBehaviour
{
    // �@ 距離が30以上あるならばナビメッシュによる移動

    // �A 距離が30以下ならばゆっくり移動

    // �B 敵が攻撃を5回してきたらまたは体力が20以下なら一度離れる

    // �C 距離が10以下ならば攻撃する。

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

    private bool _fight = false;

    private bool readyAttack = false;

    private bool _deathAnimationNow = false;

    public bool deathAnimationFinish = false;

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
            _animator.SetBool("isDead", true);
        }

        if (_deathAnimationNow)
        {
            if (animationState.normalizedTime >= 0.75f && animationState.IsName("death2"))
            {
                // 終了検知
                deathAnimationFinish = true;
                _deathAnimationNow = false;
                _animator.SetBool("isDead", false);
                _animator.SetBool("isDeadFinish", true);
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
            _fight = true;
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

    public bool CheckDie()
    {
        return true;//this.gameObject.GetComponent<HunterHPManager>().GetHP() <= 0;
    }

    /// <summary>
    /// 探索関数
    /// </summary>
    /// <param name="list"></param> 巡回する位置の配列
    public void Search(Vector3[] list)
    {

    }

    /// <summary>
    /// 攻撃できる距離にいるか
    /// </summary>
    /// <param name="acceptDistance"></param>
    /// <returns>攻撃できる距離ならtrue,できないならばfalse</returns>
    public bool CheckAttackDistance(float acceptDistance, GameObject AIType)
    {
        float calculate = Vector3.Distance(_monster.transform.position, AIType.transform.position);
        return calculate < acceptDistance;
    }

    //-------------------------------------------------------------------------
    //                           行動関係関数
    //-------------------------------------------------------------------------

    /// <summary>
    /// 
    /// </summary>
    public void Attack()
    {

    }
    /// <summary>
    /// 追跡関数
    /// </summary>
    public void Chase()
    {

    }



    //-------------------------------------------------------------------------
    //                     アニメーション関係関数
    //-------------------------------------------------------------------------

    /// <summary>
    /// 現在のアニメーションの状態を取得
    /// </summary>
    /// <returns></returns>
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

    // 攻撃アニメーション再生関数
    public void AttackAnimation()
    {

    }

    // 走るアニメーション再生関数
    public void RunAnimation()
    {

    }

    // 死亡アニメーション再生関数
    public void DeathAnimation()
    {

    }

    // 怯みアニメーション再生関数
    public void FlatterAnimation()
    {

    }
}
