using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ōU������n���^�[�̍s���_�����܂Ƃ߂��N���X
/// </summary>
public class AI3Logic : Hunter_AI
{
    public float keepDistance = 1.5f;

    public float attackDistance = 0.30f;

    public float viewAngle = 180.0f;

    public float viewLength = 100;

    // ����s���p�x
    int avoidRatio = 7;

    // ����s���̃N�[���^�C��
    public float avoidCoolTime = 6.0f;

    public override void Start()
    {
        base.Start();
        SetAttackCoolTime(keepDistance);
        SetAttackDistance(attackDistance);
        SetViewAngle(viewAngle);
        SetViewLength(viewLength);
        SetAvoidRatio(avoidRatio);
    }

    public override void Chase()
    {
        SetDestination(GetMonsterLeftPosition());
    }
}
