using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAttack : MonoBehaviour
{
    // �A�j���[�^�[�ϐ�
    private Animator animator;
    bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        // �擾
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SwitchAttackAnimation();
        }
        // �A�j���[�V�����̏�Ԃ��擾
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        if (animationState.IsName("Attack"))
        {
            Debug.Log("�I��");
        }
        else
        {
            if (attack)
            {
                Debug.Log("�I��");
                animator.SetBool("Attack", false);
                attack = animator.GetBool("Attack");
            }
        }
    }


    void SwitchAttackAnimation()
    {
        attack = animator.GetBool("Attack");
        animator.SetBool("Attack", true);
    }
}
