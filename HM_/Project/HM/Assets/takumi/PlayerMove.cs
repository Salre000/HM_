using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�v���C���[�𓮂����N���X
public class PlayerMove : MonoBehaviour
{
    Vector3 PlayerPosition;

    private float _horizontal;
    private float _vertical;
    // Start is called before the first frame update
    void Start()
    {
        //���W�����̍��W�ɍX�V����v���O����
        PlayerPosition=this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = _vertical = 0;

        // �ړ��ʂƉ�]�ʂ����߂�
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");


        if (_horizontal == 0 && _vertical == 0) return;


        Vector3 position=this.transform.position;

        position.x += _horizontal;
        position.z += _vertical;





    }
}
