using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeMoveAttack : AnimeBase
{



    string NestName= "Armature|AttackMoveLoops";


    private void Awake()
    {

        _AnimeName = "Armature|AttackMove";
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

        Destroy(jumpAnime);



    }




}
