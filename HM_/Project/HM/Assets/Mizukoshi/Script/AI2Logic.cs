using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �s���_��
/// 
/// �@�G��������܂ł͜p�j
/// 
/// �A�G����������2�Ԗڂɋ߂��Ƃ���Ɉړ�
/// 
/// �B�_��(cool time��5�b)
/// 
/// �C�G���߂Â��Ă����瓦���� �������͋ߋ����U��
/// 
/// �D�ȍ~�J��Ԃ�
/// 
/// 
/// 
/// </summary>

// �|��AI�̍s���_��
public class AI2Logic :Hunter_AI
{
    //// Update is called once per frame
    //void Update()
    //{
    //    animationState = _animator.GetCurrentAnimatorStateInfo(0);
    //    // �����X�^�[�Ǝ����̋����𑪂�
    //    distance = Vector3.Distance(this.transform.position, _monster.transform.position);

    //    if (CheckDie())
    //    {
    //        _deathAnimationNow = true;
    //        AttackAnimationEnd();
    //        // ����A�j���[�V�������~�߂�
    //        _animator.SetBool("Walk", false);
    //        _animator.SetBool("WalkFinish", true);
    //        _animator.SetBool("isDeadFinish", false);
    //        _animator.SetBool("isDead", true);
    //    }

    //    if (_deathAnimationNow)
    //    {
    //        if (animationState.normalizedTime >= 0.75f && animationState.IsName("death2"))
    //        {
    //            // �I�����m
    //            deathAnimationFinish = true;
    //            _deathAnimationNow = false;
    //            _animator.SetBool("isDead", false);
    //            _animator.SetBool("isDeadFinish", true);
    //        }
    //        return;
    //    }

    //    // �����X�^�[�Ǝ����̋�����20�ȏ�ł���΃i�r���b�V���ɂ��ړ����s��
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
    //                // �U���̃A�j���[�V�����𗬂��B
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
    //        // ����A�j���[�V�������Đ�����
    //        _animator.SetBool("Walk", true);
    //        _animator.SetBool("WalkFinish", false);
    //    }
    //    else
    //    {
    //        // ����A�j���[�V�������~�߂�
    //        _animator.SetBool("Walk", false);
    //        _animator.SetBool("WalkFinish", true);
    //    }
    //}
}
