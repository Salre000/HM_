using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptorAttackSpider : AnimeBase
{
    private GameObject CaptorPosition;
    private GameObject CaptorTarget;
    private PlayerAttackSpider PlayerAttackSpider;
    private CaptorHunter CaptorHunter;
    Hunter_AI TargetHunter = null;
    private HPManager HPManager = null;
    public void SetUp(GameObject SetCaptor, PlayerAttackSpider playerAttack)
    {

        PlayerAttackSpider = playerAttack;
        CaptorPosition = SetCaptor;
        CaptorHunter = CaptorPosition.GetComponent<CaptorHunter>();
        CaptorHunter.SetGameObject(SetTarget);
        HPManager=GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();


    }
    private void Awake()
    {
        AddAnimeName("Armature|RestraintAttackStart");
        AddAnimeName("Armature|RestraintAttackSuccess");
        AddAnimeName("Armature|RestraintAttackLoop");

    }

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
        if (CaptorTarget != null)
        {
            PlayerAttackSpider.IsCapFlag = true;
        }
        else
        {
            PlayerAttackSpider.SetULTFLag(false);
        }


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

    void FixedUpdate()
    {
        AnimeUPDate();

        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Armature|RestraintAttackLoop" && this.PlayerAttackSpider.IsCapFlag) this.PlayerAttackSpider.IsCapFlag = false;
  
        //íÕÇÒÇ≈Ç¢ÇÈÉnÉìÉ^Å[Ç™éÄÇÒÇ≈Ç¢ÇÈÇ©Çîªíf
       // if(TargetHunter.GetComponent<Hunter_AI>()?.GetHunterID()==HPManager.GetHunterLostNumber())
        this.PlayerAttackSpider.IsCapFlag =false ;
    
    }
    override protected void AnimeEnd()
    {
        this.PlayerAttackSpider.IsCapFlag = false;
       CaptorAttackSpider Anime = this.gameObject.GetComponent<CaptorAttackSpider>();

        if ("Armature|Jump" == _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name)
            this.gameObject.AddComponent<PlayerSpiderJump>();

        EndTarget();
        if (TargetHunter != null)
            TargetHunter.StopRestraining();

        Destroy(Anime);
    }
}