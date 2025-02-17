using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRelate : WeaponBase
{
    protected override void SetSound()
    {
        soundListManager.PlaySound((int)HunterSE.SpearSE, (int)Main.Hunter);
    }
    public override void SetEffect()
    {
        base.SetEffect();
    }
}
