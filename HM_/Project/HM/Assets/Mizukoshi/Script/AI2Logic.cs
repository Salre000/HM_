using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;


// �|��AI�̍s���_��
public class AI2Logic :Hunter_AI
{
    [SerializeField]
    // �U������
    private float attackDistance = 20.0f;

    [SerializeField]
    private float attackCoolTime = 5.0f;

    [SerializeField]
    private float viewAngle = 90.0f;

    [SerializeField]
    private float viewLength = 100;

    public override void Start()
    {
        SetAttackDistance(attackDistance);
        SetAttackCoolTime(attackCoolTime);
        SetViewAngle(viewAngle);
        SetViewLength(viewLength);
        SetClockwise(true);
        base.Start();
    }
}
