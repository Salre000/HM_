using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g�̖��O���擾
        string otherObjectName = collision.gameObject.name;

        // �R���\�[���Ƀ��b�Z�[�W��\��
        Debug.Log($"{otherObjectName}�ƏՓ˂��܂����I");

        // �����ŉ����ʂ̃A�N�V���������s�ł��܂�
        // ��: �X�R�A�����Z����A�I�u�W�F�N�g��j�󂷂�Ȃ�
    }
}
