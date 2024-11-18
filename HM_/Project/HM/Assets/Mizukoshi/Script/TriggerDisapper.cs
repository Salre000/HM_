using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisapper : MonoBehaviour
{
    public GameObject spearHuman;

    public GameObject objectToClone;  // ��������I�u�W�F�N�g
    private GameObject clonedObject;  // ���������I�u�W�F�N�g�̎Q��

    public bool condition = false;  // �������`�F�b�N���邽�߂̃t���O

    void Update()
    {
        condition = spearHuman.GetComponent<Hunter_AI>().GetAttackState();
        // �������^�Ȃ�I�u�W�F�N�g�𐶐�
        if (condition && clonedObject == null)
        {
            // �I�u�W�F�N�g�𐶐��i�ʒu�� (0, 0, 0) �ɐݒ�j
            clonedObject = Instantiate(objectToClone, new Vector3(0, 0, 0), Quaternion.identity);
            clonedObject.transform.SetParent(this.transform, false);
        }
        // �������U�Ȃ�I�u�W�F�N�g��j��
        else if (!condition && clonedObject != null)
        {
            // �I�u�W�F�N�g��j��
            Destroy(clonedObject);
            clonedObject = null;  // �Q�Ƃ��N���A
        }
    }
}


//spearHuman.GetComponent<Hunter_AI>().GetAttackState()