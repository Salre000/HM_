using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSpeed : MonoBehaviour
{
    // ����
    public float ratio = 5;

    public float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeed/ratio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �v���C���[�̈ړ��X�s�[�h
    float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
