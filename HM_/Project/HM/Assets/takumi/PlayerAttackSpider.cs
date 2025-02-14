using SceneSound;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//蜘蛛のモデルの攻撃アニメーション
public class PlayerAttackSpider : PlayerAttack
{
    [SerializeField] GameObject CaptorPosition;

    protected override int BarkJump()
    {

        nowMode = actionMode.jump;
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

    public void Start()
    {
        AudioSource source = GetComponent<AudioSource>();
        Animator animator = GetComponent<Animator>();
        anime[(int)actionMode.skill] = new PlayerSpiderTrap(this.gameObject, source, animator, _anime.SetLoanAttackFlag);
        anime[(int)actionMode.normal] = new AnimeBase(this.gameObject, source, animator, _anime.SetAttackFlag);
        anime[(int)actionMode.special] = new CaptorAttackSpider(this.gameObject, source, animator, _anime.SetRoarFlag, ChengeJumpAnime, CaptorPosition);
        anime[(int)actionMode.jump] = new JumpAnime(this.gameObject, source, animator, _anime.SetJumpFlag);
        anime[(int)actionMode.backJump] = new BackSteppeAnime(this.gameObject, source, animator, _anime.SetBackSteppeFlag);



    }
    void ChengeJumpAnime() 
    {
        nowMode = actionMode.jump;
        anime[(int)nowMode].Start();

        _anime.SetJumpFlag(true);

    }

}
