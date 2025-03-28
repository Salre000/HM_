using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アタックエリアのオブジェクトプール
public class AttackAreaPool : MonoBehaviour
{
    private const int MAX_NUMBER =  30;

    //攻撃判定のオブジェクトプールを生成
    List<GameObject> objectList = new List<GameObject>(MAX_NUMBER);
    List<AttackArea> attackAreaList = new List<AttackArea>(MAX_NUMBER);
    
    //攻撃判定を識別するためのタグを纏めたクラス
    [SerializeField] Tag tagObject;
    //攻撃判定を追従させるオブジェクト配列
    [Header("頭、左腕、左足、右腕、右足")]
    GameObject []GameObjects=new GameObject[5];

    //プレイヤーのオブジェクト
    [SerializeField]GameObject Player;

    private void Awake()
    {
        //プレイヤーのオブジェクトを取得
        Player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < MAX_NUMBER; i++) 
        {//オブジェクトを生成
            objectList.Add(new GameObject());
            objectList[i].transform.SetParent(this.transform);

            //分かりやすくするためにオブジェクトに名前をつける
            objectList[i].name = "attackArea";

            //識別するためにオブジェクトのタグを変更
            objectList[i].tag = tagObject.GetPlayerAttackTag();

            //ダメージクラスを追加
            objectList[i].AddComponent<Damage>().SetDamage(0);
            
            //判定の管理クラスを追加
            attackAreaList.Add(objectList[i].AddComponent<AttackArea>());

            objectList[i].transform.gameObject.SetActive(false);
        }

        int index = 0;

        //攻撃判定の追従先を取得
        GameObjects[index] = GameObject.Find("head"); index++;
        GameObjects[index] = GameObject.Find("LeftArm"); index++;
        GameObjects[index] = GameObject.Find("LeftFoot"); index++;
        GameObjects[index] = GameObject.Find("LightArm"); index++;
        GameObjects[index] = GameObject.Find("LightFoot"); index++;
    }


    public void SetAttack(AnimationEvent Event) 
    {

        //アニメーションイベントで受け取った値が無効かどうかを判定
        if (Event.intParameter < 0 || GameObjects.Length <= Event.intParameter) return;

        for(int i = 0; i < MAX_NUMBER; i++) 
        {

            if (objectList[i].activeSelf == false) 
            {
                //使用可能なオブジェクトを使用状態に変更
                attackAreaList[i].SetAttackArea(GameObjects[Event.intParameter], Event.floatParameter);

                return;
            }
        }
    }
    public void SetAttackBig(int Damage)
    {
        for (int i = 0; i < MAX_NUMBER; i++)
        {

            if (objectList[i].activeSelf == false)
            {

                attackAreaList[i].SetAttackArea(Player, Damage, 0.3f,-60);

                return;
            }
        }
    }
}
