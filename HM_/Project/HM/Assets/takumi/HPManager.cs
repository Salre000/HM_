using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

//モンスターとハンターのHPを管理するスプリクト
public class HPManager : MonoBehaviour
{

    [SerializeField] int HunterCount = 4;
    //引数はハンターの数
    void Start()
    {
        MonsterHp = MaxMonsterHP;

        //ハンターの数を指定する
        HunterHp = new float[HunterCount];
        HunterInvincibilityTime = new float[HunterCount];


        //すべてのハンターのHPをリセットする
        for (int i = 0; i < HunterCount; i++)
        {
            HunterHp[i] = MaxHunterHp;
            HunterInvincibilityTime[i] = 0;

        }

    }

    private void FixedUpdate()
    {
        TimeCount();
    }

    //モンスターのダメージ処理
    public void MonsterDamage(float Damage)
    {
        if (MonsterInvincibilityTime != -1) return;
        MonsterHp -= Damage;
        MonsterInvincibilityTime = 0;
    }

    public void HunterDamage(float Damage, int Number)
    {
        if (HunterInvincibilityTime[Number] != -1) return;

        HunterHp[Number] -= Damage;
        HunterInvincibilityTime[Number] = 0;

    }

    //ハンターの回復の処理 引数１はHPを上書きする値
    public void HunterHeel(float Heel, int Number)
    {

        MonsterHp = Heel;

        if (HunterHp[Number] > MaxHunterHp) HunterHp[Number] = MaxHunterHp;


    }

    //無敵時間をカウントする関数
    void TimeCount()
    {
        //ハンターの無敵時間
        for (int i = 0; i < HunterInvincibilityTime.Length; i++)
        {
            if (HunterInvincibilityTime[i] < 0) continue;

            //時間を追加する
            HunterInvincibilityTime[i] = Time.deltaTime;

            if (HunterInvincibilityTime[i] > MaxInvincibilityTim) HunterInvincibilityTime[i] = -1;



        }

        if (MonsterInvincibilityTime < 0) return;
        MonsterInvincibilityTime += Time.deltaTime;

        if (MonsterInvincibilityTime > MaxInvincibilityTim) MonsterInvincibilityTime = -1;

    }

    //無敵時間の最大の時間
    const float MaxInvincibilityTim = 2;

    const float MaxMonsterHP = 1000;
    float GetMaxMonsterHp() { return MaxMonsterHP; }

    float MonsterHp = 0;

    float MonsterInvincibilityTime;


    const float MaxHunterHp = 100;

    float []HunterHp;

    float []HunterInvincibilityTime;
}
