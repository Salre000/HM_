using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAnimation : MonoBehaviour
{
    private Animator _animator;

    AnimatorStateInfo animationState;

    // ���G����̍ŏ��̃t���[��
    private float invincibleStart = 38;

    // ���G����̍Ō�̃t���[��
    private float invincibleEnd = 58;

    // ���G���Ԃ̃A�j���[�V�����̑�����
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
        // ��ɃA�j���[�V�����̏�Ԃ�ێ�
        animationState=_animator.GetCurrentAnimatorStateInfo(0);
        
        // �����A�j���[�V�����Ɨ����~�܂�A�j���[�V�����ȊO�̃A�j���[�V�������I�������Ƃ�
        if (animationState.normalizedTime >= 1.0f&&!animationState.IsName("walk1")&& !animationState.IsName("idle1")) 
        {
            // �����~�܂郂�[�V�����ɂ���B
            Idle();
        }

        // ���G�t���[�����Ȃ��
        if (animationState.normalizedTime >= invincibleStart && animationState.normalizedTime <= invincibleEnd && animationState.IsName("mixamo_com"))
        {
            // 
            invincibleFlag = true;
            Debug.Log("���G��");
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

    // �A�j���[�V�����̃��Z�b�g���N����
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
