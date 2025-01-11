using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻撃時のエフェクトをクラス
public class AttackEffect : MonoBehaviour
{
    [Header("通常攻撃のかみつきエフェクトモデル")]
    [SerializeField] GameObject NomaleAttackEffect;
    Animator NomaleAttackAnimator;

    private void Awake()
    {

        NomaleAttackAnimator=NomaleAttackEffect.GetComponent<Animator>();

    }

    //通常攻撃エフェクトの描画をする関数
    public void NomaleAttadkEffectShow()
    {
        NomaleAttackEffect.SetActive(true);
        NomaleAttackAnimator.SetTrigger("AttackTrigger");

    }


}
