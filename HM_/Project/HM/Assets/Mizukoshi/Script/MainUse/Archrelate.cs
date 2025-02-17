using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archrelate : WeaponBase
{
    protected override void SetSound()
    {
        soundListManager.PlaySound((int)HunterSE.ArechSE, (int)Main.Hunter);
    }
    public override void SetEffect()
    {
        base.SetEffect();
    }
}
