using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunmerrelate : WeaponBase
{
    protected override void SetSound()
    {
        soundListManager.PlaySound((int)main.hunter, (int)HunterSE.HunmerAttackSE);
    }
    public override void SetEffect()
    {
        base.SetEffect();
    }
}
