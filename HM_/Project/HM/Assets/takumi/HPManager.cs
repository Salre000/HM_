using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

//�����X�^�[�ƃn���^�[��HP���Ǘ�����X�v���N�g
public class HPManager : MonoBehaviour
{

    [SerializeField] int HunterCount = 4;
    //�����̓n���^�[�̐�
    void Start()
    {
        MonsterHp = MaxMonsterHP;

        //�n���^�[�̐����w�肷��
        HunterHp = new float[HunterCount];
        HunterInvincibilityTime = new float[HunterCount];


        //���ׂẴn���^�[��HP�����Z�b�g����
        for (int i = 0; i < HunterCount; i++)
        {
            HunterHp[i] = MaxHunterHp;
            HunterInvincibilityTime[i] = 0;

        }

    }

    private void FixedUpdate()
    {
        TimeCount();
        SetHunterLostNumber();
    }

    //�����X�^�[�̃_���[�W����
    public void MonsterDamage(float Damage)
    {
        if (MonsterInvincibilityTime != -1) return;
        MonsterHp -= Damage;
        MonsterInvincibilityTime = 0;
    }

    public void HunterDamage(float Damage, int Number)
    {
        //if (HunterInvincibilityTime[Number] != -1) return;

        HunterHp[Number] -= Damage;
        HunterInvincibilityTime[Number] = 0;

    }

    //�n���^�[�̉񕜂̏��� �����P��HP���㏑������l
    public void HunterHeel(float Heel, int Number)
    {

        MonsterHp = Heel;

        if (HunterHp[Number] > MaxHunterHp) HunterHp[Number] = MaxHunterHp;


    }

    //���G���Ԃ��J�E���g����֐�
    void TimeCount()
    {
        //�n���^�[�̖��G����
        for (int i = 0; i < HunterInvincibilityTime.Length; i++)
        {
            if (HunterInvincibilityTime[i] < 0) continue;

            //���Ԃ�ǉ�����
            HunterInvincibilityTime[i] = Time.deltaTime;

            if (HunterInvincibilityTime[i] > MaxInvincibilityTim) HunterInvincibilityTime[i] = -1;



        }

        if (MonsterInvincibilityTime < 0) return;
        MonsterInvincibilityTime += Time.deltaTime;

        if (MonsterInvincibilityTime > MaxInvincibilityTim) MonsterInvincibilityTime = -1;

    }

    //���G���Ԃ̍ő�̎���
    const float MaxInvincibilityTim = 2;

    const float MaxMonsterHP = 1000;
    public float GetMaxMonsterHp() { return MaxMonsterHP; }

   [SerializeField] float MonsterHp = 0;
    public float GetMonsterHp() { return  MonsterHp; }

    float MonsterInvincibilityTime;


    const float MaxHunterHp = 100;

    [SerializeField]
    float []HunterHp;

    float []HunterInvincibilityTime;

    int HunterLostNumber = -1;

    public void SetHunterLostNumber() 
    {
        for(int i = 0; i < HunterCount; i++) 
        {
            if (HunterLostNumber == i) continue;

            if (HunterHp[i]<=0)HunterLostNumber = i;

        }
    }
    public void SetHunterLostNumber(int Number) 
    {
        HunterLostNumber = Number;
    }

    public int GetHunterLostNumber() 
    {

        return HunterLostNumber;
    }
}
