using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPool : MonoBehaviour
{
    GameObject []RockPools= new GameObject[21];

    int activeCount = 0;

    public int GetActiveCount() { return activeCount; }

    public void SbuActiveCount() {  activeCount--; }    
    public void AddActiveCount() {  activeCount++; }    

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
        Items = GameObject.FindGameObjectWithTag("ItemBox");

        DragonItem dragonItem=Items.GetComponent<DragonItem>();


        for (int i=0;i<RockPools.Length;i++) 
        {

            RockPools[i]= Instantiate(dragonItem.GetObjectRock());

            RockPools[i].SetActive(false);
        
        }



        
    }


}
