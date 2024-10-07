using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//プレイヤーを動かすクラス
public class PlayerMove : MonoBehaviour
{
    Vector3 PlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        //座標を今の座標に更新するプログラム
        PlayerPosition=this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {


        
    }
}
