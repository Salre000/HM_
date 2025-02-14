using SceneSound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class JumpAnime : AnimeBase
{
    public JumpAnime(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset) : base(Object, source, animator, animeFlagReset)
    {
        AddAnimeName("Armature|jump");

        //startClip = SoundListManager.instance.GetAudioClip((int)main.monster, (int)Dragon.DragonJumpStart);

    }

    const float Damages = 30.0f;

    const float MaxTime = 0.3f;

    float time = 0;
    float JumpAngle = 0;


    ////あたり判定を生成するオブジェクト
    //GameObject [] HitGameObject=new GameObject[3];

    //SphereCollider[] Sphere = new SphereCollider[3];

    //Damage []damage=new Damage[3];

    public override void Start()
    {
        // 移動量と回転量を求める
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        JumpAngle = Mathf.Atan2(_horizontal, _vertical) + this.GameObject.transform.eulerAngles.y * 3.14f / 180;

        ResetFlag();
    }

    //飛びたくない事前フレームのカウンター
    int FrameCount = 0;

    public override void Action()
    {
        time += Time.deltaTime;
        FrameCount++;
        if (FrameCount < 15) return;

        Vector3 Vec = new Vector3(Mathf.Sin(JumpAngle), 0.75f, Mathf.Cos(JumpAngle)) / 20.0f;


        if (FrameCount <= 30)
            this.GameObject.transform.position += Vec;

        AnimeUPDate();

    }

    override protected void AnimeEnd()
    {
        base.AnimeEnd(); 
        _AnimeFlagReset(false);







        //if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        //    this.audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)main.monster, (int)Dragon.DragonJumpEnd));

        useFlag = false;

    }
}

