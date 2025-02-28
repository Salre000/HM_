using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// ���C�X�̍s���_���̃N���X
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

    // ����s���p�x
    int avoidRatio = 7;

    // ����s���̃N�[���^�C��
    public float attackCoolTime = 6.0f;

    // ���łɃI���ɂȂ��Ă��邩�ǂ���
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
        p_audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.HunmerAttackSE, (int)Main.Hunter));
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
        Debug.Log("�n���}�[�̖ړI�n" + _agent.destination);
    }
}
