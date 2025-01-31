using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// �n���^�[�̊��N���X
/// </summary>
public abstract class Hunter_AI : MonoBehaviour
{
    private GameObject _monster;

    private GameObject[] _monsters;

    private NavMeshAgent _agent;

    AnimatorStateInfo animationState;

    private Animator _animator;

    public Damage damage;

    // �����X�^�[�̈ʒu�𔭌��������ǂ����̃t���O
    public bool monsterDisplay=false;

    public HunterManager manager;

    public HPManager hpManager;

    public int HP = 100;

    public bool attackReady = false;

    private 

    // Start is called before the first frame update
    void Start()
    {
        // �����X�^�[�̃^�O�擾
        _monster = GameObject.FindGameObjectWithTag("Player");
        _monsters=GameObject.FindGameObjectsWithTag("Player");
        _animator =GetComponent<Animator>();
       
        _agent= GetComponent<NavMeshAgent>();
    }

    //------------------------------------------------
    //                    ����
    //------------------------------------------------

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

    public bool CheckKeepDistance(float acceptDistance, GameObject AIType)
    {
       
        float calculate = Vector3.Distance(_monster.transform.position, AIType.transform.position);
        //Debug.Log(calculate);
        return calculate > acceptDistance;
    }
    /// <summary>
    /// �����X�^�[��������ʒu���ǂ���
    /// </summary>
    /// <returns></returns>
    public bool IsMonsterInSight()
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
        AttackAnimation();
        Debug.Log("�U��");
    }
    /// <summary>
    /// �ǐՊ֐�
    /// </summary>
    public void Chase()
    {
        if(!_agent.enabled) return;
       _agent.destination=_monster.transform.position;
    }

    public void Run()
    {

    }

    public void Avoid()
    {

    }

    public bool CheckAttackCoolTime()
    {
        return true;
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
        
        _agent.enabled = false;
        _animator.SetTrigger("FlatterStartTrigger");
       
    }

    // �S����Ԃ̏I���@�A�j���[�V�����̏I��
    public void StopRestraining()
    {
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
        _agent.isStopped = true;

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
        _agent.isStopped = true;
        _animator.SetTrigger("Reset");
    }

    private void OnTriggerEnter(Collider other)
    {
        hpManager.HunterDamage(damage.GetDamage(), this.GetHunterID());
    }
}
