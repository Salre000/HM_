using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// カメラの管理をするプログラム
public class CameraManager : MonoBehaviour
{
    //　カメラの回転速度　（ラジアン）
    [SerializeField] private float _cameraSpeed;

    //プレイヤーのゲームオブジェクト
    [SerializeField] private GameObject _player;

    //プレイヤーとカメラの距離
    [SerializeField] private float _range = 0.2f;

    //ステックの上下左右の値
    [SerializeField] private float _vertical;
    [SerializeField] private float _horizontal;

    //  プレイヤーとカメラの最小距離と最大距離
    [SerializeField] private const float _minRange = 0.2f;
    [SerializeField] private const float _maxRange = 1.0f;

    //現在カメラが向いている方向（ラジアン）
    [SerializeField] private float _cameraPositionAngle = 3.14f;

    //ハンターのリスポーン地点
    [SerializeField] GameObject resurrectionPoint;

    public float Get_CameraPositionAngle() { return _cameraPositionAngle + (180 * 3.14f / 180); }

    public void Add_CameraPositionAngle(float angle) { _cameraPositionAngle += angle; }

    //ゲーム開始時にハンターを映し終わるまでの変数
    public static bool setupFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        Setup().Forget();
    }

    //ゲーム開始時の準備の関数
    async UniTask Setup()
    {
        setupFlag = false;

        //プレイヤーを取得
        _player = GameObject.FindGameObjectWithTag("Player");

        //ゲーム開始時にハンターを映す関数
        await StartShowHunter();


        //ゲームが開始時のカメラの初期位置を設定
        Vector3 _position = this.transform.position;
        Vector3 vec = resurrectionPoint.transform.position - _player.transform.position;
        _cameraPositionAngle = Mathf.Atan2(vec.z, vec.x);
        _range = Vector3.Distance(this.transform.position, _player.transform.position);
        //レンジの最小値を設定する
        if (_range < _minRange) { _range = _minRange; }
        //レンジの最大値を設定する
        if (_range > _maxRange) { _range = _maxRange; }
        _position.y = ((_range / 3) * 2) ;
        _position.y += _player.transform.position.y;
        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;
        this.transform.position = _position;

        //プレイヤーを基準にプレイヤーの全体が見えるいちに向けてカメラの角度の変更
        transform.LookAt(_player.transform.position + Vector3.up/10);

        //セットアップを終了
        setupFlag = true;
    }

    //ハンターのリスポーン地点を映す時の距離
    private readonly float RESURRECTION＿RANGE =0.5f;

    //ハンターリスポーン地点を映している時の角度
    float resurrectionAngle = 0;
    //ゲーム開始時にハンターを移す関数
    async UniTask StartShowHunter()
    {
        //カメラの座標と角度をハンターのリスポーン地点に合わせる
        Vector3 vec =resurrectionPoint.transform.position- _player.transform.position;
        resurrectionAngle =Mathf.Atan2(vec.z, vec.x);
        this.transform.position = resurrectionPoint.transform.position + new Vector3(Mathf.Sin(resurrectionAngle + 1* Mathf.Deg2Rad) * RESURRECTION＿RANGE, 0.3f, Mathf.Cos(resurrectionAngle + 1 * Mathf.Deg2Rad) * RESURRECTION＿RANGE);
        this.transform.LookAt(resurrectionPoint.transform);

        //ゲームが始まった直後のフェードが終わるのを待つ
        await UniTask.DelayFrame(50);

        //ハンターのリスポーン地点の周りをカメラがグルグル回る
        for (int i = 0; i < 180; i++)
        {
            this.transform.position = resurrectionPoint.transform.position + new Vector3( Mathf.Sin(resurrectionAngle + ((i * 2)) * Mathf.Deg2Rad) * RESURRECTION＿RANGE, 0.3f, Mathf.Cos(resurrectionAngle + ((i * 2)) * Mathf.Deg2Rad) * RESURRECTION＿RANGE);
            this.transform.LookAt(resurrectionPoint.transform);
            await UniTask.DelayFrame(1);
        }

        vec /= 100;

        //ハンターのリスポーン地点からモンスターの地点までを移動させる
        for(int i = 0; i < 101; i++) 
        {
            this.transform.position -= vec;
            await UniTask.DelayFrame(1);
        }

        //モンスターの行動が可能に変更
        PlayerAttack.activeFlag = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!setupFlag) return;
        // 移動量と回転量を求める
        _horizontal = Input.GetAxis("HorizontalR");
        _vertical = Input.GetAxis("VerticalR");

        //元の位置を複製
        Vector3 _position = this.transform.position;
        
        //入力の値を使い移動先の角度を求める
        _cameraPositionAngle += (((_horizontal*PlayerStatus.Instance.data.sensibility)) / 3.14f * 180) * 0.0001f;
        _range += _vertical * 0.01f;

        //レンジの最小値を設定する
        if (_range < _minRange) { _range = _minRange; }

        //レンジの最大値を設定する
        if (_range > _maxRange) { _range = _maxRange; }

        //角度を使いカメラの座標を変更
        _position.y = ((_range / 3) * 2);
        _position.y += _player.transform.position.y;
        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;
        this.transform.position = _position;


        //カメラの座標からモンスターの方向にカメラを向かせる
        transform.LookAt(_player.transform.position + Vector3.up/10);
    }

    //カメラの現在の角度を使いカメラの座標を変更する関数
    public void SetCameraPosition()
    {

        Vector3 _position = this.transform.position;

        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;

        this.transform.position = _position;


    }
}
