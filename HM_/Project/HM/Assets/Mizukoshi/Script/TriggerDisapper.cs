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
        condition = spearHuman.GetComponent<Hunter_AI>().GetAttackState();
        // 条件が真ならコライダー生成
        if (condition && co == null&&!hit)
        {
            co=gameObject.AddComponent<BoxCollider>();
            co.size = new Vector3(0.2f, 5, 0.2f);
        }
        // 条件が偽なら
        else if (!condition && co != null)
        {
            Destroy(co);
            hit = false;
        }

        if (hit) 
        {
            Destroy(co);
        }

    }
}
