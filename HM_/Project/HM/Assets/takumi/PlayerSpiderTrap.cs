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
    
        StartAngle=this.GameObject.transform.eulerAngles.y;

        //�w偂̑��𐶐�����
        TrapObject=SpiderTrapPool.instance.SetTarp();

        if (TrapObject == null) { AnimeEnd(); return; }
        Trap=TrapObject.GetComponent<SpiderTrap>();


        // Unitask��0.5�b���Ƃɒw偂̑��̃T�C�Y���L����
        task= Times();

    }
    bool Flag=true;
    UniTask task;
    void FixedUpdate()
    {
        AnimeUPDate();

    }

    override protected void AnimeEnd()
    {
        base.AnimeEnd();

        Flag = false;

        useFlag = false;

    }

    async UniTask Times() 
    {

        while (Flag) 
        {


        TrapObject.transform.localScale += Vector3.one;



            await UniTask.DelayFrame(Application.targetFrameRate/2);
        }



    }


}
