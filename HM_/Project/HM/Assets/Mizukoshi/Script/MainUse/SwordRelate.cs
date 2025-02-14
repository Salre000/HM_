using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRelate : WeaponBase
{
    public override void SetEffect()
    {
        base.SetEffect();
    }

    protected override void SetSound()
    {
        //soundListManager.PlaySound((int)main.hunter, (int)HunterSE.SwordAttackSE);
    }
}
