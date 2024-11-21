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
    public float speed = 5.0f;
    public LayerMask playerLayer; // �v���C���[�̃��C���[
    public float searchRadius = 10f; // ����낫���̎����͈̔�
    public float attackDistance = 2.0f;
    private Transform player; // �v���C���[��Transform
    private bool _enemyInsight=false;
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
            Search();
        }


        // �G�͍U����?
        if (EnemyAttackNow()&&!_enemyInsight)
        {
           // �G�̑O�ɂ��邩�ǂ����𔻒f
           
        }


        // ������d�ȓ�?
        if (PlayerToDistance() <= attackDistance)
        {
            // �U���A�j���[�V�����𗬂�
            Attack();
        }


        // 



        
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
    }

    void Search()
    {
        if (IsPlayerInSight())
        {
            _state= State.Chase;
            return;
        }
       
        if(Vector3.Distance(transform.position, agent.destination) < 1f)
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
         
    }

    public void AttackAnimationEnd()
    {
        _animator.SetBool("Attack", false);
        _animator.SetBool("AttackFinish", true);
    }

}
