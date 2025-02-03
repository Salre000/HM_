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
    private NavMeshAgent _agent;

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

    public int HP = 100;

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

    protected enum eStatus
    {
        None,
        Rest,
        Max,
    };

    protected eStatus status;

    public Transform[] searchPosition=new Transform[4];

    

    //-------------------------------------------
    //           Unity�W���֐�
    //-------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        // �����X�^�[�̃^�O�擾
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
        // �g���b�v���̎擾
        

        // �S����ԂȂ�X�L�b�v
        if (CheckRest())return;

        // �����X�^�[�������Ă���Ȃ�T�����ăX�L�b�v
        if (!monsterDisplay)
        {
            Search();
            return;
        }

        // �����X�^�[�̍U�����Ƃ�ł��Ă��邩�ǂ���
        if (CheckMonsterAttack())
        {
            int randomNum=Random.Range(0, 10);
            if (randomNum > _AvoidRatio)
            {
                Avoid();
                return;
            }
        }


        // �����X�^�[�ւ̍U���͈͂ɂ���Ȃ��
        if (!CheckAttackDistance(this.gameObject))
        {
            TurnMonser();
            // �U���������ł��Ă���̂Ȃ��
            if (attackReady)
            {
                // �U��
                Attack();
            }
            else
            {
                // ���
                Back();
            }
        }
        else
        {

        }



    }

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
       if(ObjectInsightPlayer())DisappearMonster();
    }

    // �����X�^�[�̔����������ɌĂԊ֐�
    public void DisappearMonster()
    {
        //manager.SetDisapper();
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
    /// <summary>
    /// �����X�^�[�������邩�ǂ���
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

    // �����X�^�[�����E���ɂ��邩�ǂ����̊֐�
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
    /// �����X�^�[���U�����Ă��邩�ǂ���
    /// </summary>
    /// <returns></returns>
    bool CheckMonsterAttack()
    {
        return true;
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
    public void Chase()
    {
        if (!_agent.enabled) return;
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

    //
    void TurnMonser()
    {
        this.transform.LookAt(GetMonster().transform.position);
    }

    // 㩏��̍X�V
    private void UpdateTrapInformation()
    {
        
    }

    //-------------------------------------------------------------------------
    //                     �A�j���[�V�����֌W�֐�
    //-------------------------------------------------------------------------

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
        status=eStatus.Rest;
        _agent.enabled = false;
        _animator.SetTrigger("FlatterStartTrigger");

    }

    // �S����Ԃ̏I���@�A�j���[�V�����̏I��
    public void StopRestraining()
    {
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
        if(status == eStatus.Rest) return true;
        return false;
    }



}
