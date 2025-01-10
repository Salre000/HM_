using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�v���C���[�𓮂����N���X
public class PlayerMove : MonoBehaviour
{

    Vector3 PlayerPosition;

   [SerializeField]  private float _horizontal;
    [SerializeField] private float _vertical;


    //�v���C���[�̊p�x
    [SerializeField] private float _angle;


    //�p�x�̍�
    [SerializeField] private float _angleDifference;

    private PlayerStatus _status;

    [SerializeField] private CameraManager _manager;

    public Vector3 pos;
   
    //�p�x��^����֐�
    public void SetAngle(float angle) {  _angle = angle; }

    private bool _moveFlag = true;
    Animator _animator;
    private PlayerAnime _anime;
    void Start()
    {
        _animator=GetComponent<Animator>();

        //���W�����̍��W�ɍX�V����v���O����
        PlayerPosition=this.transform.position;

       // this.Roar.AddComponent<PlayerStatus>();

        _status = this.GetComponent<PlayerStatus>();


        _angle = _manager.Get_CameraPositionAngle() * 180 / 3.14f;
    
        _anime=this.gameObject.GetComponent<PlayerAnime>();
    }

    private bool Flag = false;
    // Update is called once per frame
    void Update()
    {

        string NowAnime= _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

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

        if(NowAnime == "Armature|Moves" || NowAnime == "Armature|AttackMove" || NowAnime == "Armature|AttackMoveLoops") { }
        else { _horizontal = 0;_vertical = 0 ; }

        _anime.SetMoveFlag(true);  
        _angle += (_horizontal) *_status.GetRotateSpeed();

        _manager.Add_CameraPositionAngle((_horizontal * _status.GetRotateSpeed()) *3.14f/180);

        pos = this.transform.position;


        //�v���C���[�̈ړ�
        pos.x += Mathf.Sin(_angle*3.14f/180) *( _vertical * _status.GetSpeed());
        pos.z += Mathf.Cos(_angle * 3.14f / 180) *(_vertical * _status.GetSpeed());



        this.transform.position=pos;

    }
}
