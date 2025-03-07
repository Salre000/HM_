using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTrapPool : MonoBehaviour
{

    [SerializeField] GameObject GameObjectTarp;
    GameObject []TarpPool=new GameObject[2];
    [SerializeField] GameObject Player;

    public static SpiderTrapPool instance;

    public void Awake()
    {
        if (instance != null) 
        {
            Destroy(instance.gameObject);
            return;
        }

        instance=this;

        for (int i = 0; i < TarpPool.Length; i++) 
        {

            TarpPool[i] = Instantiate(GameObjectTarp,this.transform);
            TarpPool [i].gameObject.SetActive(false);


        }


    }


    public GameObject SetTarp() 
    {
        for(int i = 0; i < TarpPool.Length; i++) 
        {
            if (TarpPool[i].active == true) continue;

            TarpPool[i].gameObject.SetActive(true);

            TarpPool[i].transform.position=Player.transform.position;


            return TarpPool[i];

        }

        return null;
    }

    public GameObject GetTrap(int i = 0)
    {
       return TarpPool[i];

    }
    //現在出現している蜘蛛の巣を返す関数
    public List<GameObject> GetTraps() 
    {
        List<GameObject> list = new List<GameObject>();

        for(int i = 0; i < 2; i++) 
        {
            if (GetTrap(i).gameObject.activeSelf != true) continue;

            list.Add(GetTrap(i));

        }

        return list;

    } 



}
