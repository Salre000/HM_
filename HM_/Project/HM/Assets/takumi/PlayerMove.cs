using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�v���C���[�𓮂����N���X
public class PlayerMove : MonoBehaviour
{

    Vector3 PlayerPosition;

    [SerializeField] private float _horizontal;
    [SerializeField] private float _vertical;


    //�v���C���[�̊p�x
    [SerializeField] private float _angle;

    [SerializeField]private string MoveAnimeName = "Armature|Move";

    //�p�x�̍�
    [SerializeField] private float _angleDifference;

    private PlayerStatus _status;

    [SerializeField] private CameraManager _manager;

    public Vector3 pos;

    //�p�x��^����֐�
    public void SetAngle(float angle) { _angle = angle; }
    private AudioSource audioSource;

    private bool _moveFlag = true;
    Animator _animator;
    private PlayerAnime _anime;

    void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();

        //���W�����̍��W�ɍX�V����v���O����
        PlayerPosition = this.transform.position;

        // this.Roar.AddComponent<PlayerStatus>();

        _status = this.GetComponent<PlayerStatus>();


        _angle = _manager.Get_CameraPositionAngle() * 180 / 3.14f;

        _anime = this.gameObject.GetComponent<PlayerAnime>();

        audioSource=GetComponent<AudioSource>();
    }

    private bool Flag = false;
    // Update is called once per frame

    private void FixedUpdate()
    {

        if (!CameraManager.setupFlag) return;
        if(this.transform.position.y<=-10)this.transform.position=new Vector3(this.transform.position.x,2,this.transform.position.z);

        string NowAnime = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        pos = Vector3.zero;
        _horizontal = _vertical = 0;


        Vector3 Angles = this.transform.eulerAngles;

        Angles.y = _angle;
        this.transform.eulerAngles = Angles;


        // �ړ��ʂƉ�]�ʂ����߂�
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        _anime.SetMoveFlag(Flag);


        if (_horizontal == 0 && _vertical == 0) return;

        if (NowAnime == "Armature|Moves" || NowAnime == "Armature|AttackMove" || NowAnime == "Armature|AttackMoveLoops" || NowAnime == MoveAnimeName) 
        {
            int sss = 0;
        }
        else { _horizontal = 0; _vertical = 0; }
        // Debug.Log(NowAnime);

        _anime.SetMoveFlag(true);
        _angle += (_horizontal) * _status.GetRotateSpeed();

        _manager.Add_CameraPositionAngle((_horizontal * _status.GetRotateSpeed()) * 3.14f / 180);

        pos = this.transform.position;

        _vertical /= 10;

        //�v���C���[�̈ړ�
        pos.x += Mathf.Sin(_angle * 3.14f / 180) * (_vertical * _status.GetSpeed());
        pos.z += Mathf.Cos(_angle * 3.14f / 180) * (_vertical * _status.GetSpeed());



        this.transform.position = pos;
        

    }
}
