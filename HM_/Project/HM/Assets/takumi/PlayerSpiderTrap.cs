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
        AddAnimeName("Armature|CreateTrap");

        StartAngle=this.transform.eulerAngles.y;

        //íwÂÅÇÃëÉÇê∂ê¨Ç∑ÇÈ
        TrapObject=SpiderTrapPool.instance.SetTarp();

        if(TrapObject==null)AnimeEnd();
        Trap=TrapObject.GetComponent<SpiderTrap>();


        // UnitaskÇ≈0.5ïbÇ≤Ç∆Ç…íwÂÅÇÃëÉÇÃÉTÉCÉYÇçLÇ∞ÇÈ
        task= Times();

        HitEffectManager.instance.HitEffectShow(TrapObject.transform.position, HitEffectManager.CharacterType.Monster);


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


        TrapObject.transform.localScale += Vector3.one;



            await UniTask.DelayFrame(Application.targetFrameRate/2);
        }



    }


}
