using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackNormal : AnimeBase
{


    const float damages = 30;

    private void Awake()
    {

        _AnimeName = "Armature|AttackNorml";
        PlayerAttackDoragon playerAttack =GetComponent<PlayerAttackDoragon>();




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
