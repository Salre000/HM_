using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//蜘蛛のモデルの攻撃アニメーション
public class PlayerAttackSpider : PlayerAttack
{
    [SerializeField] GameObject CaptorPosition;
    protected override void BarkJump()
    {
        PlayerSpiderJump playerSpiderJump = this.GetComponent<PlayerSpiderJump>();
        if (playerSpiderJump == null)
        {
            playerSpiderJump = this.AddComponent<PlayerSpiderJump>();




        }

    }

    protected override void Jump()
    {
        PlayerSpiderJump playerSpiderJump = this.GetComponent<PlayerSpiderJump>();
        if (playerSpiderJump == null)
        {
            playerSpiderJump = this.AddComponent<PlayerSpiderJump>();




        }
    }

    protected override void LTAttack()
    {
        PlayerSpiderTrap playerSpiderTrap = this.GetComponent<PlayerSpiderTrap>();
        if (playerSpiderTrap == null)
        {
            playerSpiderTrap = this.AddComponent<PlayerSpiderTrap>();




        }

    }

    protected override void LTRTAttack()
    {
        CaptorAttackSpider Anime = this.GetComponent<CaptorAttackSpider>();
        if (Anime == null)
        {
            Anime = this.AddComponent<CaptorAttackSpider>();

            Anime.SetUp(CaptorPosition,this);


        }

    }

    protected override void RTAttack()
    {
    }

    private void Awake()
    {
    }
}
