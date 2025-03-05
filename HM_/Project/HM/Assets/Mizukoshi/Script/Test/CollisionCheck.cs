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

        GameObject parent=this.gameObject.transform.parent.parent.gameObject;

        PlayerStatus ste = other.transform.gameObject.GetComponentInParent<PlayerStatus>();
        if(ste!= null)
        {
            //hPManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
            //int damege = 30;
            //float part = damege / 2;
            //hPManager.MonsterDamage(damege, ref part, false);
            parent.SetActive(false);
        }

        if (CheckCollisionTerrain(other)) parent.SetActive(false);




    }

    bool  CheckCollisionTerrain(Collider co)
    {
        Transform check=GetTopLevelParent(co.transform);
        if(check.name== "Terrain")return true;
        return false;
    }

    Transform GetTopLevelParent(Transform currentTransform)
    {
        // ���݂̐e��null�łȂ�����A�e�����ǂ葱����
        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
        }
        return currentTransform;  // �ŏ�ʂ̐e��Ԃ�
    }
}
