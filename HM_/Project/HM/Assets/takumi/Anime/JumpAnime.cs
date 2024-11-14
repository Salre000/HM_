using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class JumpAnime : AnimeBase
{


    const float MaxTime = 0.2f;

    float time = 0;

    Rigidbody rb;

    //あたり判定を生成するオブジェクト
    GameObject [] HitGameObject=new GameObject[3];

    SphereCollider[] Sphere = new SphereCollider[3];


    private void Awake()
    {
        rb=this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;

        _AnimeName = "Armature|AttackJumping";

        HitGameObject[0] = GameObject.Find("Bone.024");
        HitGameObject[1] = GameObject.Find("Bone.019");
        HitGameObject[2] = GameObject.Find("Bone.022");

        for(int i = 0; i < 3; i++) 
        {
            Sphere[i]=HitGameObject[i].AddComponent<SphereCollider>();

            //HitGameObject[i].tag;
            Sphere[i].radius = 0.01f;
            Sphere[i].center=Vector3.zero;


        }


    }

    //飛びたくない事前フレームのカウンター
    int FrameCount = 0;
    void FixedUpdate()
    {
        time += Time.deltaTime;
        FrameCount++;
        if (FrameCount < 3) return;

        Vector3 Vec= new Vector3(Mathf.Sin(this.transform.eulerAngles.y*3.14f/180), 0, Mathf.Cos(this.transform.eulerAngles.y * 3.14f / 180));

        if(FrameCount<30)this.gameObject.transform.position += Vec/5;

        if (time < MaxTime) 
        {
            this.gameObject.transform.position+=Vector3.up/ 12;



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

        JumpAnime jumpAnime=this.gameObject.GetComponent<JumpAnime>();

        Destroy(jumpAnime);



    }
}
