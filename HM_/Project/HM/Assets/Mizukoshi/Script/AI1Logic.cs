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

    public float viewAngle = 125.0f;

    public float viewLength = 100;


    // 回避行動頻度
    int avoidRatio = 7;

    // 回避行動のクールタイム
    public float avoidCoolTime = 6.0f;

    private bool _avoidCoolFlag=false;  

    // 
    public float offsetX = 0;
    public float offsetY = 0;
    public float offsetZ = 2.0f;

    // すでにオンになっているかどうか
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
        // 拘束どうか確認
        if(CheckRest())return;

        // モンスターを見つかっているかどうか
        if (!monsterDisplay)
        {
            Search();
        }
        else
        {
            int randomNum=Random.Range(1, 10);
            if (randomNum <= 7)
            {
                // 回避行動
                Avoid();

                // 攻撃処理などを行わない。 
                return;
            }
            // モンスターの方向に向く
            TurnMonser();

            //距離が近かったら攻撃
            if (CheckAttackDistance(attackDistance, this.gameObject))
            {
                if (attackReady)
                {
                    Attack();
                }
                else
                {
                    // すこし下がる
                    Back();
                }
               
            }

            // ナビメッシュの目的地
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

        // 視界範囲関数
        if (ObjectInsightPlayer())
        {
            // 発見した
            DisappearMonster();
        }

    }

    public override void Chase()
    {
        if (!_agent.enabled) return;
        //_agent.destination =GetMonsterFrontPosition();
        _agent.destination=GetMonster().transform.position;
    }

    // モンスターの方向に向く
    void TurnMonser()
    {
        this.transform.LookAt(GetMonster().transform.position);
    }



}
