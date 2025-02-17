using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archrelate : WeaponBase
{
    protected override void SetSound()
    {
        soundListManager.PlaySound((int)Main.hunter, (int)HunterSE.ArechSE);
    }
    public override void SetEffect()
    {
        base.SetEffect();
    }
}
