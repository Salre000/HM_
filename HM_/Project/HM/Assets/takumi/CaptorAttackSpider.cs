using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static InputManager;

public class CaptorAttackSpider : AnimeBase
{

    System.Action _NestJump;
    //このアクションは拘束攻撃を辞めるアクション二つめはジャンプに切り替えるアクション
    public CaptorAttackSpider(GameObject Object, AudioSource source, Animator animator,
        System.Action<bool> animeFlagReset, System.Action nestJump, GameObject setPosition)
        : base(Object, source, animator, animeFlagReset)
    {
        AddAnimeName("Armature|RestraintAttackStart");
        AddAnimeName("Armature|RestraintAttackSuccess");
        AddAnimeName("Armature|RestraintAttackLoop");

        CaptorPosition = setPosition;
        HPManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
        CaptorHunter = CaptorPosition.GetComponent<CaptorHunter>();
        CaptorHunter.SetGameObject(SetTarget);
        _NestJump = nestJump;

    }

    public override void Start()
    {
        base.Start();
        eventNumber = 0;
        
    }
    private GameObject CaptorPosition;
    private GameObject CaptorTarget;
    private CaptorHunter CaptorHunter;
    Hunter_AI TargetHunter = null;
    private HPManager HPManager = null;

    public void SetCaptorObject(GameObject gameObject) { CaptorTarget = gameObject; }

    public void StartCaptor()
    {
        CaptorPosition.gameObject.SetActive(true);
        CaptorHunter.SetActiveFlag(true);
    }

    public void EndTarget()
    {
        if (CaptorTarget != null)
        {
            CaptorTarget.transform.parent = null;
        }
        CaptorPosition.gameObject.SetActive(false);

    }
    public void CheckHitHunter()
    {
        if (CaptorTarget == null)
        {
            _AnimeFlagReset(false);
        }
        else 
        {
            _AnimeFlagReset(true);
        }

    }
    int eventNumber = 0;
    public override void AnimeEvent()
    {
        switch (eventNumber)
        {

            case 0:
                StartCaptor();
                break;
            case 1:
                CheckHitHunter();
                break;
        }

        eventNumber++;
    }
    private void SetTarget(GameObject gameObject)
    {

        CaptorTarget = gameObject;
        CaptorTarget.transform.parent = CaptorPosition.transform;


        TargetHunter = CaptorTarget.GetComponent<Hunter_AI>();

        if (TargetHunter == null) return;
        TargetHunter.StartRestraining();

        CaptorTarget.transform.localPosition = Vector3.zero;


    }

    public override void Action()
    {
        AnimeUPDate();

        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Armature|RestraintAttackLoop" && !instance.IsOnButton(InputKeys.Normal))
            _AnimeFlagReset(false);


        //掴んでいるハンターが死んでいるかを判断
        if (TargetHunter == null) return;

        Hunter_AI hunter = TargetHunter.GetComponent<Hunter_AI>();

        if (hunter == null) return;

        if (hunter.GetHunterID() != HPManager.GetHunterLostNumber()) return;


    }

    override protected void AnimeEnd()
    {
        base.AnimeEnd();


        if ("Armature|Jump" == _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name)
        {
            Debug.Log(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name + "BBB");
            useFlag = true;

            _NestJump();
        }
        else
        {
            Debug.Log(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name+"SSS");
            useFlag = false;
        }

        EndTarget();

        if (TargetHunter != null)
            TargetHunter.StopRestraining();



    }
}
