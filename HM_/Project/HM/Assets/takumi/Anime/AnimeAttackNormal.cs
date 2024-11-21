using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackNormal : AnimeBase
{
     GameObject AttackObject;
    public void SetAttackObject(GameObject objects) {  AttackObject = objects; }
    SphereCollider Sphere;

    Damage damage;

    const float damages = 30;

    private void Awake()
    {

        _AnimeName = "Armature|AttackNorml";
        PlayerAttack playerAttack =GetComponent<PlayerAttack>();

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

    override protected void DestroyHitObject() 
    {
        AnimeEnd();



    }

}
