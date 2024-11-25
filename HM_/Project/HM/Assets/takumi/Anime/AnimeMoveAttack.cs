using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeMoveAttack : AnimeBase
{
    //あたり判定を生成するオブジェクト
    GameObject[] HitGameObject = new GameObject[3];

    SphereCollider[] Sphere = new SphereCollider[3];

    Damage[] damage = new Damage[3];

    string NestName= "Armature|AttackMoveLoops";


    private void Awake()
    {

        _AnimeName = "Armature|AttackMove";

        HitGameObject[0] = GameObject.Find("Bone.024");
        HitGameObject[1] = GameObject.Find("Bone.019");
        HitGameObject[2] = GameObject.Find("Bone.022");



        for (int i = 0; i < 3; i++)
        {
            Sphere[i] = HitGameObject[i].AddComponent<SphereCollider>();

            //HitGameObject[i].tag;
            Sphere[i].radius = 0.01f;
            Sphere[i].center = Vector3.zero;
            Sphere[i].isTrigger = true;
            HitGameObject[i].tag =TagBox.GetPlayerAttackTag();

            damage[i] = HitGameObject[i].AddComponent<Damage>();
            damage[i].SetDamage(1);

        }

    }
    private void FixedUpdate()
    {
        AnimeUPDate();

    }
    override protected void AnimeEnd()
    {

        if (_AnimeName == NestName) 
        {


            _AnimeName= NestName;
            return;
        }

        AnimeMoveAttack jumpAnime = this.gameObject.GetComponent<AnimeMoveAttack>();


        for (int i = 0; i < 3; i++)
        {
            HitGameObject[i].tag = TagBox.GetPlayerTag();

            Destroy(Sphere[i]);
            Destroy(damage[i]);

        }
        Destroy(jumpAnime);



    }




}
