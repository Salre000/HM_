using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnemySight2 : MonoBehaviour
{
    public float headTurnSpeed = 2f;  // ���U�鑬��
    private Vector3 targetRotation;

    void Start()
    {
        // �����̃^�[�Q�b�g��]��ݒ�
        SetRandomHeadRotation();
    }

    public void Update()
    {
        // ���U�鏈��
        Vector3 direction = targetRotation - transform.eulerAngles;
        if (direction.magnitude > 0.1f)
        {
            // ��]
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetRotation, Time.deltaTime * headTurnSpeed);
        }
        else
        {
            // �����_���ȕ����Ɋ��������
            SetRandomHeadRotation();
        }
    }

    void SetRandomHeadRotation()
    {
        // �����_���ȕ����Ɋ��������
        float randomX = Random.Range(-30f, 30f);  
        float randomY = Random.Range(0f, 360f); 
        targetRotation = new Vector3(randomX, randomY, 0f);
    }
}
