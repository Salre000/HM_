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
    private NavMeshAgent _agent;

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

    public int HP = 100;

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

    protected enum eStatus
    {
        None,
        Rest,
        Max,
    };

    protected eStatus status;

    public Transform[] searchPosition=new Transform[4];

    

    //-------------------------------------------
    //           Unity標準関数
    //-------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        // モンスターのタグ取得
        _monster = GameObject.FindGameObjectWithTag("Player");
        _monsters = GameObject.FindGameObjectsWithTag("Player");
        _animator = GetComponent<Animator>();
        hpManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
        status=eStatus.None;
        _agent = GetComponent<NavMeshAgent>();
        GameObject[] searchObj = GameObject.FindGameObjectsWithTag("AAA");
        for (int i = 0; i < searchObj.Length; i++)
        {
            searchPosition[i] = searchObj[i].GetComponent<Transform>();
        }
        _trapList = trap.GetTraps();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerAttack") return;

        if (other.GetComponent<Damage>() == null) return;

        HitEffectManager.instance.HitEffectShow(other.transform.position, HitEffectManager.CharacterType.Monster);
        damage = other.GetComponent<Damage>();

        hpManager.HunterDamage(damage.GetDamage(), this.GetHunterID());
    }

    private void Update()
    {
        // トラップ情報の取得
        

        // 拘束状態ならスキップ
        if (CheckRest())return;

        // モンスターを見つけているなら探索してスキップ
        if (!monsterDisplay)
        {
            Search();
            return;
        }

        // モンスターの攻撃がとんできているかどうか
        if (CheckMonsterAttack())
        {
            int randomNum=Random.Range(0, 10);
            if (randomNum > _AvoidRatio)
            {
                Avoid();
                return;
            }
        }


        // モンスターへの攻撃範囲にいるならば
        if (!CheckAttackDistance(this.gameObject))
        {
            TurnMonser();
            // 攻撃準備ができているのならば
            if (attackReady)
            {
                // 攻撃
                Attack();
            }
            else
            {
                // 後退
                Back();
            }
        }
        else
        {

        }



    }

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
       if(ObjectInsightPlayer())DisappearMonster();
    }

    // モンスターの発見した時に呼ぶ関数
    public void DisappearMonster()
    {
        //manager.SetDisapper();
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
    /// <summary>
    /// モンスターが見えるかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsMonsterInSight()
    {
        Vector3 start=this.gameObject.transform.position;
        start.y += 0.75f;
        RaycastHit hit;
        if (Physics.Raycast(start, transform.forward, out hit, 20))
        {
            PlayerStatus ste = hit.transform.gameObject.GetComponentInParent<PlayerStatus>();
            if (ste != null) return true;
        }
        return false;
    }

    // モンスターが視界内にいるかどうかの関数
    private bool ObjectInsightPlayer()
    {
        Vector3 startPos= this.gameObject.transform.position;
        Vector3 monsterPos=_monster.transform.position;
        Vector3 playerToTarget= _monster.transform.position-startPos;
        RaycastHit hit;

        if(Physics.Raycast(startPos,transform.forward,out hit, _viewLength))
        {
            PlayerStatus ste = hit.transform.gameObject.GetComponentInParent<PlayerStatus>();
            if (ste != null) return false;

            float angleToPlayer=playerToTarget.magnitude;
            if (angleToPlayer <= _viewAngle) return false;

            if (Vector3.Distance(startPos, monsterPos) >= _viewLength)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false ;
    }

    protected  void SetAttackCoolTime(float attackCoolTime)
    {
        _attackCoolTime = attackCoolTime;
    }

    protected  void SetAttackDistance(float attackDistance)
    {
        _attackDistance = attackDistance;
    }

    protected  void SetViewAngle(float viewAngle)
    {
        _viewAngle = viewAngle;
    }

    protected  void SetAvoidRatio(float avoidRatio)
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
    public void Chase()
    {
        if (!_agent.enabled) return;
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

    //
    void TurnMonser()
    {
        this.transform.LookAt(GetMonster().transform.position);
    }

    // 罠情報の更新
    private void UpdateTrapInformation()
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
        status=eStatus.Rest;
        _agent.enabled = false;
        _animator.SetTrigger("FlatterStartTrigger");

    }

    // 拘束状態の終了　アニメーションの終了
    public void StopRestraining()
    {
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
        if(status == eStatus.Rest) return true;
        return false;
    }



}
