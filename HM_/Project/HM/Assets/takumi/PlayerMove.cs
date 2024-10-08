using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//プレイヤーを動かすクラス
public class PlayerMove : MonoBehaviour
{
    Vector3 PlayerPosition;

    private float _horizontal;
    private float _vertical;
    // Start is called before the first frame update
    void Start()
    {
        //座標を今の座標に更新するプログラム
        PlayerPosition=this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = _vertical = 0;

        // 移動量と回転量を求める
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");


        if (_horizontal == 0 && _vertical == 0) return;


        Vector3 position=this.transform.position;

        position.x += _horizontal;
        position.z += _vertical;





    }
}
