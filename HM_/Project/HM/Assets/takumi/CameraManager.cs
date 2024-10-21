using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// �J�����̊Ǘ�������v���O����
public class CameraManager : MonoBehaviour
{
    //�@�J�����̉�]���x�@�i���W�A���j
    [SerializeField]private float _cameraSpeed;

    //�v���C���[�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject _player;

    //�v���C���[�ƃJ�����̋���
   [SerializeField] private float _range=100;

    [SerializeField] private float _horizontal;
    [SerializeField] private float _vertical;

    [SerializeField]private float _cameraPositionAngle = 3.14f;

    public float Get_CameraPositionAngle() {  return _cameraPositionAngle+(180*3.14f/180); }

    public void Add_CameraPositionAngle(float angle) { _cameraPositionAngle += angle; }
    // Start is called before the first frame update
    void Start()
    {

        _player = GameObject.FindGameObjectWithTag("Player");
        Vector3 _position = this.transform.position;

        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;
        _position.y = _player.transform.position.y+20;

        this.transform.position = _position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_player.transform.position+Vector3.up*10);

        // �ړ��ʂƉ�]�ʂ����߂�
        _horizontal = Input.GetAxis("HorizontalR");
        _vertical = Input.GetAxis("VerticalR");

        if (_horizontal == 0 && _vertical == 0) return;

        Vector3 _position = this.transform.position;

        _cameraPositionAngle +=( (_horizontal)/3.14f*180)*0.0001f;

        _range += (_vertical)*0.1f;

        //�����W�̍ŏ��l��ݒ肷��
        if (_range < 20.0f) { _range = 20.0f; }

        //�����W�̍ő�l��ݒ肷��
        if (_range > 100) {  _range = 100; }

        _position.y=_player.transform.position.y+20+(_range/10);

        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;

        this.transform.position = _position;    







    }

    public  void SetCameraPosition() 
    {

        Vector3 _position = this.transform.position;

        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;

        this.transform.position = _position;


    }
}
