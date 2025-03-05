using SceneSound;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


// �|��AI�̍s���_��
public class AI2Logic :Hunter_AI
{
    

    public GameObject Arrow;

    public GameObject ArrowPos;


    public float detection2Radius = 1f;  // ���肪�߂Â������ɔ������鋗��
    public float flee2Distance = 2.5f;     // ����鋗��

    public float attackDistance = 1.0f;

    public float viewAngle = 180.0f;

    public float viewLength = 100;

    // ����s���p�x
    int avoidRatio = 0;

    // ����s���̃N�[���^�C��
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

        //����ړ������A�I���ɂ���
        arrowList.SetArrow(startPos, dir);
        
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.PreArechSE, (int)Main.Hunter));
        
    }

    protected override void DebugDistance()
    {
        Debug.Log("�|�̖ړI�n" + _agent.destination);
    }
}
