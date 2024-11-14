using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSteppeAnime : AnimeBase
{


    const float MaxTime = 0.2f;

    float time = 0;

    Rigidbody rb;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;

        _AnimeName = "Armature|BackSteppe";
    }

    //飛びたくない事前フレームのカウンター
    int FrameCount = 0;
    void FixedUpdate()
    {
        time += Time.deltaTime;
        FrameCount++;
        if (FrameCount < 3) return;

        Vector3 Vec = new Vector3(-Mathf.Sin(this.transform.eulerAngles.y * 3.14f / 180), 0, -Mathf.Cos(this.transform.eulerAngles.y * 3.14f / 180));

        if (FrameCount < 30) this.gameObject.transform.position += Vec / 5;

        if (time < MaxTime)
        {
            this.gameObject.transform.position += Vector3.up / 12;



        }
        else
        {
            rb.useGravity = true;



        }

        AnimeUPDate();

    }

    override protected void AnimeEnd()
    {


        rb.useGravity = true;

        BackSteppeAnime backSteppeAnime = this.gameObject.GetComponent<BackSteppeAnime>();

        Destroy(backSteppeAnime);



    }
}