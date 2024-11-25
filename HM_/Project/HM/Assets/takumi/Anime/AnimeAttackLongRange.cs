using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackLongRange : AnimeBase
{
    const float damages = 30;

    public GameObject StartObject;

    //岩のゲームオブジェクト
    GameObject Rocks;

    RockPool RockPool;

    private void Awake()
    {

        _AnimeName = "Armature|AttackLongRange";

        PlayerAttack playerAttack = GetComponent<PlayerAttack>();

        RockPool=GameObject.FindGameObjectWithTag("ItemBox").GetComponent<RockPool>();

    }
    private bool Flag = false;
    public float time = 0;
    private void FixedUpdate()
    {

        time += Time.deltaTime;

        if (time > 2.0f&&!Flag) 
        {
            SetRocks();
        }
        AnimeUPDate();
    }
    override protected void AnimeEnd()
    {
        AnimeAttackLongRange animeAttackLongRange = GetComponent<AnimeAttackLongRange>();


        Destroy(animeAttackLongRange );


    }

    void SetRocks() 
    {
        Flag = true;

        float Angle=this.transform.eulerAngles.y*3.14f/180;

        for (int i=-1;i<2;i++) 
        {

            GameObject Rock=RockPool.GetRockPool();

            if (Rock == null) return;

            Rock.SetActive(true);


            Rock.transform.position = StartObject.transform.position;



            RockAttack rockAttack = Rock.GetComponent<RockAttack>();

            rockAttack.SetMoveVec(new Vector3( Mathf.Sin(Angle+((i*15)*3.14f/180)),0,Mathf.Cos(Angle + ((i * 15) * 3.14f / 180))));


            Damage damage=Rock.GetComponent<Damage>();

            damage.SetDamage(30);

            RockPool.SbuActiveCount();


        }



    }
}
