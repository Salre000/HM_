using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAI_Ver : MonoBehaviour
{
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

   

}
