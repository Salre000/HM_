using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI2AnimationTest : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animatorコンポーネントを取得
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // プレイヤーが「W」キーを押した場合に歩くアニメーションを再生
        if (Input.GetKey(KeyCode.W))
        {
            animator.Play("Walk");
        }
        // プレイヤーが「A」キーを押した場合に攻撃アニメーションを再生
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Attack");
        }
    }
}
