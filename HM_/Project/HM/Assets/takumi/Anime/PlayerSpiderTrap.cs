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

    const int MaxSize = 30;

    GameObject TrapObject = null;

    public override void Start()
    {
        base.Start();

    }
    public override void Action()
    {
        if (!InputManager.instance.IsOnButton(InputManager.InputKeys.Skill))
            _AnimeFlagReset(false);
        AnimeUPDate();


    }
    protected override void AnimeTrue()
    {
        //’wå‚Ì‘ƒ‚ð¶¬‚·‚é
        if (TrapObject == null) TrapObject = SpiderTrapPool.instance.SetTarp();


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

        TrapObject.transform.localScale += Vector3.one/5.0f;

        TrapObject.GetComponent<SpiderTrap>().ResetTime();


    }


}
