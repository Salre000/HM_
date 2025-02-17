using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 剣の行動論理を表すクラス    行動の基本
/// </summary>
public class AI4Logic : Hunter_AI
{

    public float keepDistance = 1.5f;

    public float attackDistance = 2.0f;

    public float viewAngle = 180.0f;

    public float viewLength = 100;

    // 回避行動頻度
    int avoidRatio = 7;

    // 回避行動のクールタイム
    public float avoidCoolTime = 6.0f;

    private float attackCoolTime = 6.0f;

    public override void Start()
    {
        base.Start();
        SetAttackCoolTime(attackCoolTime);
        SetAttackDistance(attackDistance);
        SetAvoidRatio(avoidRatio);
        SetViewAngle(viewAngle);
        SetViewLength(viewLength);
    }

    public override void Chase()
    {
        SetDestination(GetMonster().transform.position);
    }
}
