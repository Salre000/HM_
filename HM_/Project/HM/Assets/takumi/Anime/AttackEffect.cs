using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�U�����̃G�t�F�N�g���N���X
public class AttackEffect : MonoBehaviour
{
    [Header("�ʏ�U���̂��݂��G�t�F�N�g���f��")]
    [SerializeField] GameObject NomaleAttackEffect;
    Animator NomaleAttackAnimator;

    private void Awake()
    {

        NomaleAttackAnimator=NomaleAttackEffect.GetComponent<Animator>();

    }

    //�ʏ�U���G�t�F�N�g�̕`�������֐�
    public void NomaleAttadkEffectShow()
    {
        NomaleAttackEffect.SetActive(true);
        NomaleAttackAnimator.SetTrigger("AttackTrigger");

    }


}
