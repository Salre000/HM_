using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackLongRange : AnimeBase
{
    const float damages = 30;

    public GameObject StartObject;

    //岩のゲームオブジェクト
    GameObject Rocks;

    private void Awake()
    {

        _AnimeName = "Armature|AttackLongRange";

        PlayerAttack playerAttack = GetComponent<PlayerAttack>();


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
        DragonItem dragonItem = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<DragonItem>();

        float Angle=this.transform.eulerAngles.y*3.14f/180;

        for (int i=-1;i<2;i++) 
        {
            GameObject Rock=Instantiate(dragonItem.GetObjectRock());

            Rock.tag = TagBox.GetPlayerAttackTag();

            Rock.transform.position = StartObject.transform.position;

            RockAttack rockAttack = Rock.GetComponent<RockAttack>();

            rockAttack.SetMoveVec(new Vector3( Mathf.Sin(Angle+((i*15)*3.14f/180)),0,Mathf.Cos(Angle + ((i * 15) * 3.14f / 180))));



        }



    }
}
