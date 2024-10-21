using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//プレイヤーを動かすクラス
public class PlayerMove : MonoBehaviour
{
    Vector3 PlayerPosition;

   [SerializeField]  private float _horizontal;
    [SerializeField] private float _vertical;


    //プレイヤーの角度
    [SerializeField] private float _angle;

    private PlayerStatus _status;


    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        //座標を今の座標に更新するプログラム
        PlayerPosition=this.transform.position;

        this.gameObject.AddComponent<PlayerStatus>();

        _status = this.GetComponent<PlayerStatus>();

    }


    // Update is called once per frame
    void Update()
    {

        pos = Vector3.zero;
        _horizontal = _vertical = 0;

        // 移動量と回転量を求める
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");


        if (_horizontal == 0 && _vertical == 0) return;


        _angle -= (_horizontal) * 0.1f;

        Vector3 Angles = this.transform.eulerAngles;

        Angles.y = _angle;
        this.transform.eulerAngles = Angles;


        pos=this.transform.position;


        //プレイヤーの移動
        //pos.x += ( _vertical * _status.GetSpeed());
        pos.z += (_vertical * _status.GetSpeed());



        this.transform.position=pos;



    }
}
