using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// メイスの行動論理のクラス
/// </summary>
public class AI1Logic :Hunter_AI
{
    private Vector3 destination;

    public float keepDistance = 1.5f;

    public float attackDistance = 2.0f;

    public float viewAngle = 180.0f;

    public float viewLength = 100;

    // 回避行動頻度
    int avoidRatio = 7;

    // 回避行動のクールタイム
    public float avoidCoolTime = 6.0f;

    private bool _avoidCoolFlag=false;  

    // すでにオンになっているかどうか
    private bool alreadyFlag=false;

   public override void Start()
   {
        base.Start();
        SetAttackCoolTime(keepDistance);
        SetViewAngle(viewAngle);
        SetViewLength(viewLength);    
   }
}
