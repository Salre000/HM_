using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeMoveAttack : AnimeBase
{



    string NestName= "Armature|AttackMoveLoops";


    private void Awake()
    {

        AddAnimeName("Armature|AttackMove");
    }
    private void FixedUpdate()
    {
        AnimeUPDate();

    }
    override protected void AnimeEnd()
    {
        base.AnimeEnd();

        if (_AnimeName.Contains(NestName)) 
        {


            AddAnimeName(NestName);
            return;
        }

        AnimeMoveAttack jumpAnime = this.gameObject.GetComponent<AnimeMoveAttack>();

        Destroy(jumpAnime);



    }




}
