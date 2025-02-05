using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAILogic : Hunter_AI
{
    public float attackDistance = 5.0f;

    public float _coolTime = 2.5f;

    public float elaspedCooltime = 0;

    
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckKeepDistance(attackDistance, this.gameObject))
        {
            Chase();
            attackReady=false;
        }
        else
        {
            attackReady = true;
        }
        if (attackReady)
        {
            elaspedCooltime += Time.deltaTime;
        }
        if (elaspedCooltime >= _coolTime)
        {
            Attack();
            attackReady = false;
            elaspedCooltime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartRestraining();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StopRestraining();
        }



    }
}
