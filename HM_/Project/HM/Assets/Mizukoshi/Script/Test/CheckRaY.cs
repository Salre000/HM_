using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRaY : MonoBehaviour
{
    public GameObject person1; 
    public GameObject person2; 
    public GameObject block;   



    void Update()
    {
        // Ray���΂��F�l1����l2�Ɍ�����
        Ray ray = new Ray(person1.transform.position, person2.transform.position - person1.transform.position);
        RaycastHit hit;

        // Ray�������ɏՓ˂���������
        if (Physics.Raycast(ray, out hit))
        {
            // �Փ˂����I�u�W�F�N�g���u���b�N�������ꍇ
            if (hit.collider.gameObject == block)
            {
                Debug.Log("1����2�̓u���b�N�ɂ���ĎՂ��Ă��܂��B");
            }
            else
            {
                Debug.Log("1����2�͌����܂��B");
            }
        }
        else
        {
            Debug.Log("1����2�͌����܂��B");
        }

        // Ray�̉����i�f�o�b�O�p�j
        Debug.DrawRay(person1.transform.position, person2.transform.position - person1.transform.position, Color.green);
    }

    bool GetHitRayNone(Vector3 pos,Vector3 targetPos,GameObject obstacle)
    {
        // Ray���΂��F1����2�Ɍ�����
        Ray ray = new Ray(pos, targetPos - pos);
        RaycastHit hit;

        // Ray�̉����i�f�o�b�O�p�j
        Debug.DrawRay(pos, targetPos - pos);

        // Ray�������ɏՓ˂���������
        if (Physics.Raycast(ray, out hit))
        {
            // �Փ˂����I�u�W�F�N�g���ǂ������ꍇ
            if (hit.collider.gameObject == obstacle)
            {
                Debug.Log("1����2�͕ǂɂ���ĎՂ��Ă��܂��B");
                return false;
            }
            else
            {
                Debug.Log("1����2�͌����܂��B");
                return true;
            }
        }
        else
        {
            return true;
        }
    }
}
