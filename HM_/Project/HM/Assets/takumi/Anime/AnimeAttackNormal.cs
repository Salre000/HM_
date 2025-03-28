using Cysharp.Threading.Tasks;
using MapMagic.Locks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimeAttackNormal : AnimeBase
{
    public AnimeAttackNormal(GameObject Object, AudioSource source, Animator animator,System.Action<bool> animeFlagReset) : base(Object, source, animator ,animeFlagReset)
    {
        AddAnimeName("Armature|AttackNorml");


    }

    public override void Start()
    {
        _AnimeFlagReset(false);
        ResetFlag().Forget();

    }
    public void SetHitTest()
    {


    }

    public override void Action()
    {


        AnimeUPDate();

    }
    override protected void AnimeEnd()
    {
        base.AnimeEnd();
        useFlag = false;

    }

}

