using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public HPManager hPManager;

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
        GameObject collisionObj=other.gameObject.transform.parent.gameObject;
        GameObject checkObj = collisionObj.transform.GetChild(0).gameObject;

        //if (collisionObj.tag == "Hunter") return;

        if (checkObj.tag=="Player")
        {
            float d = 200;
            hPManager = GameObject.Find("GameManager").GetComponent<HPManager>();
            hPManager.MonsterDamage(10, ref d, false);
            SoundListManager.instance.PlaySound((int)HunterSE.ArechSE, (int)Main.Hunter);
        }
        Destroy(this.gameObject);
    }
}
