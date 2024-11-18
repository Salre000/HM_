using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAI_Ver : MonoBehaviour
{

    public float sightRange = 10f; // ���F����
    public float fieldOfViewAngle = 110f; // ���E�p�x
    public LayerMask playerLayer; // �v���C���[�̃��C���[

    private Transform player; // �v���C���[��Transform


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
    }


    private void Update()
    {

        // �G�̍U����������?


        // �G�������Ă���?


        // �G�͍U����?

       
        // ������d�ȓ�?


        // 



        
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Search:

                break;
            case State.Chase:
                break;
            case State.Fighting:
                break;
        }



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

    }

    void Search()
    {

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




}
