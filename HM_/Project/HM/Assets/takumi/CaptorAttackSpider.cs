using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptorAttackSpider : AnimeBase
{
    private GameObject CaptorPosition;
    private GameObject CaptorTarget;
    private PlayerAttackSpider PlayerAttackSpider;
    private CaptorHunter CaptorHunter;
    public void SetUp(GameObject SetCaptor,PlayerAttackSpider playerAttack) 
    {

        PlayerAttackSpider = playerAttack;
        CaptorPosition = SetCaptor;
        CaptorHunter = CaptorPosition.GetComponent<CaptorHunter>();
        CaptorHunter.SetGameObject(SetTarget);



    }
    private void Awake()
    {
        AddAnimeName("Armature|RestraintAttackStart");
        AddAnimeName("Armature|RestraintAttackSuccess");
        AddAnimeName("Armature|RestraintAttackLoop");

    }

    public void SetCaptorObject(GameObject gameObject) { CaptorTarget = gameObject; }
    
    public void StartCaptor() {CaptorHunter.SetActiveFlag(true); }

    public void EndTarget() 
    {
        CaptorTarget.transform.parent = null;

    }
    public void CheckHitHunter() 
    {
        if (CaptorTarget != null) return;

        PlayerAttackSpider.SetULTFLag(false);


    }

    private void SetTarget(GameObject gameObject) 
    {

        CaptorTarget = gameObject;
        CaptorTarget.transform.parent=CaptorPosition.transform;
    }

    void FixedUpdate()
    {
        AnimeUPDate();
    }
    override protected void AnimeEnd()
    {
        CaptorAttackSpider Anime = this.gameObject.GetComponent<CaptorAttackSpider>();

        Destroy(Anime);
    }


}