using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisapper : MonoBehaviour
{
    public GameObject spearHuman;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (spearHuman.GetComponent<Hunter_AI>().GetAttackState())
        {
            this.gameObject.tag = "EnemyAttack";
            Debug.Log("Ç‚ÇËï∫émçUåÇíÜ");
        }
        else
        {
            this.gameObject.tag = "Untagged";
        }
    }
}
