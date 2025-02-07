using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPool : MonoBehaviour
{
    GameObject []RockPools= new GameObject[21];

    GameObject Items;

    public GameObject GetRockPool() 
    {

        for (int i=0;i<RockPools.Length;i++) 
        {
            if(RockPools[i].transform.gameObject.activeSelf == false) 
            {
                return RockPools[i];
            }
        }

        return null;    
    }

    void Start()
    {
        DragonItem dragonItem=GetComponent<DragonItem>();

        for (int i=0;i<RockPools.Length;i++) 
        {

            RockPools[i]= Instantiate(dragonItem.GetObjectRock(),dragonItem.transform);



            RockPools[i].transform.parent = this.transform;

            RockPools[i].SetActive(false);


        
        }


    }
}
