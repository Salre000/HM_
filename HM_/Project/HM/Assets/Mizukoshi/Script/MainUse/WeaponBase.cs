using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase
{
    protected SoundListManager soundListManager;

    

    // 武器の音を鳴らす
    protected virtual void SetSound()
    {

    }

    // 武器のエフェクトを出現させる
    public virtual void SetEffect()
    {

    }
}
