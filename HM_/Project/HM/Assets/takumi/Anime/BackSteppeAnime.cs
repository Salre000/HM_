using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSteppeAnime : AnimeBase
{


    const float MaxTime = 0.2f;

    float time = 0;

    float JumpAngle;

    private void Awake()
    {
        AddAnimeName("Armature|BackSteppe");

        JumpAngle = (this.transform.eulerAngles.y - 180) * Mathf.Deg2Rad;
    }

    //飛びたくない事前フレームのカウンター
    int FrameCount = 0;
    void FixedUpdate()
    {
        time += Time.deltaTime;
        FrameCount++;
        if (FrameCount < 15) return;

        Vector3 Vec = new Vector3(Mathf.Sin(JumpAngle), 0.75f, Mathf.Cos(JumpAngle)) / 20.0f;


        if (FrameCount <= 30)
            this.transform.position += Vec;

        AnimeUPDate();

    }

    override protected void AnimeEnd()
    {

        base.AnimeEnd();


        BackSteppeAnime backSteppeAnime = this.gameObject.GetComponent<BackSteppeAnime>();

        Destroy(backSteppeAnime);



    }
}