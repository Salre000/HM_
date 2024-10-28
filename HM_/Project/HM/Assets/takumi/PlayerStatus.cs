using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーをステータスを管理するクラス
public class PlayerStatus : MonoBehaviour
{
    //現在のプレイヤーのHPの変数
    [SerializeField] float HP = 0.0f;

    //プレイヤーの最大HPの定数
   　const float MAXHP = 0.0f;

    //プレイヤーの速度
    [SerializeField] float Speed = 0.02f;

    //プレイヤーの回転の速度の変数
    [SerializeField] float RotateSpeed = 0.0f;

    //最大HPを返す関数
    public float GetMaxHP() { return MAXHP; }

    public float GetHp() { return HP; }

    public void Damage(float Damage) { HP -= Damage; }

    //プレイヤーのスピードを返す関数関数
    public float GetSpeed() { return Speed; }


}
