using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisapper : MonoBehaviour
{
    public GameObject spearHuman;

    private BoxCollider co;

    public bool condition = false;

    public bool hit=false;

    void Update()
    {
        condition = CheckGetHitAnumation();
        // 条件が真ならコライダー生成
        if (condition && co == null && !hit)
        {
            co = gameObject.AddComponent<BoxCollider>();
            co.size = new Vector3(0.2f, 5, 0.2f);
            co.isTrigger = true;
        }
        // 条件が偽なら
        else if (!condition && co != null)
        {
            Destroy(co);
            hit = false;
        }
        else if (co == null && !condition)
        {
            hit = false;
        }

        if (hit) 
        {

            Destroy(co);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Destroy(co);
            hit=true;  
        }
    }

    bool CheckGetHitAnumation()
    {
        return spearHuman.GetComponent<Hunter_AI>().GetAttackState()/* || spearHuman.GetComponent<HunterAI_Ver>().GetAttackState()*/;
    }

    public void DestoryTrigger()
    {
        Destroy(co);
    }
}
