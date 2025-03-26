using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// ハンターの基底クラス
/// </summary>
public abstract class Hunter_AI : MonoBehaviour
{
    // モンスターのオブジェクト
    private GameObject _monster;

    // 230Frameの遅延
    private float waitTime = 3.0f;

    private bool startWait = true;

    // トラップ感知
    public SpiderTrapPool trap;

    // ナビメッシュ
    protected NavMeshAgent _agent;

    protected Vector3 _spwnPosition;

    // アニメーションの状態
    AnimatorStateInfo animationState;

    [SerializeField]
    // アニメーションコントローラー
    protected Animator _animator;

    // ダメージクラス
    public Damage damage;

    // モンスターの位置を発見したかどうかのフラグ
    private bool _monsterDisplay = false;

    // ハンターマネージャー
    public HunterManager manager;

    // HP マネージャ-の接続
    public HPManager hpManager;

    private GameObject effect;

    protected AudioSource p_audioSource;

    public int HP = 100;

    private float _speed = 0.5f;

    private bool alreadyNear = false;

    private bool avoiding = false;

    // 待機時間
    private float waitSecond = 1.0f;

    // 待機経過時間
    private float elapsedTime = 0;

    // 待機フラグ
    [SerializeField] private bool waitFlag = false;

    // 攻撃準備ができているか
    [SerializeField] protected bool attackReady = true;

    // 時間経過用変数
    private float coolTime = 0.0f;

    public float detectionRadius = 0.0f;  // 相手が近づいた時に反応する距離
    public float fleeDistance = 0.0f;     // 離れる距離

    // 攻撃のクールタイム
    private float _attackCoolTime = 10.0f;

    [SerializeField]
    // 攻撃距離
    private float _attackDistanceFF = 1.0f;

    // 視野角度
    private float _viewAngle;

    // 視野距離
    private float _viewLength;

    // 回避頻度
    private float _AvoidRatio;

    // 死亡状態に
    private bool deathAnimNow = false;

    private bool runAway = false;

    static PlayerAttack playerAttack;

    private float deathWaitTime = 1.0f;

    private bool deathWaitNow = false;

    protected virtual void DebugDistance()
    {
        Debug.Log(restrainCount + "DDD");
        if (!_agent.isOnNavMesh)
        {
            Debug.Log("計算未完了QQQ");
        }
        else
        {
            Debug.Log("計算完了!QQQ");
        }

        if (!_agent.hasPath)
        {
            Debug.Log("検索中QQQ");
            if (_agent.enabled == false) return;
            if (float.IsInfinity(_agent.destination.magnitude)) return;

            _agent.destination = playerAttack.transform.position;

        }
        else
        {
            Debug.Log("検索完了!QQQ");
        }

    }
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
    // 捜索位置の指定
    protected int searchPointIndex = 0;

    // 時計周りかどうか
    protected bool CloclWise = false;

    private Collider myCollider;
    //-------------------------------------------
    //           Unity標準関数
    //-------------------------------------------

    // Start is called before the first frame update
    public virtual void Start()
    {
        Initialize();
        SetOffNavmesh();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerAttack") return;

        if (other.GetComponent<Damage>() == null) return;

        HitEffectManager.instance.HitEffectShow(this.transform.position, HitEffectManager.CharacterType.Monster);

        damage = other.GetComponent<Damage>();
        if (p_audioSource != null)
        {
            p_audioSource.PlayOneShot(_MonsterHitSound(), SoundListManager.instance.GetSoundVolume());
        }

        hpManager.HunterDamage(damage.GetDamage(), this.GetHunterID());
    }

    private void Update()
    {

        DebugDistance();

        if (startWait)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > waitTime)
            {
                startWait = false;
                SetNavmesh();
                elapsedTime = 0;
            }
            return;
        }

        if (deathWaitNow)
        {
            _agent.enabled = false;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > deathWaitTime)
            {
                deathWaitNow = false;
                SetNavmesh();
                elapsedTime = 0;
                int num = this.GetComponent<Hunter_ID>().GetHunterID();
                //manager.Respawn(num);
            }
            return;
        }

        WaitAttackCoolTime();


        if (avoiding) return;

        // 拘束状態なら停止
        if (CheckRest()) return;

        // 近づきすぎなら逃げる
        if (CheckKeepDistance(detectionRadius, this.gameObject))
        {
            //if (!_agent.enabled) _agent.enabled = true;
            if(!_agent.enabled)return;
            FleeFromPlayer();
            
        }

        if (CheckKeepDistance(4.0f, this.gameObject))
        {
            if (alreadyNear)
            {
                if (!_agent.enabled) _agent.enabled = true;
            }
        }

        // ハンターの攻撃がとんできているかどうかを確認
        if (CheckMonsterAttack())
        {
            // 視界内の距離に入っていないならスルー
            if (GetMonstersDistance() <= _viewLength)
            {
                // 行動理念により回避の確率を変動
                int avoidNum = Random.Range(0, 10);
                if (avoidNum <= _AvoidRatio)
                {
                    Avoid();
                    return;
                }
            }
        }

        TurnMonser();

        // 攻撃できる距離にいないなら
        if (!CheckAttackDistance(this.gameObject))
        {
            // 攻撃中ならスキップ
            if (CheckAttack()) return;
            Chase();
        }
        else
        {
            if (!alreadyNear) alreadyNear = true;
            // 攻撃準備ができているのならば
            if (attackReady)
            {

                AnimatorStateInfo s = GetAnimState();
                if (GetAnimState().IsName("アーマチュア|Attack1")) return;

                // 攻撃
                Attack();
            }
            else
            {
                if (CheckAttack()) return;
            }
        }
    }
    //------------------------------------------------
    //                    処理
    //------------------------------------------------
    void Initialize()
    {
        // モンスターのタグ取得
        _monster = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HunterManager>();
        _animator = GetComponent<Animator>();
        hpManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
        status = eStatus.None;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        myCollider = GetComponent<Collider>();
        playerAttack = GameObject.FindAnyObjectByType<PlayerAttack>();
        this.gameObject.AddComponent<AudioSource>();
        p_audioSource = GetComponent<AudioSource>();

    }
    /// <summary>
    /// 目的地の設定
    /// </summary>
    /// <param name="pos"></param>
    public void SetDestination(Vector3 pos)
    {
        if (!CheckNavmeshEnable()) return;
        _agent.enabled = true;
        _agent.isStopped = false;
       if (float.IsInfinity(_agent.destination.magnitude)) return;

        _agent.destination = pos;
    }

    /// <summary>
    /// 探索関数
    /// </summary>
    protected void Search()
    {

    }

    protected void SetClockwise(bool on)
    {
        CloclWise = on;
    }

    // モンスターの発見した時に呼ぶ関数
    public void DisappearMonster()
    {
        if (manager == null) return;
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
        return calculate < _attackDistanceFF;
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

    public void SetKeepDistance(float activeDistance, float keep)
    {
        detectionRadius = activeDistance;
        fleeDistance = keep;
    }

    // モンスターとの距離を確認
    private float GetMonstersDistance()
    {
        return Vector3.Distance(_monster.transform.position, this.transform.position);
    }
    /// <summary>
    /// 距離を確認する関数
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    private bool CheckNearDestination(Vector3 targetPos)
    {
        if (Vector3.Distance(this.transform.position, targetPos) <= 5.0f)
        {
            return true;
        }
        return false;
    }

    // モンスターが視界内にいるかどうかの関数
    public bool ObjectInsightPlayer()
    {
        Vector3 startPos = this.gameObject.transform.position;
        Vector3 monsterPos = _monster.transform.position;

        // プレイヤーとモンスターの方向を取得
        Vector3 playerToTarget = (_monster.transform.position - startPos).normalized;

        // プレイヤーが見ている方向を取得
        Vector3 lookDir = transform.TransformDirection(Vector3.forward).normalized;
        RaycastHit hit;

        // モンスターに向けてRayを発射
        if (Physics.Raycast(startPos, playerToTarget, out hit, _viewLength))
        {
            // 当たったRayがモンスターでないなら飛ばす
            PlayerStatus ste = hit.transform.gameObject.GetComponentInParent<PlayerStatus>();
            if (ste == null) return false;

            // かつ視野角が範囲内なら
            float angle = Vector3.Angle(playerToTarget, lookDir);
            if (angle <= _viewAngle / 2) return true;
        }
        return false;
    }

    public void DrawRay()
    {
        Vector3 startPos = this.gameObject.transform.position;
        startPos.y += 0.75f;
        Vector3 monsterPos = _monster.transform.position;

        // プレイヤーとモンスターの方向を取得
        Vector3 playerToTarget = (_monster.transform.position - startPos).normalized;

        // プレイヤーが見ている方向を取得
        Vector3 lookDir = transform.TransformDirection(Vector3.forward).normalized;

        Debug.DrawRay(startPos, playerToTarget, Color.red, _viewLength);
        Debug.DrawRay(startPos, lookDir, Color.blue, _viewLength);
    }


    // 
    protected void SetAttackCoolTime(float attackCoolTime)
    {
        _attackCoolTime = attackCoolTime;
    }

    protected void SetAttackDistance(float attackDistance)
    {
        _attackDistanceFF = attackDistance;
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
    // 攻撃のクールタイムを待つ
    protected void WaitAttackCoolTime()
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
    /// 
    protected bool CheckMonsterAttack()
    {
        return playerAttack.GetPredictionAttackFlag();
    }

    protected void WaitAvoidCoolTime()
    {
        if (attackReady) return;
        coolTime += Time.deltaTime;
        if (coolTime > _attackCoolTime)
        {
            coolTime = 0;
            attackReady = true;
        }
    }

    private bool CheckAttack()
    {
        animationState = GetAnimState();
        if (animationState.IsName("アーマチュア|Attack1")) return true;
        return false;
    }

    // モンスターの正面の位置を取得
    protected Vector3 GetMonsterFrontPosition()
    {
        float offsetX = 0;
        float offsetY = 0;
        float offsetZ = 0.180f;
        Vector3 newPos = GetMonster().transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        offset = GetMonster().transform.rotation * offset;
        newPos = newPos + offset;
        return newPos;
    }

    // モンスターの右の位置を取得
    protected Vector3 GetMonsterRightPosition()
    {
        float offsetX = 0.120f;
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
        float offsetX = -0.12f;
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
        float offsetZ = -0.180f;
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
        dir.Normalize();
        dir *= 5;
        float offsetX = dir.x;
        float offsetY = dir.y;
        float offsetZ = dir.z;
        Vector3 newPos = this.transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        offset = this.transform.rotation * offset;
        newPos = newPos + offset;
        return newPos;
    }
    /// <summary>
    /// 回避のフレーム数になっているか。
    /// </summary>
    /// <returns></returns>
    bool CheckAvoid()
    {
        animationState = GetAnimState();

        // 回避アニメーションかどうか
        if (animationState.IsName("アーマチュア|Avoid") && animationState.normalizedTime >= 0.5f && animationState.normalizedTime < 0.8)
        {
            return true;
        }

        return false;
    }

    protected bool CheckAudioSourceNull()
    {
        return p_audioSource == null;
    }

    //-------------------------------------------------------------------------
    //                           行動関係関数
    //-------------------------------------------------------------------------

    /// <summary>
    /// 
    /// </summary>
    public virtual void Attack()
    {
        // ナビメーションによる移動をなくす。
        SetOffNavmesh();

        if (attackReady)
        {
            AttackAnimation();
            attackReady = false;
            //Debug.Log(this.gameObject.name + "SSS" + attackReady);

        }

    }

    public void AttackEnd()
    {
        SetNavmesh();
    }

    /// <summary>
    /// 追跡関数
    /// </summary>
    public virtual void Chase()
    {
        if (!_agent.enabled) _agent.enabled = true;
    }

    public void Run()
    {
        // アニメーションを流す
    }

    public void Avoid()
    {
        // アニメーションを流す
        _animator.SetTrigger("AvoidTrigger");

        avoiding = true;
        _agent.enabled = false;
    }

    /// <summary>
    /// 少し下がる関数
    /// </summary>
    public void Back()
    {
        SetDestination(GetBackPosition());
    }

    public void Death()
    {
        if(GetAnimState().IsName("アーマチュア|Die(仮)"))return;
        DeathAnimation();
        deathAnimNow = true;
        // アニメーションイベントにより終了後リスポーンさせる
    }

    public void DeathFinish()
    {
        //if(restrainCount!=0)restrainCount--;
        deathAnimNow = false;
        alreadyNear = false;
        avoiding = false;
        hpManager.SetHunterLostNumber(-1);
        Respown();
    }

    public void AvoidFinish()
    {
        _agent.enabled = true;
        avoiding = false;
    }

    //
    void TurnMonser()
    {
        if (GetMonster() == null) return;
        this.transform.LookAt(new Vector3(_monster.transform.position.x, this.transform.position.y, _monster.transform.position.z));
    }

    void FleeFromPlayer()
    {
        // プレイヤーから逆方向に一定距離離れる位置を計算
        Vector3 directionAwayFromPlayer = transform.position - _monster.transform.position;
        Vector3 fleeTarget = transform.position + directionAwayFromPlayer.normalized * fleeDistance;

        if (!_agent.enabled) return;
        if(float.IsInfinity(_agent.destination.magnitude))return;
        // ナビメッシュエージェントを使って、逃げる場所に移動
        _agent.SetDestination(fleeTarget);
    }

    // 罠情報の更新
    private void UpdateTrapInformation()
    {

    }

    

    public void Respown()
    {
        deathWaitNow = true;
        SetOffNavmesh();
    }

    // モンスターオブジェクトの取得
    public GameObject GetMonster()
    {
        return _monster;
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
        return _animator.GetCurrentAnimatorStateInfo(0);
    }

    // 拘束状態の開始 アニメーションの開始
    public void StartRestraining()
    {
        restrainCount++;
        status = eStatus.Rest;
        _agent.enabled = false;
        _animator.SetTrigger("FlatterStartTrigger");
        attackReady = true;
    }

    // 拘束状態の終了　アニメーションの終了
    public void StopRestraining()
    {
        restrainCount--;
        if (restrainCount != 0) return;
        status = eStatus.None;
        _agent.enabled = true;
        _animator.SetTrigger("FlatterFinishTrigger");
        
    }

    // 攻撃アニメーション再生関数
    public void AttackAnimation()
    {
        _animator.SetTrigger("AttackTrigger");
    }

    private void StartAttack()
    {
        _animator.SetBool("Attack", true);
    }

    public void FinishAttack()
    {
        //_animator.SetBool("Attack", false);
    }

    // 走るアニメーション再生関数
    public void RunAnimation()
    {

    }

    // 死亡アニメーション再生関数
    public void DeathAnimation()
    {
        _animator.SetTrigger("DeathTrigger");
        attackReady = true;
        _agent.enabled = false;

    }

    // 怯みアニメーション再生関数
    public void FlatterAnimation()
    {

    }

    public void AvoidAnimation()
    {

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

    public void SetNavmesh()
    {
        if (!_agent.enabled) _agent.enabled = true;
    }

    public void SetOffNavmesh()
    {
        if (_agent.enabled) _agent.enabled = false;
    }

    // ナビメッシュが有効かどうかを確認
    protected bool CheckNavmeshEnable()
    {
        if(!_agent.enabled)return false;
        if(_agent==null) return false;
        return true;
    }

    public void ResetPosition()
    {
        if (_spwnPosition == null)
        {
            Vector3 pos = new Vector3(2.80463648f, 0.207313895f, -1.71056747f);
            this.transform.position = pos;
        }
        this.transform.position = _spwnPosition;
    }

    protected void SetSpeed(float speed)
    {
        this._speed = speed;
    }

    private System.Func<AudioClip> _MonsterHitSound;

    public void SetMonsterHitSound(System.Func<AudioClip> _monsterHitSound)
    { _MonsterHitSound = _monsterHitSound; }

    public async UniTask AnimeResetFlag() 
    {
        while (restrainCount != 0) 
        {
            await UniTask.DelayFrame(1);
        }

        _animator.SetTrigger("FlatterStartTrigger");
        _animator.SetTrigger("FlatterFinishTrigger");


        await UniTask.CompletedTask;
    }

}
