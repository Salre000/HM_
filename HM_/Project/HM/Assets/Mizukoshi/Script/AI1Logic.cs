using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// ���C�X�̍s���_���̃N���X
/// </summary>
public class AI1Logic :Hunter_AI
{
    private Vector3 destination;

    public float keepDistance = 1.5f;

    public float attackDistance = 2.0f;


    // ����s���p�x
    int avoidRatio = 7;

    // ����s���̃N�[���^�C��
    public float avoidCoolTime = 6.0f;

    private bool _avoidCoolFlag=false;  

    // 
    public float offsetX = 0;
    public float offsetY = 0;
    public float offsetZ = 2.0f;

    // ���łɃI���ɂȂ��Ă��邩�ǂ���
    private bool alreadyFlag=false;

    private void Start()
    {
        SetAttackCoolTime(keepDistance);
    }

    public void Update()
    {
        // �S���ǂ����m�F
        if(CheckRest())return;

        // �����X�^�[���������Ă��邩�ǂ���
        if (!monsterDisplay)
        {
            Search();
        }
        else
        {
            int randomNum=Random.Range(1, 10);
            if (randomNum <= 7)
            {
                // ����s��
                Avoid();

                // �U�������Ȃǂ��s��Ȃ��B 
                return;
            }
            // �����X�^�[�̕����Ɍ���
            TurnMonser();

            //�������߂�������U��
            if (CheckAttackDistance(attackDistance, this.gameObject))
            {
                if (attackReady)
                {
                    Attack();
                }
                else
                {
                    // ������������
                    Back();
                }
               
            }

            // �i�r���b�V���̖ړI�n
            Chase();
            SetDestination(GetDestinationPosition());
        }
    }

    //public override void Search()
    //{
    //    // �T���֐�


    //    // ���E�͈͊֐�
    //    if (IsMonsterInSight())
    //    {
    //        // ��������
    //        DisappearMonster();
    //    }
       
    //}

    // �ړI�n�̎擾
    Vector3 GetDestinationPosition()
    {
        Vector3 newPos=GetMonster().transform.position;
        Vector3 offset=new Vector3(offsetX, offsetY, offsetZ);
        offset=GetMonster().transform.rotation*offset;
        newPos=newPos+offset;
        return newPos;
    }

    // �����X�^�[�̕����Ɍ���
    void TurnMonser()
    {
        this.transform.LookAt(GetMonster().transform.position);
    }

    

}
