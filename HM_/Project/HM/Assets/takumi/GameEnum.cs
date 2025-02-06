using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneSound 
{
    //タイトル画面の音の番号の列挙体
    enum Title 
    {

        system,


    }

    //セレクト画面の音の番号の列挙体
    enum Select 
    {
        system,


    }

    //インゲームで使う音の列挙体
    enum main 
    {
        
        Dragon,
        Spider,
        system








    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////
    /// </summary>
    enum Dragon 
    {
        DragonAttackHit,
        DragonJumpStart,
        DragonJumpEnd,
        DragonMove,
        DragonMoveVoice,
        DragonVoice,
        DragonLongAttack,
        DragonBite,
        DragonRoar,
        DragonBigRoar,
        DragonHitVoice,//攻撃を食らった後の声
        DragonNormalDown,
        DragonNormalDownVoice,
        DragonHardDown,//ダウン時の物音
        DragonHardDownVoice,//ダウン時の叫び声
        //ここまでどらごん

    }
    enum Spider 
    {
        SpiderAttackHIt,
        SpiderJumpStart,
        SpiderJumpSpace,
        SpiderJumpEnd,
        SpiderCaptorStart,
        SpiderCaptorSuccess,//捕獲成功
        SpiderCaptorMiss,
        SpiderTrapCreation,
        SpiderTrapSnagged,//トラップに引っかかった音
        SpiderTrapLoop,//トラップに引っかかっている時の音
        SpiderTrapEnd,//トラップが無くなる音
        SpiderMove,
        SpiderHitVoice,//攻撃を食らった後の声
        SpiderNormalDown,
        SpiderNormalDownVoice,
        SpiderHardDown,//ダウン時の物音
        SpiderHardDownVoice,//ダウン時の叫び声
        //ここまで蜘蛛

    }

    enum System
    {
        BGM,
        Decision,
    }


}
