using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpiderTrap : AnimeBase
{
    float StartAngle = 0;

    const int MaxSize = 30;

    const float AddSize = 0.01f;

    GameObject TrapObject = null;
    SpiderTrap Trap = null;

    private void Awake()
    {
        _AnimeName = "Armature|CreateTrap";

        StartAngle=this.transform.eulerAngles.y;

        //’wå‚Ì‘ƒ‚ğ¶¬‚·‚é
        TrapObject=SpiderTrapPool.instance.SetTarp();

        if(TrapObject==null)AnimeEnd();
        Trap=TrapObject.GetComponent<SpiderTrap>();


        // Unitask‚Å0.5•b‚²‚Æ‚É’wå‚Ì‘ƒ‚ÌƒTƒCƒY‚ğL‚°‚é
        task= Times();

        int i = 0;

    }
    bool Flag=true;
    UniTask task;
    void FixedUpdate()
    {
        AnimeUPDate();

    }

    override protected void AnimeEnd()
    {
        PlayerSpiderTrap TrapAnime = this.gameObject.GetComponent<PlayerSpiderTrap>();
        Flag = false;
        Destroy(TrapAnime);
    }

    async UniTask Times() 
    {

        while (Flag) 
        {


        TrapObject.transform.localScale += Vector3.one/50;



            await UniTask.DelayFrame(Application.targetFrameRate/2);
        }



    }


}
