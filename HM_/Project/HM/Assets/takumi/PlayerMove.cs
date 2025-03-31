using Cysharp.Threading.Tasks;
using SceneSound;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
//プレイヤーを動かすクラス
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _horizontal;
    [SerializeField] private float _vertical;


    //プレイヤーの角度
    [SerializeField] private float _angle;

    [SerializeField] private string MoveAnimeName = "Armature|Move";

    //角度の差
    [SerializeField] private float _angleDifference;

    private PlayerStatus _status;

    [SerializeField] private CameraManager _manager;

    public Vector3 pos;

    //角度を与える関数
    public void SetAngle(float angle) { _angle = angle; }
    private AudioSource audioSource;

    Animator _animator;
    private PlayerAnime _anime;

    private Vector3 respawnPosition;

    private Camera _camera;

    [SerializeField] float Speed = 0.7f;

    private void SetSpeed(float speed) 
    {
        Speed = speed;
    }
    void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();

        _status = this.GetComponent<PlayerStatus>();

        _status.GetSet_SetSpeed(SetSpeed);

        _angle = _manager.Get_CameraPositionAngle() * 180 / 3.14f;

        _anime = this.gameObject.GetComponent<PlayerAnime>();

        audioSource = GetComponent<AudioSource>();

        respawnPosition = this.transform.position;
        _camera = Camera.main;
    }

    private bool Flag = false;
    // Update is called once per frame

    private void FixedUpdate()
    {
        if (!PlayerStatus.isLife) return;

        if (!CameraManager.useFlag) return;
        if (this.transform.position.y <= -10) this.transform.position = respawnPosition;

        LookAtMove();
    }

    void LookAtMove()
    {

        string NowAnime = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;


        pos = Vector3.zero;
        _horizontal = _vertical = 0;
        _angle = 0;


        Vector3 Angles = _camera.transform.position - this.transform.position;

        _angle = Mathf.Atan2(Angles.x, Angles.z);



        // 移動量と回転量を求める
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        _anime.SetMoveFlag(Flag);


        if (_horizontal == 0 && _vertical == 0) return;

        if (NowAnime == "Armature|Moves" || NowAnime == "Armature|AttackMove" || NowAnime == "Armature|AttackMoveLoops" || NowAnime == MoveAnimeName)
        {
        }
        else { _horizontal = 0; _vertical = 0; }
        // Debug.Log(NowAnime);

        _anime.SetMoveFlag(true);
        if (_horizontal == 0 && _vertical == 0) return;

        _vertical *= -1;
        _horizontal *= -1;

        float angle = Mathf.Atan2(_horizontal, _vertical);
        float vecAngle = angle - _angle;
        _angle += angle;


        this.transform.eulerAngles = new Vector3(0, _angle * Mathf.Rad2Deg, 0);

        _manager.Add_CameraPositionAngle(vecAngle);

        pos = this.transform.position;


        //プレイヤーの移動
        pos.x += Mathf.Sin(_angle) * (Speed * _status.GetSpeed());
        pos.z += Mathf.Cos(_angle) * (Speed * _status.GetSpeed());



        this.transform.position = pos;


    }
}
