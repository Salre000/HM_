using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackNormal : AnimeBase
{
    GameObject AttackObject;

    SphereCollider Sphere;

    Damage damage;

    const float damages = 30;

    private void Awake()
    {

        _AnimeName = "Armature|AttackNorml";
        //頭のゲームオブジェクト
        AttackObject = GameObject.Find("Bone.024");

        PlayerAttack playerAttack =GetComponent<PlayerAttack>();

        AttackObject.tag = playerAttack.GetTag().GetPlayerAttackTag();
        Sphere=AttackObject.AddComponent<SphereCollider>();
        Sphere.center = Vector3.zero;
        Sphere.radius = 0.05f;
        Sphere.isTrigger = true;

        damage = AttackObject.AddComponent<Damage>();
        damage.SetDamage(damages);


    }

    private void FixedUpdate()
    {
        AnimeUPDate();
    }
    override protected void AnimeEnd()
    {
        AnimeAttackNormal animeAttackNormal = GetComponent<AnimeAttackNormal>();
        AttackObject.tag=TagBox.GetPlayerTag();
        Destroy(Sphere);
        Destroy(damage);
        Destroy(animeAttackNormal);


    }
}
