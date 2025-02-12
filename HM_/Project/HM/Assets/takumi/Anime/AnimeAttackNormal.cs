using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackNormal : AnimeBase
{


    const float damages = 30;

    private void Awake()
    {
        AddAnimeName("Armature|AttackNorml");
        PlayerAttackDragon playerAttack =GetComponent<PlayerAttackDragon>();




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
        base.AnimeEnd();

        AnimeAttackNormal animeAttackNormal = GetComponent<AnimeAttackNormal>();
        Destroy(animeAttackNormal);


    }

}
