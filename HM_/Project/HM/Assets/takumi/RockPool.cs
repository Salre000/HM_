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

    public GameObject GetRockPool(int index) { return RockPools[index];}

    void Start()
    {
        Items = GameObject.FindGameObjectWithTag("ItemBox");

        DragonItem dragonItem=Items.GetComponent<DragonItem>();


        for (int i=0;i<RockPools.Length;i++) 
        {
            activeCount++;

            RockPools[i]= Instantiate(dragonItem.GetObjectRock());

            RockPools[i].SetActive(false);
        
        }



        
    }


}
