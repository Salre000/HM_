using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChainPool : MonoBehaviour
{
    [SerializeField] GameObject Chain;
    GameObject[] ChainPools=new GameObject[8];

    ChainObject[] ChainObjects=new ChainObject[8];

    static ChainPool instance=null;
    void Start()
    {
        if(instance != null)Destroy(this.gameObject);
        instance = this;
        for (int i = 0; i < 8; i++)
        {
            ChainPools[i] = Instantiate(Chain, transform);

            ChainObjects[i]=ChainPools[i].GetComponent<ChainObject>(); 
        }

    }



    public  GameObject GetChainPool(Vector3 Start,Vector3 End) 
    {
        for(int i = 0; i < 8; i++) 
        {
            if (ChainPools[i].activeSelf != false) continue;


            ChainPools[i].SetActive(true);

            ChainObjects[i].SetUp(Start, End);


            return ChainPools[i];

        }


        return null;

    }



}
