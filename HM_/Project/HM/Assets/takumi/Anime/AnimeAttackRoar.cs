using Cysharp.Threading.Tasks;
using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackRoar : AnimeBase
{

    public AnimeAttackRoar(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset) : base(Object, source, animator, animeFlagReset)
    {
        AddAnimeName("Armature|AttackRoar");


    }
    public override void Start()
    {
        _AnimeFlagReset(false);

    }


    RadialBlur radialBlur;
    public void SetRadialBlur(RadialBlur radial) { radialBlur = radial; }
    public override void Action()
    {
        AnimeUPDate();
    }
    public override void AnimeEvent()
    {
        radialBlur.enabled = true;

        AnimeRoarEnd().Forget();
        _AnimeFlagReset(false);

    }

    private async UniTask AnimeRoarEnd() 
    {
        await UniTask.DelayFrame(90);

        radialBlur.enabled = false;


    }




    //アニメーションコントローラー
    protected override void AnimeEnd()
    {
        base.AnimeEnd();
        useFlag = false;
    }


}
