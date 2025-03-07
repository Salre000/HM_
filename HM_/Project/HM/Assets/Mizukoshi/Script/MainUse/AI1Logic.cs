using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// メイスの行動論理のクラス
/// </summary>
public class AI1Logic :Hunter_AI
{
    private Vector3 destination;

    private Vector3 respoenPos;

    public float keepDistance = 1.5f/10/4;

    public float attackDistance = 2.0f/10/4;

    public float viewAngle = 180.0f;

    public float viewLength = 100 / 10/4;

    public GameObject colliderObj;

    // 回避行動頻度
    int avoidRatio = 7;

    // 回避行動のクールタイム
    public float attackCoolTime = 6.0f;

    // すでにオンになっているかどうか
    private bool alreadyFlag=false;

   public override void Start()
   {
        base.Start();
        SetAttackCoolTime(attackCoolTime);
        SetAttackDistance(attackDistance);
        SetViewAngle(viewAngle);
        SetViewLength(viewLength);  
        respoenPos=this.transform.position;
        _spwnPosition=respoenPos;
        CloseCollider();
   }

    public override void Chase()
    {
        base.Chase();
        SetDestination(GetMonsterFrontPosition());
    }

    public override void Attack()
    {
        base.Attack();
        if(CheckAudioSourceNull())return;
        p_audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.PreSpearAttack, (int)Main.Hunter),SoundListManager.instance.GetSoundVolume());
        Debug.Log("BBB");
    }

    public void SetCollider()
    {
        if (colliderObj == null)return;
        colliderObj.GetComponent<Collider>().enabled = true;
    }

    public void CloseCollider()
    {
        if (colliderObj == null) return;
        colliderObj.GetComponent<Collider>().enabled = false;
    }

    protected override void DebugDistance()
    {
        Debug.Log("ハンマーの目的地" + _agent.destination);
    }
}
