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

    private GameObject[] _monsters;

    // �g���b�v���X�g
    private List<GameObject> _trapList;

    // �g���b�v���m
    public SpiderTrapPool trap;

    // �i�r���b�V��
    protected NavMeshAgent _agent;

    // �A�j���[�V�����̏��
    AnimatorStateInfo animationState;

    // �A�j���[�V�����R���g���[���[
    private Animator _animator;

    // �_���[�W�N���X
    public Damage damage;

    // �����X�^�[�̈ʒu�𔭌��������ǂ����̃t���O
    public bool monsterDisplay = false;

    // �n���^�[�}�l�[�W���[
    public HunterManager manager;

    public HPManager hpManager;

    private GameObject terrian;

    public int HP = 100;

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
    private float _attackCoolTime = 2.0f;

    // �U������
    private float _attackDistance = 1.0f;

    // ����p�x
    private float _viewAngle;

    // ���싗��
    private float _viewLength;

    // ���p�x
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
    //           Unity�W���֐�
    //-------------------------------------------

    // Start is called before the first frame update
    public virtual void Start()
    {
        // �����X�^�[�̃^�O�擾
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

    //    // �S����ԂȂ�X�L�b�v
    //    if (CheckRest()) return;

    //    // �����X�^�[�������Ă���Ȃ�T�����ăX�L�b�v
    //    if (!monsterDisplay)
    //    {
    //        // �ω�
    //        Search();
    //        return;
    //    }

    //    // �����X�^�[�̍U�����Ƃ�ł��Ă��邩�ǂ���
    //    if (CheckMonsterAttack())
    //    {
    //        int randomNum = Random.Range(0, 10);
    //        if (randomNum > _AvoidRatio)
    //        {
    //            Avoid();
    //            return;
    //        }
    //    }


    //    // �����X�^�[�ւ̍U���͈͂ɂ���Ȃ��
    //    if (!CheckAttackDistance(this.gameObject))
    //    {
    //        TurnMonser();
    //        // �U���������ł��Ă���̂Ȃ��
    //        if (attackReady)
    //        {
    //            // �U��
    //            Attack();
    //        }
    //        else
    //        {
    //            // ���
    //            Back();
    //        }
    //    }
    //    else
    //    {

    //    }



    //}

    //------------------------------------------------
    //                    ����
    //------------------------------------------------
    /// <summary>
    /// �ړI�n�̐ݒ�
    /// </summary>
    /// <param name="pos"></param>
    public void SetDestination(Vector3 pos)
    {
        _agent.destination = pos;
    }

    /// <summary>
    /// �T���֐�
    /// </summary>
    public virtual void Search()
    {

    }

    // �����X�^�[�̔����������ɌĂԊ֐�
    public void DisappearMonster()
    {
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
    /// �����X�^�[�������邩�ǂ���
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

    // �����X�^�[�����E���ɂ��邩�ǂ����̊֐�
    public bool ObjectInsightPlayer()
    {
        Vector3 startPos = this.gameObject.transform.position;
        Vector3 monsterPos = _monster.transform.position;
        Vector3 playerToTarget = (_monster.transform.position - startPos).normalized;
        Vector3 lookDir = transform.TransformDirection(Vector3.forward).normalized;
        RaycastHit hit;

        if (Physics.Raycast(startPos, playerToTarget * _viewLength, out hit, _viewLength))
        {
            // ��������Ray�������X�^�[�łȂ��Ȃ��΂�
            PlayerStatus ste = hit.transform.gameObject.GetComponentInParent<PlayerStatus>();
            if (ste != null) return false;

            // ������p��
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
    /// �����X�^�[���U�����Ă��邩�ǂ���
    /// </summary>
    /// <returns></returns>
    bool CheckMonsterAttack()
    {
        return true;
    }

    // �����X�^�[�̐��ʂ̈ʒu���擾
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

    // �����X�^�[�̉E�̈ʒu���擾
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

    // �����X�^�[�̍��̈ʒu���擾
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

    // �����ɉ�����ʒu���擾
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

        // ����A�j���[�V�������ǂ���
        if(animationState.IsName("�A�[�}�`���A|Avoid") && animationState.normalizedTime >= 0.5f && animationState.normalizedTime < 0.8)
        {
            
        }
    }

    //-------------------------------------------------------------------------
    //                           �s���֌W�֐�
    //-------------------------------------------------------------------------

    /// <summary>
    /// 
    /// </summary>
    public void Attack()
    {
        attackReady = false;
        AttackAnimation();
        Debug.Log("�U��");
    }
    /// <summary>
    /// �ǐՊ֐�
    /// </summary>
    public virtual void Chase()
    {
        _agent.destination = _monster.transform.position;
    }

    public void Run()
    {
        // �A�j���[�V�����𗬂�
    }

    public void Avoid()
    {
        // �A�j���[�V�����𗬂�
    }

    /// <summary>
    /// ����������֐�
    /// </summary>
    public void Back()
    {

    }

    public void Death()
    {
        DeathAnimation();
        deathAnimNow = true;
        // �A�j���[�V�����C�x���g�ɂ��I���ナ�X�|�[��������
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
        return animationState;
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

    // ����A�j���[�V�����Đ��֐�
    public void RunAnimation()
    {

    }

    // ���S�A�j���[�V�����Đ��֐�
    public void DeathAnimation()
    {
        _animator.SetTrigger("Death");
        _agent.enabled = false;

    }

    // ���݃A�j���[�V�����Đ��֐�
    public void FlatterAnimation()
    {

    }

    public void AvoidAnimation()
    {

    }


    // �����X�^�[�I�u�W�F�N�g�̎擾
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

    // �S����Ԃł��邩�ǂ���
    public bool CheckRest()
    {
        if (status == eStatus.Rest) return true;
        return false;
    }





}
