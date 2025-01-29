using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 行動論理
/// 
/// ①敵を見つけるまでは徘徊
/// 
/// ②敵を見つけたら2番目に近いところに移動
/// 
/// ③狙撃(cool timeは5秒)
/// 
/// ④敵が近づいてきたら逃げる もしくは近距離攻撃
/// 
/// ⑤以降繰り返し
/// 
/// 
/// 
/// </summary>

// 弓のAIの行動論理
public class AI2Logic :Hunter_AI
{
    //// Update is called once per frame
    //void Update()
    //{
    //    animationState = _animator.GetCurrentAnimatorStateInfo(0);
    //    // モンスターと自分の距離を測る
    //    distance = Vector3.Distance(this.transform.position, _monster.transform.position);

    //    if (CheckDie())
    //    {
    //        _deathAnimationNow = true;
    //        AttackAnimationEnd();
    //        // 走るアニメーションを止める
    //        _animator.SetBool("Walk", false);
    //        _animator.SetBool("WalkFinish", true);
    //        _animator.SetBool("isDeadFinish", false);
    //        _animator.SetBool("isDead", true);
    //    }

    //    if (_deathAnimationNow)
    //    {
    //        if (animationState.normalizedTime >= 0.75f && animationState.IsName("death2"))
    //        {
    //            // 終了検知
    //            deathAnimationFinish = true;
    //            _deathAnimationNow = false;
    //            _animator.SetBool("isDead", false);
    //            _animator.SetBool("isDeadFinish", true);
    //        }
    //        return;
    //    }

    //    // モンスターと自分の距離が20以上であればナビメッシュによる移動を行う
    //    if (!CheckAttackDistance(10,this.gameObject))
    //    {

    //        RunAnimation();
    //    }
    //    else
    //    {
    //        agent.isStopped = true;
    //        _fight = true;
    //    }

    //    if (_fight)
    //    {

    //        if (agent.isStopped)
    //        {
    //            if (!attackNow)
    //            {
    //                waitTime += Time.deltaTime;
    //            }
    //            if (waitTime > AttackCoolTime)
    //            {
    //                // 攻撃のアニメーションを流す。
    //                _animator.SetBool("Attack", true);
    //                _animator.SetBool("AttackFinish", false);
    //                waitTime = 0;
    //            }
    //        }
    //    }
    //    if (animationState.normalizedTime >= 0.01f && animationState.IsName("ataka1"))
    //    {
    //        attackNow = true;
    //    }

    //    if (animationState.normalizedTime >= 0.75f && animationState.IsName("ataka1"))
    //    {
    //        AttackAnimationEnd();
    //    }
    //    if (!animationState.IsName("ataka1")) attackNow = false;
    //    if (!agent.isStopped)
    //    {
    //        // 走るアニメーションを再生する
    //        _animator.SetBool("Walk", true);
    //        _animator.SetBool("WalkFinish", false);
    //    }
    //    else
    //    {
    //        // 走るアニメーションを止める
    //        _animator.SetBool("Walk", false);
    //        _animator.SetBool("WalkFinish", true);
    //    }
    //}
}
