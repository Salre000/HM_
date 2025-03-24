using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 剣の行動論理を表すクラス    行動の基本
/// </summary>
public class AI4Logic : Hunter_AI
{

    public float keepDistance = 1.5f;

    public float detection2Radius = 0.07f;  // 相手が近づいた時に反応する距離
    public float flee2Distance = 2.5f;     // 離れる距離

    public float attackDistance = 2.0f;
    
    public float viewAngle = 180.0f;

    public float viewLength = 100;

    // 回避行動頻度
    int avoidRatio = 7;

    // 回避行動のクールタイム
    public float avoidCoolTime = 6.0f;

    private float attackCoolTime = 7.5f;

    public GameObject colliderObject;

    public override void Start()
    {
        base.Start();
        SetAttackCoolTime(attackCoolTime);
        SetAttackDistance(attackDistance);
        SetAvoidRatio(avoidRatio);
        SetViewAngle(viewAngle);
        SetViewLength(viewLength);
        SetKeepDistance(detectionRadius, flee2Distance);
        CloseCollider();
    }

    public override void Chase()
    {
        base.Chase();
        int random = Random.Range(0, 10);
        //if (random >= 3) SetDestination(GetMonsterBackPosition());
        //else
        //{
        //    SetDestination(GetMonsterLeftPosition());
        //}
        if (float.IsInfinity(_agent.destination.magnitude)) return;

        SetDestination(GetMonsterRightPosition());
    }

    public override void Attack()
    {
        base.Attack();
        if (CheckAudioSourceNull()) return;
        p_audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.PreSwordAttack, (int)Main.Hunter), SoundListManager.instance.GetSoundVolume());
    }

    public void SetCollider()
    {
        if(colliderObject != null) colliderObject.GetComponent<Collider>().enabled = true;
    }

    public void CloseCollider()
    {
        if (colliderObject != null) colliderObject.GetComponent<Collider>().enabled = false;
    }

    protected override void DebugDistance()
    {
        base.DebugDistance();
    }
}
