using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptorAttackSpider : AnimeBase
{
    private GameObject CaptorPosition;
    private GameObject CaptorTarget;
    private PlayerAttackSpider PlayerAttackSpider;
    private void Awake()
    {
        AddAnimeName("Armature|Jump");

    }

    void FixedUpdate()
    {
        AnimeUPDate();
    }
    override protected void AnimeEnd()
    {
        PlayerSpiderJump jumpAnime = this.gameObject.GetComponent<PlayerSpiderJump>();

        Destroy(jumpAnime);
    }


}