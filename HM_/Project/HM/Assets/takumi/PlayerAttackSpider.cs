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
        PlayerSpiderJump playerSpiderJump = this.GetComponent<PlayerSpiderJump>();
        if (playerSpiderJump == null)
        {
            playerSpiderJump = this.AddComponent<PlayerSpiderJump>();

            return 1;



        }
        return -1;
    }

    protected override int Jump()
    {
        PlayerSpiderJump playerSpiderJump = this.GetComponent<PlayerSpiderJump>();
        if (playerSpiderJump == null)
        {
            playerSpiderJump = this.AddComponent<PlayerSpiderJump>();


            return 1;


        }
        return -1;
    }

    protected override int LTAttack()
    {
        PlayerSpiderTrap playerSpiderTrap = this.GetComponent<PlayerSpiderTrap>();
        if (playerSpiderTrap == null)
        {

            playerSpiderTrap = this.AddComponent<PlayerSpiderTrap>();
            return 1;

        }
        return -1;
    }

    protected override int LTRTAttack()
    {
        CaptorAttackSpider Anime = this.GetComponent<CaptorAttackSpider>();

        if (Anime == null)
        {
            _status.PlaySound(SoundListManager.instance.GetAudioClip((int)main.Spider,(int)Spider.SpiderAttackHIt));

            Anime = this.AddComponent<CaptorAttackSpider>();

            Anime.SetUp(CaptorPosition,this);
            return 1;


        }
        return -1;


    }

    protected override int RTAttack()
    {
        return 1;

    }

    private void Awake()
    {
    }
}
