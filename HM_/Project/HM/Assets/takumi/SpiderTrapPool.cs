using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTrapPool : MonoBehaviour
{

    [SerializeField] GameObject GameObjectTarp;
    [SerializeField]GameObject []TarpPool=new GameObject[10];
    [SerializeField] GameObject Player;

    public void Awake()
    {
        Player = this.gameObject;
        for (int i = 0; i < TarpPool.Length; i++) 
        {

            TarpPool[i] = Instantiate(GameObjectTarp);
            TarpPool [i].gameObject.SetActive(false);


        }


    }


    public void SetTarp() 
    {
        for(int i = 0; i < TarpPool.Length; i++) 
        {
            if (TarpPool[i].active == true) continue;

            TarpPool[i].gameObject.SetActive(true);

            TarpPool[i].transform.position=this.gameObject.transform.position;


            return;

        }


    }



}
