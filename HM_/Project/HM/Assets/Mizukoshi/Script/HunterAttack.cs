using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAttack : MonoBehaviour
{
    // アニメーター変数
    private Animator animator;
    bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        // 取得
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SwitchAttackAnimation();
        }
        // アニメーションの状態を取得
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        if (animationState.IsName("Attack"))
        {
            Debug.Log("終了");
        }
        else
        {
            if (attack)
            {
                Debug.Log("終了");
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
