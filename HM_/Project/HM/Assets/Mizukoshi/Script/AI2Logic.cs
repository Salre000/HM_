using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �s���_��
/// 
/// �@�G��������܂ł͜p�j
/// 
/// �A�G����������2�Ԗڂɋ߂��Ƃ���Ɉړ�
/// 
/// �B�_��(cool time��5�b)
/// 
/// �C�G���߂Â��Ă����瓦���� �������͋ߋ����U��
/// 
/// �D�ȍ~�J��Ԃ�
/// 
/// 
/// 
/// </summary>

// �|��AI�̍s���_��
public class AI2Logic :Hunter_AI
{
    [SerializeField]
    // �U������
    private float attackDistance = 20.0f;

    [SerializeField]
    private float attackCoolTime = 5.0f;

    private void Start()
    {
        SetAttackDistance(attackDistance);
        SetAttackCoolTime(attackCoolTime);
    }

    private void Update()
    {
        // �S����ԂȂ�Γ����Ȃ�
        if (CheckRest())return;

        if (!monsterDisplay)
        {
            Search();
        }
        else
        {

        }
    }
}
