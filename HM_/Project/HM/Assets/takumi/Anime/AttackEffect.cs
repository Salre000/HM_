using Cysharp.Threading.Tasks;
using SceneSound;
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
    public void NomaleAttadkEffectShow()
    {
        NomaleAttackEffect.SetActive(true);
        NomaleAttackAnimator.SetTrigger("AttackTrigger");

    }

    public void RoarShow() 
    {
        RoarAttackEffect.SetActive(true);
        RoarEnd().Forget();
    }
    public async UniTask RoarEnd() 
    {
        await UniTask.DelayFrame(70);
        if (RoarAttackEffect == null) return;
        RoarAttackEffect.SetActive(false);

    }


}
