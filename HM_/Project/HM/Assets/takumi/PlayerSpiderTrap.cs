using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public override void Start()
    {
        base.Start();

        StartAngle = this.GameObject.transform.eulerAngles.y;

        //’wå‚Ì‘ƒ‚ð¶¬‚·‚é
        TrapObject = SpiderTrapPool.instance.SetTarp();

        ///if (TrapObject == null) { AnimeEnd(); return; }

    }
    public override void Action()
    {
        if (!InputManager.instance.IsOnButton(InputManager.InputKeys.Skill))
            _AnimeFlagReset(false);
        AnimeUPDate();

        Times();

    }

    override protected void AnimeEnd()
    {
        base.AnimeEnd();

        _AnimeFlagReset(false);

        useFlag = false;

        TrapObject = null;
    }


    void Times()
    {

        if (TrapObject == null) return;

        if (TrapObject.transform.localScale.x >= MaxSize) return;

        TrapObject.transform.localScale += Vector3.one;

        TrapObject.GetComponent<SpiderTrap>().ResetTime();


    }


}
