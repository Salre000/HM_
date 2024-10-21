using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーをステータスを管理するクラス
public class PlayerStatus : MonoBehaviour
{
    //現在のプレイヤーのHPの変数
    [SerializeField] float HP = 0.0f;

    //プレイヤーの最大HPの変数
   　float MAXHP = 0.0f;

    //プレイヤーの速度
    [SerializeField] float Speed = 0.1f;

    //プレイヤーの回転の速度の変数
    [SerializeField] float RotateSpeed = 0.0f;

    //最大HPを返す関数
    public float GetMaxHP() { return MAXHP; }

    //プレイヤーのスピードを返す関数関数
    public float GetSpeed() { return Speed; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
