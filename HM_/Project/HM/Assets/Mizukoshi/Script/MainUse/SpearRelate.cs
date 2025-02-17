using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRelate : WeaponBase
{
    protected override void SetSound()
    {
        soundListManager.PlaySound((int)Main.hunter, (int)HunterSE.SpearSE);
    }
    public override void SetEffect()
    {
        base.SetEffect();
    }
}
