using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeMoveAttack : AnimeBase
{
    public AnimeMoveAttack(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset) : base(Object, source, animator, animeFlagReset)
    {
        AddAnimeName("Armature|AttackMove");


    }



    string NestName = "Armature|AttackMoveLoops";
    public override void Action()
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


        useFlag = false;

    }




}

