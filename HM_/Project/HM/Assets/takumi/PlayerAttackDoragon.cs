using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerAttackDoragon : PlayerAttack
{
   
    private GameObject LeftHand;
    [SerializeField] RockPool rockPool;
    RadialBlur radialBlur;

    protected override void BarkJump()
    {
        BackSteppeAnime backSteppeAnime = this.gameObject.GetComponent<BackSteppeAnime>();

        if (backSteppeAnime == null)
        {
            backSteppeAnime = this.gameObject.AddComponent<BackSteppeAnime>();

            backSteppeAnime.TagBox = TagBox;

        }


    }

    protected override void Jump()
    {
        JumpAnime jumpAnime = this.gameObject.GetComponent<JumpAnime>();

        if (jumpAnime == null)
        {
            jumpAnime = this.gameObject.AddComponent<JumpAnime>();

            jumpAnime.TagBox = TagBox;

        }



    }

    protected override void LTAttack()
    {
        AnimeAttackLongRange animeAttackLongRange = this.gameObject.GetComponent<AnimeAttackLongRange>();

        if (animeAttackLongRange == null)
        {

            animeAttackLongRange = this.gameObject.AddComponent<AnimeAttackLongRange>();

            animeAttackLongRange.TagBox = TagBox;

            animeAttackLongRange.StartObject = LeftHand;

            animeAttackLongRange.RockPool = rockPool;
        }


    }

    protected override void LTRTAttack()
    {
        AnimeAttackRoar Roar = GetComponent<AnimeAttackRoar>();

        if (Roar == null)
        {
            Roar = transform.AddComponent<AnimeAttackRoar>();

            Roar.SetRadialBlur(radialBlur);

        }

    }

    protected override void RTAttack()
    {
        AnimeAttackNormal AttackNormalAnime = this.gameObject.GetComponent<AnimeAttackNormal>();

        if (AttackNormalAnime == null)
        {
            AttackNormalAnime = this.gameObject.AddComponent<AnimeAttackNormal>();

            AttackNormalAnime.TagBox = TagBox;
            AttackNormalAnime.SetHitTest();

        }

    }

    private void Awake()
    {
        radialBlur=Camera.main.GetComponent<RadialBlur>();
        LeftHand = GameObject.Find("Bone.019");
    }
    // Start is called before the first frame update



    void ResetObject() 
    {

    }
}
