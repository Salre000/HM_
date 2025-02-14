using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenSet : MonoBehaviour
{
    public GameObject objectA; // �I�u�W�F�N�gA���C���X�y�N�^�Őݒ�
    public GameObject objectB; // �I�u�W�F�N�gB�̃v���n�u

    void Start()
    {
        if (objectA != null && objectB != null)
        {
            // ObjectA�̉���ObjectB�𐶐�
            GameObject newObjectB = Instantiate(objectB, objectA.transform);

            // �K�v�ɉ����ĐV������������B�I�u�W�F�N�g�̈ʒu�𒲐�
            newObjectB.transform.localPosition = Vector3.zero; // �ʒu�͐e�̈ʒu�ɍ��킹��
        }
        else
        {
            Debug.LogError("ObjectA�܂���ObjectB���ݒ肳��Ă��܂���");
        }
    }
}
