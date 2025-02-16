using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSteppeAnime : AnimeBase
{
    public BackSteppeAnime(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset) : base(Object, source, animator, animeFlagReset)
    {
        AddAnimeName("Armature|BackSteppe");



    }



    const float MaxTime = 0.2f;

    float time = 0;

    float JumpAngle;

    public override void Start()
    {

        JumpAngle = (this.GameObject.transform.eulerAngles.y - 180) * Mathf.Deg2Rad;
        Debug.Log(JumpAngle);
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

        useFlag = false;

    }
}
