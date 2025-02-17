using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunmerrelate : WeaponBase
{
    protected override void SetSound()
    {
        soundListManager.PlaySound((int)HunterSE.HunmerAttackSE, (int)Main.Hunter);
    }
    public override void SetEffect()
    {
        base.SetEffect();
    }
}
