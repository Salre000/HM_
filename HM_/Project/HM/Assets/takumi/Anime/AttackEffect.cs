using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//攻撃時のエフェクトをクラス
public class AttackEffect : MonoBehaviour
{
    [Header("通常攻撃のエフェクトモデル")]
    [SerializeField] GameObject NomaleAttackEffect;

    [Header("咆哮時に見えるエフェクトモデル")]
    [SerializeField] GameObject RoarAttackEffect;

    Animator NomaleAttackAnimator;


    private void Awake()
    {

        NomaleAttackAnimator=NomaleAttackEffect.GetComponent<Animator>();

    }

    //通常攻撃エフェクトの描画をする関数
    public void NormalAttackEffectShow()
    {
        NomaleAttackEffect.SetActive(true);
        NomaleAttackAnimator.SetTrigger("AttackTrigger");

    }

    public void RoarShow() 
    {
        RoarAttackEffect.SetActive(true);
    }
    public void RoarEnd() 
    {
        RoarAttackEffect.SetActive(false);

    }


}
