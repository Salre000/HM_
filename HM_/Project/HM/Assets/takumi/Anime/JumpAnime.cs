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

    Rigidbody rb;

    ////あたり判定を生成するオブジェクト
    //GameObject [] HitGameObject=new GameObject[3];

    //SphereCollider[] Sphere = new SphereCollider[3];

    //Damage []damage=new Damage[3];

    private void Awake()
    {
        rb=this.gameObject.GetComponent<Rigidbody>();

        _AnimeName = "Armature|jump";

        //HitGameObject[0] = GameObject.Find("Bone.024");
        //HitGameObject[1] = GameObject.Find("Bone.019");
        //HitGameObject[2] = GameObject.Find("Bone.022");

        PlayerAttack playerAttack = GetComponent<PlayerAttack>();

                // 移動量と回転量を求める
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        JumpAngle=Mathf.Atan2(_horizontal, _vertical)+this.transform.eulerAngles.y*3.14f/180;
        Debug.Log(JumpAngle);

        //for (int i = 0; i < 3; i++) 
        //{
        //    Sphere[i]=HitGameObject[i].AddComponent<SphereCollider>();

        //    //HitGameObject[i].tag;
        //    Sphere[i].radius = 0.01f;
        //    Sphere[i].center=Vector3.zero;
        //    Sphere[i].isTrigger= true;
        //    HitGameObject[i].tag = playerAttack.GetTag().GetPlayerAttackTag();

        //    damage[i] = HitGameObject[i].AddComponent<Damage>();
        //    damage[i].SetDamage(Damages);

        //}


    }

    //飛びたくない事前フレームのカウンター
    int FrameCount = 0;
    void FixedUpdate()
    {
        time += Time.deltaTime;
        FrameCount++;
        if (FrameCount < 3) return;

        Vector3 Vec= new Vector3(Mathf.Sin(JumpAngle), 0, Mathf.Cos(JumpAngle));

        if(FrameCount<30)this.gameObject.transform.position += Vec/5;

        if (time < MaxTime) 
        {
            this.gameObject.transform.position+=Vector3.up/ 12;



        }
        else 
        {



        }

        AnimeUPDate();

    }

    override protected void AnimeEnd()
    {



        JumpAnime jumpAnime=this.gameObject.GetComponent<JumpAnime>();

        Destroy(jumpAnime);

        for(int i = 0; i < 3; i++)
        {
            //HitGameObject[i].tag = TagBox.GetPlayerTag();

            //Destroy(Sphere[i]);
            //Destroy(damage[i]);

        }


    }
}
