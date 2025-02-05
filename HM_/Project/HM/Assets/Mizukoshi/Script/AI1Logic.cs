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

    public float viewAngle = 125.0f;

    public float viewLength = 100;


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

   public override void Start()
    {
        base.Start();
        SetAttackCoolTime(keepDistance);
        SetViewAngle(viewAngle);
        SetViewLength(viewLength);
        
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
        }
    }

    public override void Search()
    {
        _agent.destination = searchPosition[searchPointIndex];
        if (CheckKeepDistance(searchPosition[searchPointIndex], this.gameObject, 10))
        {
            searchPointIndex++;
            if(searchPointIndex >= searchPosition.Length)
            {
                searchPointIndex = 0;
            }
            _agent.destination= searchPosition[searchPointIndex];   
        }

        // ���E�͈͊֐�
        if (ObjectInsightPlayer())
        {
            // ��������
            DisappearMonster();
        }

    }

    public override void Chase()
    {
        if (!_agent.enabled) return;
        //_agent.destination =GetMonsterFrontPosition();
        _agent.destination=GetMonster().transform.position;
    }

    // �����X�^�[�̕����Ɍ���
    void TurnMonser()
    {
        this.transform.LookAt(GetMonster().transform.position);
    }



}
