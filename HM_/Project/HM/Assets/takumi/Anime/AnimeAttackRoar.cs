using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackRoar :AnimeBase
{




    private void Awake()
    {
        _AnimeName = "Armature|AttackRoar";
    }
    private void FixedUpdate()
    {
        AnimeUPDate();
    }


    //�A�j���[�V�����R���g���[���[
    protected override void AnimeEnd()
    {
        




    }


}
