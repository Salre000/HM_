using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI2AnimationTest : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator�R���|�[�l���g���擾
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // �v���C���[���uW�v�L�[���������ꍇ�ɕ����A�j���[�V�������Đ�
        if (Input.GetKey(KeyCode.W))
        {
            animator.Play("Walk");
        }
        // �v���C���[���uA�v�L�[���������ꍇ�ɍU���A�j���[�V�������Đ�
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Attack");
        }
    }
}
