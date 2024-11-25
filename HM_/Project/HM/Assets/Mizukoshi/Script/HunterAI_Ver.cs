using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterAI_Ver : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3[] distination=new Vector3[4];
    private int distinationNum = 0;
    public float sightRange = 10f; // ���F����
    public float fieldOfViewAngle = 110f; // ���E�p�x
    public float speed;
    public LayerMask playerLayer; // �v���C���[�̃��C���[
    public float searchRadius; // ����낫���̎����͈̔�
    public float attackDistance;
    public Transform player; // �v���C���[��Transform
    private bool _enemyInsight=false;
    private bool _attackNow=false;

    private Animator _animator;

    private enum State
    {
        Idle=0,
        Search,
        Chase,
        Fighting,
     }

    private State _state;

    private void Start()
    {

        _state = State.Search;
        _animator = GetComponent<Animator>();
        agent.destination=distination[0];
        agent.speed=speed;
    }


    private void Update()
    {
        AnimatorStateInfo animationState = _animator.GetCurrentAnimatorStateInfo(0);

        // �G�̍U����������?
        if (HitEnemyAttack())
        {
            agent.destination = player.position;
        }

        // �G�������Ă���?
        if (_enemyInsight)
        {
            agent.destination=player.position;
        }
        else
        {
            Run();
            Search();
        }

        //if (agent.isStopped)
        //{
            
        //}


        // �G�͍U����?
        //if (EnemyAttackNow()&&!_enemyInsight)
        //{
        //   // �G�̑O�ɂ��邩�ǂ����𔻒f
           
        //}


        // ������d�ȓ�?
        if (PlayerToDistance() <= attackDistance)
        {
            // �U���A�j���[�V�����𗬂�
            Attack();
        }

        Debug.Log(agent.destination);
        
    }

   

    bool HitEnemyAttack()
    {
        return false;
    }

    bool EnemyAttackNow()
    {
        return false ;
    }

    // �G�̍U���̑O�ɂ��邩
    bool In_Front_Of_EnemyAttack(Vector3 dir)
    {
        return true ;
    }

    // ���̂܂܂��ƓG�̍U�����������邩�ǂ���
    bool CheckHitIntheFuture()
    {
        return false;
    }

    void Rolling()
    {

    }

    void Walk()
    {

    }

    void Attack()
    {
        // �U���̃A�j���[�V�����𗬂��B
        _animator.SetBool("Attack", true);
        _animator.SetBool("AttackFinish", false);
        _attackNow = true;
    }

    void Search()
    {
        if (IsPlayerInSight())
        {
            _state= State.Chase;
            _enemyInsight = true;
        }
       
        if(Vector3.Distance(transform.position,agent.destination) < 1f)
        {
            distinationNum++;
            agent.destination = distination[distinationNum];
        }

    }

    bool IsPlayerInSight()
    {
        // �v���C���[�Ƃ̕����x�N�g�����v�Z
        Vector3 directionToPlayer = player.position - transform.position;

        // �v���C���[�����E�p�x���ɂ��邩���`�F�b�N
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
        if (angleToPlayer < fieldOfViewAngle / 2)
        {
            // �v���C���[�����E�p�x���ɂ���ꍇ�A���C���΂��ĎՕ������Ȃ����m�F
            float distanceToPlayer = directionToPlayer.magnitude;
            if (distanceToPlayer <= sightRange)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out hit, sightRange, playerLayer))
                {
                    // ���C���v���C���[�ɓ��������ꍇ�A���E���Ƀv���C���[������
                    return true;
                }
            }
        }
        return false;
    }

    bool IsPlayerInSight(Vector3 []targetPosition)
    {
        for (int i = 0; i < targetPosition.Length; i++)
        {
            // �v���C���[�Ƃ̕����x�N�g�����v�Z
            Vector3 directionToPlayer = targetPosition[i] - transform.position;

            // �v���C���[�����E�p�x���ɂ��邩���`�F�b�N
            float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
            if (angleToPlayer < fieldOfViewAngle / 2)
            {
                // �v���C���[�����E�p�x���ɂ���ꍇ�A���C���΂��ĎՕ������Ȃ����m�F
                float distanceToPlayer = directionToPlayer.magnitude;
                if (distanceToPlayer <= sightRange)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out hit, sightRange, playerLayer))
                    {
                        // ���C���v���C���[�ɓ��������ꍇ�A���E���Ƀv���C���[������
                        return true;
                    }
                }
            }
        }

        return false;
    }

    float PlayerToDistance()
    {
        float distance = 0;
        distance=Vector3.Distance(player.transform.position,this.transform.position);
        return distance;
    }

    void AnimationFinishInform(AnimatorStateInfo inform)
    {
        //animationState.normalizedTime >= 0.75f && animationState.IsName("ataka1")
        if (inform.normalizedTime >= 0.75f && inform.IsName("ataka1"))
        {
            AttackAnimationEnd();
        }

        if (agent.isStopped)
        {

        }
    }

    public void AttackAnimationEnd()
    {
        _animator.SetBool("Attack", false);
        _animator.SetBool("AttackFinish", true);
        _attackNow=false;
    }

    public void RunAnimationEnd()
    {
        _animator.SetBool("Walk", false);
        _animator.SetBool("WalkFinish", true);
    }

    public void Run()
    {
        _animator.SetBool("Walk", true);
        _animator.SetBool("WalkFinish", false);
    }

    public void Idle()
    {

    }

    public bool GetAttackState()
    {
        return true;
    }

}
