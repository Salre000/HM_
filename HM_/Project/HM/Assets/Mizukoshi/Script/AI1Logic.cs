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


    // 回避行動頻度
    int avoidRatio = 7;

    // 
    public float offsetX = 0;
    public float offsetY = 0;
    public float offsetZ = 2.0f;

    // すでにオンになっているかどうか
    private bool alreadyFlag=false;

    private void Start()
    {
        
    }

    public void Update()
    {
        // モンスターを見つかっているかどうか
        if (!monsterDisplay)
        {
            Search();
        }
        else
        {
            // モンスターの方向に向く
            TurnMonser();

            //距離が近かったら攻撃
            if (CheckAttackDistance(attackDistance, this.gameObject))
            {
                Attack();
            }

            // ナビメッシュの目的地
            Chase();
            SetDestination(GetDestinationPosition());
        }
    }

    public override void Search()
    {
        // 探索関数


        // 視界範囲関数
        if (IsMonsterInSight())
        {
            // 発見した
            DisappearMonster();
        }
       
    }

    // 目的地の取得
    Vector3 GetDestinationPosition()
    {
        Vector3 newPos=GetMonster().transform.position;
        Vector3 offset=new Vector3(offsetX, offsetY, offsetZ);
        offset=GetMonster().transform.rotation*offset;
        newPos=newPos+offset;
        return newPos;
    }

    // モンスターの方向に向く
    void TurnMonser()
    {
        this.transform.LookAt(GetMonster().transform.position);
    }


}
