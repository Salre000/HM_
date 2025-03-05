using SceneSound;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


// 弓のAIの行動論理
public class AI2Logic :Hunter_AI
{
    

    public GameObject Arrow;

    public GameObject ArrowPos;


    public float detection2Radius = 1f;  // 相手が近づいた時に反応する距離
    public float flee2Distance = 2.5f;     // 離れる距離

    public float attackDistance = 1.0f;

    public float viewAngle = 180.0f;

    public float viewLength = 100;

    // 回避行動頻度
    int avoidRatio = 0;

    // 回避行動のクールタイム
    public float avoidCoolTime = 6.0f;

    private float attackCoolTime = 6.0f;

    public GameObject colliderObject;

    AudioSource audioSource;

    ArrowList arrowList;

    public override void Start()
    {
        audioSource = GetComponent<AudioSource>();
        base.Start();
        SetAttackCoolTime(attackCoolTime);
        SetAttackDistance(attackDistance);
        SetAvoidRatio(avoidRatio);
        SetViewAngle(viewAngle);
        SetViewLength(viewLength);
        CloseCollider();
        arrowList=GetComponentInChildren<ArrowList>();
        SetKeepDistance(detectionRadius,flee2Distance);
        
    }

    public override void Chase()
    {
        _agent.enabled = true;
        base.Chase();
        SetDestination(GetMonster().transform.position);
    }

    public override void Attack()
    {
        base.Attack();
        if (CheckAudioSourceNull()) return;
    }

    public void SetCollider()
    {
        if (colliderObject != null) colliderObject.GetComponent<Collider>().enabled = true;
    }

    public void CloseCollider()
    {
        if (colliderObject != null) colliderObject.GetComponent<Collider>().enabled = false;
    }

    public void SetArch()
    {
        //GameObject clone = GameObject.Instantiate(Arrow);
        Vector3 startPos = this.transform.position;
        startPos.y += 0.0750f;
        //clone.transform.position = startPos;
        Vector3 dir = GetMonster().transform.position;
        dir.y += 0.0665f;
        //clone.transform.LookAt(dir);

        if(arrowList==null) return;

        //矢を移動させ、オンにする
        arrowList.SetArrow(startPos, dir);
        
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.PreArechSE, (int)Main.Hunter));
        
    }

    protected override void DebugDistance()
    {
        Debug.Log("弓の目的地" + _agent.destination);
    }
}
