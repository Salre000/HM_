using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneSound 
{
    //タイトル画面の音の番号の列挙体
    enum Title
    {
        System
    }

    //セレクト画面の音の番号の列挙体
    enum Select
    {
        System,


    }

    //インゲームで使う音の列挙体
    enum Main 
    {
        
        Monster,
        System,
        Hunter,
        BGM,
    }


    enum TitleSystem
    {
        Start,
        Anim,
        BGM,
    }

    enum SelectSystem
    {
        Start,
        Change,
    }
    enum Dragon 
    {
        DragonMove,
        DragonMoveVoice,
        DragonVoice,
        DragonAttackHit,
        DragonJumpStart,
        DragonJumpEnd,
        DragonLongAttack,
        DragonBite,
        DragonRoar,
        DragonBigRoar,
        DragonHitVoice,//攻撃を食らった後の声
        DragonNormalDown,
        DragonNormalDownVoice,
        DragonHardDown,//ダウン時の物音
        DragonHardDownVoice,//ダウン時の叫び声
        Max
        //ここまでどらごん

    }
    enum Spider 
    {
        SpiderMove,
        SpiderHitVoice,//攻撃を食らった後の声
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
        SpiderNormalDown,
        SpiderNormalDownVoice,
        SpiderHardDown,//ダウン時の物音
        SpiderHardDownVoice,//ダウン時の叫び声
        //ここまで蜘蛛

    }

    enum MainSystem
    {
        BGM,
        Open,
        Decision,
    }

    enum HunterSE
    {
       WalkSE,
       PreArechSE,
       SpearSE,
       HunmerAttackSE,
       SwordAttackSE,
       ArechSE,
       PreSwordAttack,
       PreSpearAttack,
    }

    enum BGM
    {
        MainGameBGM,
    }

}
