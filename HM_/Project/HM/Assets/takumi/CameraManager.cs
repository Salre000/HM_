using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// カメラの管理をするプログラム
public class CameraManager : MonoBehaviour
{
    //　カメラの回転速度　（ラジアン）
    [SerializeField]private float _cameraSpeed;

    //プレイヤーのゲームオブジェクト
    [SerializeField] private GameObject _player;
    [SerializeField]private UIManager _manager;
    //プレイヤーとカメラの距離
   [SerializeField] private float _range=2.0f;

    [SerializeField] private float _horizontal;
    [SerializeField] private float _vertical;

    [SerializeField] private const float _minRange=2.0f;
    [SerializeField] private const float _maxRange=10.0f;

    [SerializeField]private float _cameraPositionAngle = 3.14f;

    public float Get_CameraPositionAngle() {  return _cameraPositionAngle+(180*3.14f/180); }

    public void Add_CameraPositionAngle(float angle) { _cameraPositionAngle += angle; }
    // Start is called before the first frame update
    void Start()
    {

        _player = GameObject.FindGameObjectWithTag("Player");
        Vector3 _position = this.transform.position;

        _cameraPositionAngle += (((_horizontal) * _manager.GetSensibility()) / 3.14f * 180) * 0.0001f;

        _range += 1 * 0.1f;

        //レンジの最小値を設定する
        if (_range < _minRange) { _range = _minRange; }

        //レンジの最大値を設定する
        if (_range > _maxRange) { _range = _maxRange; }

        _position.y = (_range / 3) * 2;

        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;

        this.transform.position = _position;



        transform.LookAt(_player.transform.position + Vector3.up);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // 移動量と回転量を求める
        _horizontal = Input.GetAxis("HorizontalR");
        _vertical = Input.GetAxis("VerticalR");

        if (_horizontal == 0 &&( _vertical <=0&&_vertical>=-0.5f)) return;

        Vector3 _position = this.transform.position;

        _cameraPositionAngle +=( ((_horizontal)* _manager.GetSensibility()) /3.14f*180)*0.0001f;

        _range+= _vertical*0.1f;

        //レンジの最小値を設定する
        if (_range < _minRange) { _range = _minRange; }

        //レンジの最大値を設定する
        if (_range > _maxRange) {  _range = _maxRange; }

        _position.y =(_range/3)*2 ;

        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;

        this.transform.position = _position;    



        transform.LookAt(_player.transform.position+Vector3.up);




    }

    public  void SetCameraPosition() 
    {

        Vector3 _position = this.transform.position;

        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;

        this.transform.position = _position;


    }
}
