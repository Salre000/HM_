using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChainPool : MonoBehaviour
{
    [SerializeField] GameObject Chain;
    GameObject[] ChainPools=new GameObject[8];

    ChainObject[] ChainObjects=new ChainObject[8];

    static public ChainPool instance=null;
    void Start()
    {
        if(instance != null)Destroy(this.gameObject);
        instance = this;
        for (int i = 0; i < 8; i++)
        {
            ChainPools[i] = Instantiate(Chain, transform);

            ChainObjects[i]=ChainPools[i].GetComponent<ChainObject>();

            ChainPools[i].SetActive(false);
        }

    }



    public  GameObject GetChainPool(Vector3 Start,Vector3 End) 
    {
        for(int i = 0; i < 8; i++) 
        {
            if (ChainPools[i].activeSelf != false) continue;


            ChainPools[i].SetActive(true);

            ChainPools[i].transform.position = Start-End+Start;

            ChainObjects[i].SetUp(Start, End);

            //EndChain(ChainPools[i]);
            return ChainPools[i];

        }


        return null;

    }

    async UniTask EndChain(GameObject gameObject) 
    {
        await UniTask.DelayFrame(150);

        float x=gameObject.transform.position.x;
        float z=gameObject.transform.position.z;
        while (gameObject.transform.position.y>0)
        {
            gameObject.transform.position = new Vector3(x, gameObject.transform.position.y - 0.1f, z);

            
        }

        gameObject.SetActive(false);


    }



}
