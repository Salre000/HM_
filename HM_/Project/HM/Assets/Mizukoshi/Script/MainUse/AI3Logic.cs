using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// やりで攻撃するハンターの行動論理をまとめたクラス
/// </summary>
public class AI3Logic : Hunter_AI
{
    public float keepDistance = 1.5f;

    public float attackDistance = 0.30f;

    public float viewAngle = 180.0f;

    public float viewLength = 100;

    public GameObject colliderObj;

    // 回避行動頻度
    int avoidRatio = 7;

    // 回避行動のクールタイム
    public float avoidCoolTime = 6.0f;

    public override void Start()
    {
        base.Start();
        SetAttackCoolTime(keepDistance);
        SetAttackDistance(attackDistance);
        SetViewAngle(viewAngle);
        SetViewLength(viewLength);
        SetAvoidRatio(avoidRatio);
        CloseCollider();
    }

    public override void Chase()
    {
        base.Chase();
        int random=Random.Range(0, 10);
        if(random >= 3) SetDestination(GetMonsterLeftPosition());
        else
        {
            SetDestination(GetMonsterRightPosition());
        }

    }

    public override void Attack()
    {
        base.Attack();
        if (CheckAudioSourceNull()) return;
        p_audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.SpearSE, (int)Main.Hunter));
    }

    public void SetCollider()
    {
        if (colliderObj == null) return;
        colliderObj.GetComponent<Collider>().enabled = true;
    }

    public void CloseCollider()
    {
        if (colliderObj == null) return;
        colliderObj.GetComponent<Collider>().enabled = false;
    }

    protected override void DebugDistance()
    {
        Debug.Log("やりの目的地" + _agent.destination);
    }
}
