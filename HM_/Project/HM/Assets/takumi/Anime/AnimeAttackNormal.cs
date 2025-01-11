using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackNormal : AnimeBase
{
     GameObject AttackObject;
    public void SetAttackObject(GameObject objects) {  AttackObject = objects; }


    const float damages = 30;

    private void Awake()
    {

        _AnimeName = "Armature|AttackNorml";
        PlayerAttack playerAttack =GetComponent<PlayerAttack>();




    }
    public void SetHitTest() 
    {


    }

    private void FixedUpdate()
    {


        AnimeUPDate();

    }
    override protected void AnimeEnd()
    {

        AnimeAttackNormal animeAttackNormal = GetComponent<AnimeAttackNormal>();
        Destroy(animeAttackNormal);


    }

    override protected void DestroyHitObject() 
    {
    }

}
