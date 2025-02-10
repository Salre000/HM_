using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�U�����̃G�t�F�N�g���N���X
public class AttackEffect : MonoBehaviour
{
    [Header("�ʏ�U���̃G�t�F�N�g���f��")]
    [SerializeField] GameObject NomaleAttackEffect;

    [Header("���K���Ɍ�����G�t�F�N�g���f��")]
    [SerializeField] GameObject RoarAttackEffect;

    Animator NomaleAttackAnimator;


    private void Awake()
    {

        NomaleAttackAnimator=NomaleAttackEffect.GetComponent<Animator>();

    }

    //�ʏ�U���G�t�F�N�g�̕`�������֐�
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
