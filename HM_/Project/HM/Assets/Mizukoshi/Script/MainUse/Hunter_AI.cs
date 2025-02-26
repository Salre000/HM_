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
/// �n���^�[�̊��N���X
/// </summary>
public abstract class Hunter_AI : MonoBehaviour
{
    // �����X�^�[�̃I�u�W�F�N�g
    private GameObject _monster;

    // 230Frame�̒x��
    private float waitTime = 3.0f;

    private bool startWait = true;

    private GameObject[] _monsters;

    // �g���b�v���X�g
    private List<GameObject> _trapList;

    // �g���b�v���m
    public SpiderTrapPool trap;

    // �i�r���b�V��
    protected NavMeshAgent _agent;

    protected Vector3 _spwnPosition;

    // �A�j���[�V�����̏��
    AnimatorStateInfo animationState;

    [SerializeField]
    // �A�j���[�V�����R���g���[���[
    private Animator _animator;

    // �_���[�W�N���X
    public Damage damage;

    // �����X�^�[�̈ʒu�𔭌��������ǂ����̃t���O
    private bool _monsterDisplay = false;

    // �n���^�[�}�l�[�W���[
    public HunterManager manager;

    // HP �}�l�[�W��-�̐ڑ�
    public HPManager hpManager;

    private GameObject effect;

    protected AudioSource p_audioSource;

    public int HP = 100;

    private float speed = 0.5f;

    // �ҋ@����
    private float waitSecond = 1.0f;

    // �ҋ@�o�ߎ���
    private float elapsedTime = 0;

    // �ҋ@�t���O
    private bool waitFlag = false;

    // �U���������ł��Ă��邩
    protected bool attackReady = true;

    // ���Ԍo�ߗp�ϐ�
    private float coolTime = 0.0f;

    // �U���̃N�[���^�C��
    private float _attackCoolTime = 10.0f;

    [SerializeField]
    // �U������
    private float _attackDistanceFF = 1.0f;

    // ����p�x
    private float _viewAngle;

    // ���싗��
    private float _viewLength;

    // ���p�x
    private float _AvoidRatio;

    private bool deathAnimNow = false;

    static PlayerAttack playerAttack;


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
    // �{���ʒu�̎w��
    protected int searchPointIndex = 0;

    // ���v���肩�ǂ���
    protected bool CloclWise = false;

    private Collider myCollider;
    //-------------------------------------------
    //           Unity�W���֐�
    //-------------------------------------------

    // Start is called before the first frame update
    public virtual void Start()
    {
        Initialize();
        _agent.destination = _monster.transform.position;
        SetOffNavmesh();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerAttack") return;

        if (other.GetComponent<Damage>() == null) return;

        HitEffectManager.instance.HitEffectShow(other.transform.position, HitEffectManager.CharacterType.Monster);
        damage = other.GetComponent<Damage>();
        p_audioSource.PlayOneShot(_MonsterHitSound());
        hpManager.HunterDamage(damage.GetDamage(), this.GetHunterID());
    }

    private void Update()
    {


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

        WaitAttackCoolTime();

        // �S����ԂȂ��~
        if (CheckRest()) return;

        // �n���^�[�̍U�����Ƃ�ł��Ă��邩�ǂ������m�F
        if (CheckMonsterAttack())
        {
            // ���E���̋����ɓ����Ă��Ȃ��Ȃ�X���[
            if (GetMonstersDistance() <= _viewLength)
            {
                // �s�����O�ɂ�����̊m����ϓ�
                int avoidNum = Random.Range(0, 10);
                if (avoidNum <= _AvoidRatio)
                {
                    Avoid();
                    return;
                }
            }
        }

        TurnMonser();

        // �U���ł��鋗���ɂ��Ȃ��Ȃ�
        if (!CheckAttackDistance(this.gameObject))
        {
            // �U�����Ȃ�X�L�b�v
            if (CheckAttack()) return;
            Chase();
            //if (manager.GetHunterDeathAmount() >= 3)
            //{
            //    int random = Random.Range(0, 5);
            //    switch (random)
            //    {
            //        case 0: SetDestination(_monster.transform.position); break;
            //        case 1: SetDestination(GetMonsterBackPosition()); break;
            //        case 2: SetDestination(GetMonsterFrontPosition()); break;
            //        case 3: SetDestination(GetMonsterLeftPosition()); break;
            //        case 4: SetDestination(GetMonsterRightPosition()); break;
            //        default:
            //            SetDestination(_monster.transform.position); break;
                       
            //    }
            //    return;
            //}
            //else
            //{
            //    SetNavmesh();
            //}

        }
        else
        {
            // �U���������ł��Ă���̂Ȃ��
            if (attackReady)
            {
                // �U��
                Attack();
            }
            else
            {
                if (CheckAttack()) return;

                // �����������Ƃ�B
                //Back();
            }
        }
        //------------------------------------------------
        //                    ����
        //------------------------------------------------

    }

        void Initialize()
        {
            // �����X�^�[�̃^�O�擾
            _monster = GameObject.FindGameObjectWithTag("Player");
            _monsters = GameObject.FindGameObjectsWithTag("Player");
            manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HunterManager>();
            _animator = GetComponent<Animator>();
            hpManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
            status = eStatus.None;
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = speed;
            if (CloclWise)
            {
                _agent.destination = searchPosition[searchPosition.Length - 1];
            }
            else { _agent.destination = searchPosition[0]; }
            _trapList = SpiderTrapPool.instance?.GetTraps();
            myCollider = GetComponent<Collider>();
            playerAttack = GameObject.FindAnyObjectByType<PlayerAttack>();
            p_audioSource = GetComponent<AudioSource>();
            SetDestination(_monster.transform.position);
        }
    /// <summary>
    /// �ړI�n�̐ݒ�
    /// </summary>
    /// <param name="pos"></param>
    public void SetDestination(Vector3 pos)
    {
        if (!CheckNavmeshEnable()) return;
        _agent.isStopped = false;
        _agent.destination = pos;
    }

    /// <summary>
    /// �T���֐�
    /// </summary>
    protected void Search()
    {

    }

    protected void SetClockwise(bool on)
    {
        CloclWise = on;
    }

    // �����X�^�[�̔����������ɌĂԊ֐�
    public void DisappearMonster()
    {
        if (manager == null) return;
        manager.SetDisapper();
    }

    /// <summary>
    /// �U���ł��鋗���ɂ��邩
    /// </summary>
    /// <param name="acceptDistance"></param>
    /// <returns>�U���ł��鋗���Ȃ�true,�ł��Ȃ��Ȃ��false</returns>
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

    // �����X�^�[�Ƃ̋������m�F
    private float GetMonstersDistance()
    {
        return Vector3.Distance(_monster.transform.position, this.transform.position);
    }
    /// <summary>
    /// �������m�F����֐�
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

    // �����X�^�[�����E���ɂ��邩�ǂ����̊֐�
    public bool ObjectInsightPlayer()
    {
        Vector3 startPos = this.gameObject.transform.position;
        Vector3 monsterPos = _monster.transform.position;

        // �v���C���[�ƃ����X�^�[�̕������擾
        Vector3 playerToTarget = (_monster.transform.position - startPos).normalized;

        // �v���C���[�����Ă���������擾
        Vector3 lookDir = transform.TransformDirection(Vector3.forward).normalized;
        RaycastHit hit;

        // �����X�^�[�Ɍ�����Ray�𔭎�
        if (Physics.Raycast(startPos, playerToTarget, out hit, _viewLength))
        {
            // ��������Ray�������X�^�[�łȂ��Ȃ��΂�
            PlayerStatus ste = hit.transform.gameObject.GetComponentInParent<PlayerStatus>();
            if (ste == null) return false;

            // ������p���͈͓��Ȃ�
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

        // �v���C���[�ƃ����X�^�[�̕������擾
        Vector3 playerToTarget = (_monster.transform.position - startPos).normalized;

        // �v���C���[�����Ă���������擾
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
    /// �����X�^�[���U�����Ă��邩�ǂ���
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
        if (animationState.IsName("�A�[�}�`���A|Attack1")) return true;
        return false;
    }

    // �����X�^�[�̐��ʂ̈ʒu���擾
    protected Vector3 GetMonsterFrontPosition()
    {
        float offsetX = 0;
        float offsetY = 0;
        float offsetZ = 0.0080f;
        Vector3 newPos = GetMonster().transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        offset = GetMonster().transform.rotation * offset;
        newPos = newPos + offset;
        return newPos;
    }

    // �����X�^�[�̉E�̈ʒu���擾
    protected Vector3 GetMonsterRightPosition()
    {
        float offsetX = 0.0080f;
        float offsetY = 0;
        float offsetZ = 0f;
        Vector3 newPos = GetMonster().transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        offset = GetMonster().transform.rotation * offset;
        newPos = newPos + offset;
        return newPos;
    }

    // �����X�^�[�̍��̈ʒu���擾
    protected Vector3 GetMonsterLeftPosition()
    {
        float offsetX = -0.008f;
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
        float offsetZ = -0.0080f;
        Vector3 newPos = GetMonster().transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        offset = GetMonster().transform.rotation * offset;
        newPos = newPos + offset;
        return newPos;
    }

    // �����ɉ�����ʒu���擾
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
    /// ����̃t���[�����ɂȂ��Ă��邩�B
    /// </summary>
    /// <returns></returns>
    bool CheckAvoid()
    {
        animationState = GetAnimState();

        // ����A�j���[�V�������ǂ���
        if (animationState.IsName("�A�[�}�`���A|Avoid") && animationState.normalizedTime >= 0.5f && animationState.normalizedTime < 0.8)
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
    //                           �s���֌W�֐�
    //-------------------------------------------------------------------------

    /// <summary>
    /// 
    /// </summary>
    public virtual void Attack()
    {
        // �i�r���[�V�����ɂ��ړ����Ȃ����B
        SetOffNavmesh();
        if (attackReady)
        {
            AttackAnimation();
            attackReady = false;
        }

    }

    public void AttackEnd()
    {
        SetNavmesh();
    }

    /// <summary>
    /// �ǐՊ֐�
    /// </summary>
    public virtual void Chase()
    {
       if(!_agent.enabled) _agent.enabled = true;
    }

    public void Run()
    {
        // �A�j���[�V�����𗬂�
    }

    public void Avoid()
    {
        // �A�j���[�V�����𗬂�
        _animator.SetTrigger("AvoidTrigger");
    }

    /// <summary>
    /// ����������֐�
    /// </summary>
    public void Back()
    {
        SetDestination(GetBackPosition());
    }

    public void Death()
    {
        DeathAnimation();
        deathAnimNow = true;
        // �A�j���[�V�����C�x���g�ɂ��I���ナ�X�|�[��������
    }

    public void DeathFinish()
    {
        int num = this.GetComponent<Hunter_ID>().GetHunterID();
        manager.Respawn(num);
    }

    //
    void TurnMonser()
    {
        if(GetMonster() == null) return;
        this.transform.LookAt(GetMonster().transform.position);
    }

    // 㩏��̍X�V
    private void UpdateTrapInformation()
    {

    }

    public void WaitForCount(float length = 1)
    {
        waitSecond = length;
        if (waitFlag) return;
        waitFlag = true;

    }

    // �����X�^�[�I�u�W�F�N�g�̎擾
    public GameObject GetMonster()
    {
        return _monster;
    }
    //-------------------------------------------------------------------------
    //                     �A�j���[�V�����֌W�֐�
    //-------------------------------------------------------------------------

    int restrainCount = 0;

    /// <summary>
    /// ���݂̃A�j���[�V�����̏�Ԃ��擾
    /// </summary>
    /// <returns></returns>
    public AnimatorStateInfo GetAnimState()
    {
        return _animator.GetCurrentAnimatorStateInfo(0);
    }

    // �S����Ԃ̊J�n �A�j���[�V�����̊J�n
    public void StartRestraining()
    {
        restrainCount++;
        status = eStatus.Rest;
        _agent.enabled = false;
        _animator.SetTrigger("FlatterStartTrigger");
    }

    // �S����Ԃ̏I���@�A�j���[�V�����̏I��
    public void StopRestraining()
    {
        restrainCount--;
        if (restrainCount < 0) return;
        status = eStatus.None;
        _agent.enabled = true;
        _animator.SetTrigger("FlatterFinishTrigger");
    }

    // �U���A�j���[�V�����Đ��֐�
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

    // ����A�j���[�V�����Đ��֐�
    public void RunAnimation()
    {

    }

    // ���S�A�j���[�V�����Đ��֐�
    public void DeathAnimation()
    {
        _animator.SetTrigger("DeathTrigger");
        _agent.enabled = false;

    }

    // ���݃A�j���[�V�����Đ��֐�
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

    // �S����Ԃł��邩�ǂ���
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

    // �i�r���b�V�����L�����ǂ������m�F
    protected bool CheckNavmeshEnable()
    {
        return _agent.enabled;
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
        this.speed = speed;
    }

    private System.Func<AudioClip> _MonsterHitSound;

    public void SetMonsterHitSound(System.Func<AudioClip> _monsterHitSound) 
    { _MonsterHitSound = _monsterHitSound; }


}
