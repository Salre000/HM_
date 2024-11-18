using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackLongRange : AnimeBase
{
    const float damages = 30;

    //岩のゲームオブジェクト
    GameObject Rocks;

    private void Awake()
    {

        _AnimeName = "Armature|AttackLongRange";

        PlayerAttack playerAttack = GetComponent<PlayerAttack>();


    }

    private void FixedUpdate()
    {
        AnimeUPDate();
    }
    override protected void AnimeEnd()
    {
        AnimeAttackLongRange animeAttackLongRange = GetComponent<AnimeAttackLongRange>();


        Destroy(animeAttackLongRange );


    }
}
