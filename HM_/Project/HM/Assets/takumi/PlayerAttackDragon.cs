using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerAttackDragon :PlayerAttack
{

    private GameObject LeftHand;
    [SerializeField] RockPool rockPool;
    RadialBlur radialBlur;

    protected override int BarkJump()
    {

        nowMode = actionMode.backJump;
        AnimeBase.useFlag = true;
        anime[(int)nowMode].Start();
        return 1;
    }

    protected override int Jump()
    {
        nowMode = actionMode.jump;
        AnimeBase.useFlag = true;
        anime[(int)nowMode].Start();

        return 1;


    }

    protected override int LTAttack()
    {
        nowMode = actionMode.skill;
        AnimeBase.useFlag = true;
        anime[(int)nowMode].Start();

        return 1;

    }

    protected override int LTRTAttack()
    {
        nowMode = actionMode.special;
        AnimeBase.useFlag = true;
        anime[(int)nowMode].Start();

        return 1;

    }

    protected override int RTAttack()
    {

        nowMode = actionMode.normal;
        AnimeBase.useFlag = true;
        anime[(int)nowMode].Start();


        return 1;

    }

    private void Start()
    {
        radialBlur = Camera.main.GetComponent<RadialBlur>();
        LeftHand = GameObject.Find("Bone.019_end");

        AudioSource source=GetComponent<AudioSource>();
        Animator animator=GetComponent<Animator>();
        anime[(int)actionMode.skill] = new AnimeAttackLongRange(this.gameObject, source, animator,_anime.SetLoanAttackFlag);
        anime[(int)actionMode.normal]=new AnimeAttackNormal(this.gameObject, source, animator, _anime.SetAttackFlag);
        anime[(int)actionMode.special]=new AnimeAttackRoar(this.gameObject, source, animator, _anime.SetRoarFlag);
        anime[(int)actionMode.jump]=new JumpAnime(this.gameObject, source, animator,_anime.SetJumpFlag);
        anime[(int)actionMode.backJump]=new BackSteppeAnime(this.gameObject, source, animator,_anime.SetBackSteppeFlag);
        
        AnimeAttackRoar animeAttackRoar =(AnimeAttackRoar)anime[(int)actionMode.special];
        animeAttackRoar.SetRadialBlur(radialBlur);

    }
    // Start is called before the first frame update

    public GameObject GetStartPositionn() { return LeftHand; }


    void ResetObject()
    {

    }

}
