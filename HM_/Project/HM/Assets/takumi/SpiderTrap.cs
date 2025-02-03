using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTrap : MonoBehaviour
{
    GameObject Hunter;

    Hunter_AI []Hunter_AI=new Hunter_AI[2];

    int NUmber = 0;
    

    int capacity = 0;
    public void OnTriggerEnter(Collider other)
    {
        if (capacity <= 0) return;

        if (other.transform.tag != "Hunter") return;

        if (Hunter == other.gameObject) return;

        Hunter = other.gameObject;

        Hunter_AI[NUmber]=Hunter.GetComponent<Hunter_AI>();

        //拘束の関数を呼ぶ
        Hunter_AI[NUmber].StartRestraining();

        capacity--;
        time = 0;


    }

    private void Awake()
    {
        capacity = 1;
    }

    bool Flag = false;
    bool capacityCount = false;
    public void SetStart() { Flag = true; }

    float time = 0;

    private void FixedUpdate()
    {
        if (this.gameObject.transform.localScale.x >= 15 && capacityCount == false) { capacity++; capacityCount = true; }

        if (!Flag) return;
        time += Time.deltaTime;

        if (time < 4) return;

        Flag = false;
        capacityCount = false;
        capacity = 1;


        //ハンターの拘束攻撃を終了する処理をかく

        Hunter = null;
        time = 0;
        this.gameObject.transform.localScale=Vector3.one;

        //ナビメッシュのベイクのし直しをするスプリクトをデリート//まだ



        for (int i = 0; i < NUmber; i++)
        {
            Hunter_AI[i].StopRestraining();

        }



        NUmber = 0;



    }

}
