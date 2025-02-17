using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackLongRange : AnimeBase
{
    const float damages = 30;

    public GameObject StartObject;

    //岩のゲームオブジェクト
    GameObject Rocks;

    public RockPool RockPool;

    public AnimeAttackLongRange(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset) : base(Object,source,animator,animeFlagReset)
    {
        AddAnimeName("Armature|AttackLongRange");

        Rocks = GameObject.FindGameObjectWithTag("ItemBox");

        StartObject =Object.GetComponent<PlayerAttackDragon>().GetStartPositionn();

        RockPool=Rocks.GetComponent<RockPool>();
    }
    public override void Start()
    {
        _AnimeFlagReset(false);
    }


    private bool Flag = false;
    public float time = 0;
    public override void Action()
    {
        AnimeUPDate();
    }
    override protected void AnimeEnd()
    {
        base.AnimeEnd();
    
        useFlag = false;
    }

    public override void AnimeEvent()
    {
        SetRocks();
    }
    void SetRocks() 
    {
        Flag = true;

        float Angle=this.GameObject.transform.eulerAngles.y*3.14f/180;
        //audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.Monster, (int)Dragon.DragonLongAttack));

        for (int i=-1;i<2;i++) 
        {

            GameObject Rock =RockPool.GetRockPool();

            if (Rock == null) return;

            


            Rock.SetActive(true);


            Rock.transform.position = StartObject.transform.position;



            RockAttack rockAttack = Rock.GetComponent<RockAttack>();

            rockAttack.ActiveChenge();

            rockAttack.SetMoveVec(new Vector3( Mathf.Sin(Angle+((i*15)*3.14f/180)),0,Mathf.Cos(Angle + ((i * 15) * 3.14f / 180))));


            Damage damage=Rock.GetComponent<Damage>();

            damage.SetDamage(30);



        }



    }
}
