using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class JumpAnime : AnimeBase
{

    const float Damages=30.0f;

    const float MaxTime = 0.2f;

    float time = 0;
    float JumpAngle = 0;


    ////あたり判定を生成するオブジェクト
    //GameObject [] HitGameObject=new GameObject[3];

    //SphereCollider[] Sphere = new SphereCollider[3];

    //Damage []damage=new Damage[3];

    private void Awake()
    {

        AddAnimeName("Armature|jump");
        PlayerAttackDoragon playerAttack = GetComponent<PlayerAttackDoragon>();

                // 移動量と回転量を求める
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        JumpAngle=Mathf.Atan2(_horizontal, _vertical)+this.transform.eulerAngles.y*3.14f/180;

    }

    //飛びたくない事前フレームのカウンター
    int FrameCount = 0;
    void FixedUpdate()
    {
        time += Time.deltaTime;
        FrameCount++;
        if (FrameCount < 3) return;

        Vector3 Vec= new Vector3(Mathf.Sin(JumpAngle), 0, Mathf.Cos(JumpAngle));

        if(FrameCount<30)this.gameObject.transform.position += Vec/50;

        if (time < MaxTime) 
        {
            this.gameObject.transform.position+=Vector3.up/ 10;



        }
        else 
        {



        }

        AnimeUPDate();

    }

    override protected void AnimeEnd()
    {
        base.AnimeEnd();



        JumpAnime jumpAnime =this.gameObject.GetComponent<JumpAnime>();

        Destroy(jumpAnime);

    }
}
