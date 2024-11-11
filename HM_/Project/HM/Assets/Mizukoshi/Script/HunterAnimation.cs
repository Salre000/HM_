using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAnimation : MonoBehaviour
{
    private Animator _animator;

    AnimatorStateInfo animationState;

    // 無敵判定の最初のフレーム
    private float invincibleStart = 38;

    // 無敵判定の最後のフレーム
    private float invincibleEnd = 58;

    // 無敵時間のアニメーションの総時間
    private float invincibleTime = 84;

    public bool invincibleFlag=false;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        invincibleStart/=invincibleTime;
        invincibleEnd/=invincibleTime;
    }

    private void Update()
    {
        // 常にアニメーションの状態を保持
        animationState=_animator.GetCurrentAnimatorStateInfo(0);
        
        // 歩くアニメーションと立ち止まるアニメーション以外のアニメーションが終了したとき
        if (animationState.normalizedTime >= 1.0f&&!animationState.IsName("walk1")&& !animationState.IsName("idle1")) 
        {
            // 立ち止まるモーションにする。
            Idle();
        }

        // 無敵フレーム中ならば
        if (animationState.normalizedTime >= invincibleStart && animationState.normalizedTime <= invincibleEnd && animationState.IsName("mixamo_com"))
        {
            // 
            invincibleFlag = true;
            Debug.Log("無敵中");
        }
        else
        {
            invincibleFlag = false;
        }
    }

    public void Idle()
    {
        ResetAnimation();
        _animator.SetBool("idleFlag", true);
    }

    public void Attack()
    {
        ResetAnimation(); 
        _animator.SetBool("attackFlag", true);
    }

    public void Walk()
    {
        ResetAnimation();
        _animator.SetBool("walkFlag", true);
    }

    public void Falter()
    {
        ResetAnimation();
        _animator.SetBool("impactFlag", true);
    }

    public void Death()
    {
        ResetAnimation();
        _animator.SetBool("deathFlag", true);
    }

    public void Roling()
    {
        ResetAnimation();
        _animator.SetBool("rollingFlag", true);
    }

    // アニメーションのリセットを起こす
    void ResetAnimation()
    {
        _animator.SetBool("idleFlag", false);
        _animator.SetBool("attackFlag", false);
        _animator.SetBool("impactFlag", false);
        _animator.SetBool("deathFlag", false);
        _animator.SetBool("walkFlag", false);
        _animator.SetBool("rollingFlag", false);
    }
}
