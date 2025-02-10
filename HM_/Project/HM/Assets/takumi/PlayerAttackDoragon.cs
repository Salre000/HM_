using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerAttackDoragon :PlayerAttack
{

    private GameObject LeftHand;
    [SerializeField] RockPool rockPool;
    RadialBlur radialBlur;

    protected override int BarkJump()
    {
        BackSteppeAnime backSteppeAnime = this.gameObject.GetComponent<BackSteppeAnime>();

        if (backSteppeAnime == null)
        {
            backSteppeAnime = this.gameObject.AddComponent<BackSteppeAnime>();

            backSteppeAnime.TagBox = TagBox;
            nowAnime=backSteppeAnime;
            return 1;

        }

        return -1;
    }

    protected override int Jump()
    {
        JumpAnime jumpAnime = this.gameObject.GetComponent<JumpAnime>();

        if (jumpAnime == null)
        {
            jumpAnime = this.gameObject.AddComponent<JumpAnime>();

            jumpAnime.TagBox = TagBox;
            return 1;


        }

        return -1;


    }

    protected override int LTAttack()
    {
        AnimeAttackLongRange animeAttackLongRange = this.gameObject.GetComponent<AnimeAttackLongRange>();

        if (animeAttackLongRange == null)
        {

            animeAttackLongRange = this.gameObject.AddComponent<AnimeAttackLongRange>();

            animeAttackLongRange.TagBox = TagBox;

            animeAttackLongRange.StartObject = LeftHand;

            animeAttackLongRange.RockPool = rockPool;
            return 1;

        }

        return -1;

    }

    protected override int LTRTAttack()
    {
        AnimeAttackRoar Roar = GetComponent<AnimeAttackRoar>();

        if (Roar == null)
        {
            Roar = transform.AddComponent<AnimeAttackRoar>();

            Roar.SetRadialBlur(radialBlur);
            return 1;

        }
        return -1;

    }

    protected override int RTAttack()
    {
        AnimeAttackNormal AttackNormalAnime = this.gameObject.GetComponent<AnimeAttackNormal>();

        if (AttackNormalAnime == null)
        {
            AttackNormalAnime = this.gameObject.AddComponent<AnimeAttackNormal>();

            AttackNormalAnime.TagBox = TagBox;
            AttackNormalAnime.SetHitTest();
            return 1;

        }
        return -1;

    }

    private void Awake()
    {
        radialBlur = Camera.main.GetComponent<RadialBlur>();
        LeftHand = GameObject.Find("Bone.019_end");

    }
    // Start is called before the first frame update

    public GameObject GetStartPositionn() { return LeftHand; }


    void ResetObject()
    {

    }
}
