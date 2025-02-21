using SceneSound;
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
        Debug.Log($"Collider{otherObjectName}�ƏՓ˂��܂����I");

        // �����ŉ����ʂ̃A�N�V���������s�ł��܂�
        // ��: �X�R�A�����Z����A�I�u�W�F�N�g��j�󂷂�Ȃ�
    }

    private void OnTriggerEnter(Collider other)
    {
        string ObjName=other.gameObject.name;

        Debug.Log($"Trigger{ObjName}�ƏՓ˂��܂����I");

        if (ObjName == "�h���S��1")
        {
            SoundListManager.instance.PlaySound((int)HunterSE.ArechSE, (int)Main.Hunter);
        }
    }
}
