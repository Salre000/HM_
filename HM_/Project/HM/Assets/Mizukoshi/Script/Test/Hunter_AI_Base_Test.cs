using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunter_AI_Base_Test : MonoBehaviour
{
    // �@ ������30�ȏ゠��Ȃ�΃i�r���b�V���ɂ��ړ�

    // �A ������30�ȉ��Ȃ�΂������ړ�

    // �B �G���U����5�񂵂Ă�����܂��̗͑͂�20�ȉ��Ȃ��x�����

    // �C ������10�ȉ��Ȃ�΍U������B

    int attackDistance = 2;

    public float speed = 3.0f;

    public float AttackCoolTime;

    // �����X�^�[�Ƃ̋���
    float distance = 0;

    // �U�����Ă�����
    int attackNum = 0;

    // �����X�^�[�̃I�u�W�F�N�g 
    private GameObject _monster;

    //�G�[�W�F���g�ƂȂ�I�u�W�F�N�g��NavMeshAgent�i�[�p 
    private NavMeshAgent agent;

    // �҂���
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
        // �����X�^�[�̃^�O�擾
        _monster = GameObject.FindGameObjectWithTag("Player");

        // �i�r�̎擾
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animationState = _animator.GetCurrentAnimatorStateInfo(0);
        // �����X�^�[�Ǝ����̋����𑪂�
        distance = Vector3.Distance(this.transform.position, _monster.transform.position);

        if (CheckDie())
        {
            _deathAnimationNow = true;
            AttackAnimationEnd();
            // ����A�j���[�V�������~�߂�
            _animator.SetBool("Walk", false);
            _animator.SetBool("WalkFinish", true);
            _animator.SetBool("isDeadFinish", false);
            _animator.SetBool("isDead", true);
        }

        if (_deathAnimationNow)
        {
            if (animationState.normalizedTime >= 0.75f && animationState.IsName("death2"))
            {
                // �I�����m
                deathAnimationFinish = true;
                _deathAnimationNow = false;
                _animator.SetBool("isDead", false);
                _animator.SetBool("isDeadFinish", true);
            }
            return;
        }

        // �����X�^�[�Ǝ����̋�����20�ȏ�ł���΃i�r���b�V���ɂ��ړ����s��
        if (distance > attackDistance)
        {
            // ����A�j���[�V�������Đ�����
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
                    // �U���̃A�j���[�V�����𗬂��B
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
            // ����A�j���[�V�������Đ�����
            _animator.SetBool("Walk", true);
            _animator.SetBool("WalkFinish", false);
        }
        else
        {
            // ����A�j���[�V�������~�߂�
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
    /// �T���֐�
    /// </summary>
    /// <param name="list"></param> ���񂷂�ʒu�̔z��
    public void Search(Vector3[] list)
    {

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

    //-------------------------------------------------------------------------
    //                           �s���֌W�֐�
    //-------------------------------------------------------------------------

    /// <summary>
    /// 
    /// </summary>
    public void Attack()
    {

    }
    /// <summary>
    /// �ǐՊ֐�
    /// </summary>
    public void Chase()
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
        //_animator.SetBool("RestrainFlag", true);
    }

    // �S����Ԃ̏I���@�A�j���[�V�����̏I��
    public void StopRestraining()
    {
        //_animator.SetBool("RestraingFlag", false);
    }

    // �U���A�j���[�V�����Đ��֐�
    public void AttackAnimation()
    {

    }

    // ����A�j���[�V�����Đ��֐�
    public void RunAnimation()
    {

    }

    // ���S�A�j���[�V�����Đ��֐�
    public void DeathAnimation()
    {

    }

    // ���݃A�j���[�V�����Đ��֐�
    public void FlatterAnimation()
    {

    }
}
