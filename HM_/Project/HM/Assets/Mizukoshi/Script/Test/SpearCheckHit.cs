using Den.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCheckHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // �Փ˂����I�u�W�F�N�g�̖��O���擾
        string otherObjectName = other.gameObject.name;

        // �R���\�[���Ƀ��b�Z�[�W��\��
        Debug.Log($"����{otherObjectName}���Փ˂��܂����I");

        // �����ŉ����ʂ̃A�N�V���������s�ł��܂�
        // ��: �X�R�A�����Z����A�I�u�W�F�N�g��j�󂷂�Ȃ�
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g�̖��O���擾
        string otherObjectName = collision.gameObject.name;

        // �R���\�[���Ƀ��b�Z�[�W��\��
        Debug.Log($"����{otherObjectName}���Փ˂��܂����I");

        // �����ŉ����ʂ̃A�N�V���������s�ł��܂�
        // ��: �X�R�A�����Z����A�I�u�W�F�N�g��j�󂷂�Ȃ�
    }
}

