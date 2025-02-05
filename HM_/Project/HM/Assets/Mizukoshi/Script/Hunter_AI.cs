using Den.Tools.GUI;
using MapMagic.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// ハンターの基底クラス
/// </summary>
public abstract class Hunter_AI : MonoBehaviour
{
    // モンスターのオブジェクト
    private GameObject _monster;

    private GameObject[] _monsters;

    // トラップリスト
    private List<GameObject> _trapList;

    // トラップ感知
    public SpiderTrapPool trap;

    // ナビメッシュ
    protected NavMeshAgent _agent;

    // アニメーションの状態
    AnimatorStateInfo animationState;

    // アニメーションコントローラー
    private Animator _animator;

    // ダメージクラス
    public Damage damage;

    // モンスターの位置を発見したかどうかのフラグ
    public bool monsterDisplay = false;

    // ハンターマネージャー
    public HunterManager manager;

    public HPManager hpManager;

    private GameObject terrian;

    public int HP = 100;

    // 待機時間
    private float waitSecond = 1.0f;

    // 待機経過時間
    private float elapsedTime = 0;

    // 待機フラグ
    private bool waitFlag = false;

    // 攻撃準備ができているか
    protected bool attackReady = true;

    // 時間経過用変数
    private float coolTime = 0.0f;

    // 攻撃のクールタイム
    private float _attackCoolTime = 2.0f;

    // 攻撃距離
    private float _attackDistance = 1.0f;

    // 視野角度
    private float _viewAngle;

    // 視野距離
    private float _viewLength;

    // 回避頻度
    private float _AvoidRatio;

    private bool deathAnimNow = false;

    protected enum eStatus
    {
        None,
        Rest,
        Max,
    };

    protected eStatus status;

    protected Vector3[] searchPosition =
    {
        new Vector3(20.0f,0.5f,44.0f),
        new Vector3(74.0f,0.5f,14.0f),
        new Vector3(74,2.5f,75),
    };

    protected int searchPointIndex = 0;

    protected bool CloclWise = false;

    private Collider myCollider;



    //-------------------------------------------
    //           Unity標準関数
    //-------------------------------------------

    // Start is called before the first frame update
    public virtual void Start()
    {
        // モンスターのタグ取得
        _monster = GameObject.FindGameObjectWithTag("Player");
        _monsters = GameObject.FindGameObjectsWithTag("Player");
        manager = new HunterManager();
        _animator = GetComponent<Animator>();
        hpManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
        status = eStatus.None;
        _agent = GetComponent<NavMeshAgent>();
        _trapList = SpiderTrapPool.instance?.GetTraps();
       myCollider = GetComponent<Collider>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerAttack") return;

        if (other.GetComponent<Damage>() == null) return;

        HitEffectManager.instance.HitEffectShow(other.transform.position, HitEffectManager.CharacterType.Monster);
        damage = other.GetComponent<Damage>();

        hpManager.HunterDamage(damage.GetDamage(), this.GetHunterID());
    }

    //private void Update()
    //{

    //    if (waitFlag)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        if(elapsedTime > waitSecond)
    //        {
    //            elapsedTime = 0;
    //            waitFlag = false;
    //        }
    //        return;
    //    }

    //    // 拘束状態ならスキップ
    //    if (CheckRest()) return;

    //    // モンスターを見つけているなら探索してスキップ
    //    if (!monsterDisplay)
    //    {
    //        // 変化
    //        Search();
    //        return;
    //    }

    //    // モンスターの攻撃がとんできているかどうか
    //    if (CheckMonsterAttack())
    //    {
    //        int randomNum = Random.Range(0, 10);
    //        if (randomNum > _AvoidRatio)
    //        {
    //            Avoid();
    //            return;
    //        }
    //    }


    //    // モンスターへの攻撃範囲にいるならば
    //    if (!CheckAttackDistance(this.gameObject))
    //    {
    //        TurnMonser();
    //        // 攻撃準備ができているのならば
    //        if (attackReady)
    //        {
    //            // 攻撃
    //            Attack();
    //        }
    //        else
    //        {
    //            // 後退
    //            Back();
    //        }
    //    }
    //    else
    //    {

    //    }



    //}

    //------------------------------------------------
    //                    処理
    //------------------------------------------------
    /// <summary>
    /// 目的地の設定
    /// </summary>
    /// <param name="pos"></param>
    public void SetDestination(Vector3 pos)
    {
        _agent.destination = pos;
    }

    /// <summary>
    /// 探索関数
    /// </summary>
    public virtual void Search()
    {

    }

    // モンスターの発見した時に呼ぶ関数
    public void DisappearMonster()
    {
        manager.SetDisapper();
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

    protected bool CheckAttackDistance(GameObject AIType)
    {
        float calculate = Vector3.Distance(_monster.transform.position, AIType.transform.position);
        return calculate < _attackDistance;
    }

    public bool CheckKeepDistance(float acceptDistance, GameObject AIType)
    {

        float calculate = Vector3.Distance(_monster.transform.position, AIType.transform.position);
        //Debug.Log(calculate);
        return calculate > acceptDistance;
    }
    public bool CheckKeepDistance(Vector3 pos, GameObject AIType, float distance)
    {
        return Vector3.Distance(pos, AIType.transform.position) < distance;
    }
    /// <summary>
    /// モンスターが見えるかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsMonsterInSight()
    {
        Vector3 start = this.gameObject.transform.position;
        start.y += 1.75f;
        RaycastHit hit;
        if (Physics.Raycast(start, transform.forward, out hit, 20))
        {
            PlayerStatus ste = hit.transform.gameObject.GetComponentInParent<PlayerStatus>();
            if (ste != null) return true;
        }
        return false;
    }

    // モンスターが視界内にいるかどうかの関数
    public bool ObjectInsightPlayer()
    {
        Vector3 startPos = this.gameObject.transform.position;
        Vector3 monsterPos = _monster.transform.position;
        Vector3 playerToTarget = (_monster.transform.position - startPos).normalized;
        Vector3 lookDir = transform.TransformDirection(Vector3.forward).normalized;
        RaycastHit hit;

        if (Physics.Raycast(startPos, playerToTarget * _viewLength, out hit, _viewLength))
        {
            // 当たったRayがモンスターでないなら飛ばす
            PlayerStatus ste = hit.transform.gameObject.GetComponentInParent<PlayerStatus>();
            if (ste != null) return false;

            // かつ視野角が
            float angle = Vector3.Angle(playerToTarget, lookDir);
            if (angle <= _viewAngle / 2) return true;

        }
        return false;
    }

    protected void SetAttackCoolTime(float attackCoolTime)
    {
        _attackCoolTime = attackCoolTime;
    }

    protected void SetAttackDistance(float attackDistance)
    {
        _attackDistance = attackDistance;
    }

    protected void SetViewAngle(float viewAngle)
    {
        _viewAngle = viewAngle;
    }

    protected void SetViewLength(float length)
    {
        _viewLength = length;
    }

    protected void SetAvoidRatio(float avoidRatio)
    {
        _AvoidRatio = avoidRatio;
    }

    private void WaitAttackCoolTime()
    {
        if (attackReady) return;

        coolTime += Time.deltaTime;
        if (coolTime > _attackCoolTime)
        {
            coolTime = 0;
            attackReady = true;
        }
    }
    /// <summary>
    /// モンスターが攻撃しているかどうか
    /// </summary>
    /// <returns></returns>
    bool CheckMonsterAttack()
    {
        return true;
    }

    // モンスターの正面の位置を取得
    protected Vector3 GetMonsterFrontPosition()
    {
        float offsetX = 0;
        float offsetY = 0;
        float offsetZ = 2.0f;
        Vector3 newPos = GetMonster().transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        offset = GetMonster().transform.rotation * offset;
        newPos = newPos + offset;
        return newPos;
    }

    // モンスターの右の位置を取得
    protected Vector3 GetMonsterRightPosition()
    {
        float offsetX = 2.0f;
        float offsetY = 0;
        float offsetZ = 0f;
        Vector3 newPos = GetMonster().transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        offset = GetMonster().transform.rotation * offset;
        newPos = newPos + offset;
        return newPos;
    }

    // モンスターの左の位置を取得
    protected Vector3 GetMonsterLeftPosition()
    {
        float offsetX = -2.0f;
        float offsetY = 0;
        float offsetZ = 0f;
        Vector3 newPos = GetMonster().transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        offset = GetMonster().transform.rotation * offset;
        newPos = newPos + offset;
        return newPos;
    }

    protected Vector3 GetMonsterBackPosition()
    {
        float offsetX = 0f;
        float offsetY = 0;
        float offsetZ = -2.0f;
        Vector3 newPos = GetMonster().transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        offset = GetMonster().transform.rotation * offset;
        newPos = newPos + offset;
        return newPos;
    }

    // やや後ろに下がる位置を取得
    protected Vector3 GetBackPosition()
    {
        Vector3 dir = this.transform.position - _monster.transform.position;
        dir *= 3;
        float offsetX = dir.x;
        float offsetY = dir.y;
        float offsetZ = dir.z;
        Vector3 newPos = GetMonster().transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        offset = GetMonster().transform.rotation * offset;
        newPos = newPos + offset;
        return newPos;
    }

    void CheckAvoid()
    {
        
        animationState=GetAnimState();

        // 回避アニメーションかどうか
        if(animationState.IsName("アーマチュア|Avoid") && animationState.normalizedTime >= 0.5f && animationState.normalizedTime < 0.8)
        {
            
        }
    }

    //-------------------------------------------------------------------------
    //                           行動関係関数
    //-------------------------------------------------------------------------

    /// <summary>
    /// 
    /// </summary>
    public void Attack()
    {
        attackReady = false;
        AttackAnimation();
        Debug.Log("攻撃");
    }
    /// <summary>
    /// 追跡関数
    /// </summary>
    public virtual void Chase()
    {
        _agent.destination = _monster.transform.position;
    }

    public void Run()
    {
        // アニメーションを流す
    }

    public void Avoid()
    {
        // アニメーションを流す
    }

    /// <summary>
    /// 少し下がる関数
    /// </summary>
    public void Back()
    {

    }

    public void Death()
    {
        DeathAnimation();
        deathAnimNow = true;
        // アニメーションイベントにより終了後リスポーンさせる
    }

    public void DeathFinish()
    {
        manager.Respawn(GetHunterID());
    }

    //
    void TurnMonser()
    {
        this.transform.LookAt(GetMonster().transform.position);
    }

    // 罠情報の更新
    private void UpdateTrapInformation()
    {

    }

    public void WaitForCount(float length = 1)
    {
        waitSecond = length;
        if (waitFlag) return;
        waitFlag = true;

    }

    //-------------------------------------------------------------------------
    //                     アニメーション関係関数
    //-------------------------------------------------------------------------

    int restrainCount = 0;

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
        restrainCount++;
        status = eStatus.Rest;
        _agent.enabled = false;
        _animator.SetTrigger("FlatterStartTrigger");
    }

    // 拘束状態の終了　アニメーションの終了
    public void StopRestraining()
    {
        restrainCount--;
        if (restrainCount < 0) return;
        status = eStatus.None;
        _agent.enabled = true;
        _animator.SetTrigger("FlatterFinishTrigger");
    }

    // 攻撃アニメーション再生関数
    public void AttackAnimation()
    {
        _animator.SetTrigger("AttackTrigger");
    }

    // 走るアニメーション再生関数
    public void RunAnimation()
    {

    }

    // 死亡アニメーション再生関数
    public void DeathAnimation()
    {
        _animator.SetTrigger("Death");
        _agent.enabled = false;

    }

    // 怯みアニメーション再生関数
    public void FlatterAnimation()
    {

    }

    public void AvoidAnimation()
    {

    }


    // モンスターオブジェクトの取得
    public GameObject GetMonster()
    {
        return _monster;
    }


    public bool GetFlat()
    {
        return (_animator.GetBool("FlatterStartTrigger") && !_animator.GetBool("FlatterFinishTrigger"));
    }

    public int GetHunterID()
    {
        return this.GetComponent<Hunter_ID>().GetHunterID();
    }

    public void ResetAnimation()
    {
        _agent.enabled = true;
        _animator.SetTrigger("Reset");
    }

    // 拘束状態であるかどうか
    public bool CheckRest()
    {
        if (status == eStatus.Rest) return true;
        return false;
    }





}
