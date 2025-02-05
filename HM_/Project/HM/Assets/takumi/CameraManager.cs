using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// �J�����̊Ǘ�������v���O����
public class CameraManager : MonoBehaviour
{
    //�@�J�����̉�]���x�@�i���W�A���j
    [SerializeField] private float _cameraSpeed;

    //�v���C���[�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject _player;
    [SerializeField] private UIManager _manager;
    //�v���C���[�ƃJ�����̋���
    [SerializeField] private float _range = 2.0f;

    [SerializeField] private float _horizontal;
    [SerializeField] private float _vertical;

    [SerializeField] private const float _minRange = 2.0f;
    [SerializeField] private const float _maxRange = 10.0f;

    [SerializeField] private float _cameraPositionAngle = 3.14f;

    [SerializeField] GameObject risuPos;

    public float Get_CameraPositionAngle() { return _cameraPositionAngle + (180 * 3.14f / 180); }

    public void Add_CameraPositionAngle(float angle) { _cameraPositionAngle += angle; }

    bool setupFlag = false;
    // Start is called before the first frame update
    void Start()
    {

        Setup();
    }

    async UniTask Setup()
    {

        await StartShowHunter();

        Vector3 _position = this.transform.position;

        Vector3 vec= risuPos.transform.position - _player.transform.position;
        _cameraPositionAngle = Mathf.Atan2(vec.z, vec.x);
        _range =Vector3.Distance(this.transform.position,_player.transform.position);


        //�����W�̍ŏ��l��ݒ肷��
        if (_range < _minRange) { _range = _minRange; }

        //�����W�̍ő�l��ݒ肷��
        if (_range > _maxRange) { _range = _maxRange; }

        _position.y = (_range / 3) * 2;

        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;

        this.transform.position = _position;



        transform.LookAt(_player.transform.position + Vector3.up);


        setupFlag = true;
    }
    const float risurange = 5;
    float resuAngle = 0;
    //�Q�[���J�n���Ƀn���^�[���ڂ��֐�
    async UniTask StartShowHunter()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        Vector3 vec =risuPos.transform.position- _player.transform.position;

        resuAngle =Mathf.Atan2(vec.z, vec.x);
        this.transform.position = risuPos.transform.position + new Vector3(Mathf.Sin(resuAngle + 1* Mathf.Deg2Rad) * risurange, 3, Mathf.Cos(resuAngle + 1 * Mathf.Deg2Rad) * risurange);
        this.transform.LookAt(risuPos.transform);

        await UniTask.DelayFrame(50);
        Debug.Log(resuAngle);
        for (int i = 0; i < 180; i++)
        {

            this.transform.position = risuPos.transform.position + new Vector3( Mathf.Sin(resuAngle + ((i * 2)) * Mathf.Deg2Rad) * risurange, 3, Mathf.Cos(resuAngle + ((i * 2)) * Mathf.Deg2Rad) * risurange);
            this.transform.LookAt(risuPos.transform);

            await UniTask.DelayFrame(1);




        }

        vec /= 100;

        for(int i = 0; i < 101; i++) 
        {

            this.transform.position -= vec;

            await UniTask.DelayFrame(1);
        }






    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!setupFlag) return;

        // �ړ��ʂƉ�]�ʂ����߂�
        _horizontal = Input.GetAxis("HorizontalR");
        _vertical = Input.GetAxis("VerticalR");

        if (_horizontal == 0 && (_vertical <= 0 && _vertical >= -0.5f)) return;

        Vector3 _position = this.transform.position;

        _cameraPositionAngle += (((_horizontal) * _manager.GetSensibility()) / 3.14f * 180) * 0.0001f;

        _range += _vertical * 0.1f;

        //�����W�̍ŏ��l��ݒ肷��
        if (_range < _minRange) { _range = _minRange; }

        //�����W�̍ő�l��ݒ肷��
        if (_range > _maxRange) { _range = _maxRange; }

        _position.y = (_range / 3) * 2;

        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;

        this.transform.position = _position;



        transform.LookAt(_player.transform.position + Vector3.up);

        


    }

    public void SetCameraPosition()
    {

        Vector3 _position = this.transform.position;

        _position.x = _player.transform.position.x + Mathf.Sin(_cameraPositionAngle) * _range;
        _position.z = _player.transform.position.z + Mathf.Cos(_cameraPositionAngle) * _range;

        this.transform.position = _position;


    }
}
