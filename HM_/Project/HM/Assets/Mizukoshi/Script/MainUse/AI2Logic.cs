using SceneSound;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


// ã|ÇÃAIÇÃçsìÆò_óù
public class AI2Logic :Hunter_AI
{
    [SerializeField]
    // çUåÇãóó£
    private float attackDistance = 10.0f;

    [SerializeField]
    private float attackCoolTime = 5.0f;

    [SerializeField]
    private float viewAngle = 90.0f;

    [SerializeField]
    private float viewLength = 100;

    public GameObject Arrow;

    public GameObject ArrowPos;

    AudioSource audioSource;

    public override void Start()
    {
        SetAttackDistance(attackDistance);
        SetAttackCoolTime(attackCoolTime);
        SetAvoidRatio(0);
        SetViewAngle(viewAngle);
        SetViewLength(viewLength);
        audioSource = gameObject.AddComponent<AudioSource>();
        base.Start();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Chase()
    {
        base.Chase();
    }

    public void SetArch()
    {
        GameObject clone = GameObject.Instantiate(Arrow);
        Vector3 startPos = this.transform.position;
        startPos.y += 0.0750f;
        clone.transform.position = startPos;
        Vector3 dir = GetMonster().transform.position;
        dir.y += 0.0065f;
        clone.transform.LookAt(dir);
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.PreArechSE, (int)Main.Hunter));
        
    }
}
