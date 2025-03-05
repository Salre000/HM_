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
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");


    }
    public override void Start()
    {
        _AnimeFlagReset(false);

    }


    GameObject Roar;
    RadialBlur radialBlur;
    Shader shader;
    GameObject mainCamera;
    void SetShader(Shader shader) { this.shader = shader; }
    public void SetRadialBlur(RadialBlur radial) { radialBlur = radial; }
    int FrameCount = 0;
    public override void Action()
    {
        AnimeUPDate();
    }
    public override void AnimeEvent()
    {
        radialBlur.enabled = true;
        //audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.Monster, (int)Dragon.DragonRoar));

        AnimeRoarEnd();
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
