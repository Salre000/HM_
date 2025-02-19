using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

//�����X�^�[�ƃn���^�[��HP���Ǘ�����X�v���N�g
public class HPManager : MonoBehaviour
{

    [SerializeField] int HunterCount = 4;
    [SerializeField] UIManager uiManager;
    //�����̓n���^�[�̐�
    void Start()
    {
       // uiManager=GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
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
    public int MonsterDamage(float Damage,ref float PartHp,bool DownFlag)
    {
        if (MonsterInvincibilityTime != -1) return -1;
        MonsterHp -= Damage;
        MonsterInvincibilityTime = 0;
        uiManager.HPSliderUpdate();
        if (DownFlag) return 1;   
        PartHp-= Damage;

        return 1;

    }

    public void HunterDamage(float Damage, int Number)
    {
        //if (HunterInvincibilityTime[Number] != -1) return;

        HunterHp[Number] -= Damage;
        HunterInvincibilityTime[Number] = 0;

        if (HunterHp[Number] < 0)
        {
            Debug.Log("AAA");
        }
    }

    //�n���^�[�̉񕜂̏��� �����P��HP���㏑������l
    public void HunterHeel(float Heel, int Number)
    {

        HunterHp[Number] = Heel;

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
    const float MaxInvincibilityTim = 0.5f;

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

            if (HunterHp[i] <= 0)
            {
                HunterLostNumber = i;


            }
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

    private bool lostNumberCountFlag=false;
    public async UniTask TimeCountLostNumber() 
    {
        int Count =0;
        while (true) 
        {
            if(Count>=1)break;

            await UniTask.DelayFrame(1);
            Count++;


            if (lostNumberCountFlag) continue;
            lostNumberCountFlag = true;

            Count = 0;
        }

        HunterLostNumber = -1;

    }
}
