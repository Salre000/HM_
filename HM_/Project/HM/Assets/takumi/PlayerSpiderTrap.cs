using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpiderTrap : AnimeBase
{

    public PlayerSpiderTrap(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset) : base(Object, source, animator, animeFlagReset)
    {
        AddAnimeName("Armature|CreateTrap");




    }

    float StartAngle = 0;

    const int MaxSize = 30;

    const float AddSize = 0.01f;

    GameObject TrapObject = null;
    SpiderTrap Trap = null;

    public override void Start()
    {
        base.Start();

        StartAngle = this.GameObject.transform.eulerAngles.y;

        //íwÂÅÇÃëÉÇê∂ê¨Ç∑ÇÈ
        TrapObject = SpiderTrapPool.instance.SetTarp();

        if (TrapObject == null) { AnimeEnd(); return; }
        Trap = TrapObject.GetComponent<SpiderTrap>();


        // UnitaskÇ≈0.5ïbÇ≤Ç∆Ç…íwÂÅÇÃëÉÇÃÉTÉCÉYÇçLÇ∞ÇÈ
        task = Times();

    }
    bool Flag = true;
    UniTask task;
    public override void Action()
    {
        AnimeUPDate();
        if (!InputManager.instance.IsOnButton(InputManager.InputKeys.LT)) _AnimeFlagReset(false);

    }

    override protected void AnimeEnd()
    {
        base.AnimeEnd();

        _AnimeFlagReset(false);

        Flag = false;

        useFlag = false;

    }


    async UniTask Times()
    {

        while (Flag)
        {


            TrapObject.transform.localScale += Vector3.one;

            TrapObject.GetComponent<SpiderTrap>().ResetTime();

            await UniTask.DelayFrame(Application.targetFrameRate / 2);

            if (TrapObject.transform.localScale.x >= MaxSize) break;
        }



    }


}
