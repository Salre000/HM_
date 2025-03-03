using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アタックエリアのオブジェクトプール
public class AttackAreaPool : MonoBehaviour
{
    const int MaxNumber =  30;
    List<GameObject> objectList = new List<GameObject>(MaxNumber);
    List<AttackArea> attackAreaList = new List<AttackArea>(MaxNumber);
    [SerializeField] Tag tag;
    [Header("頭、左腕、左足、右腕、右足")]
    GameObject []GameObjects=new GameObject[5];
    [SerializeField]GameObject Player;

    private void Awake()
    {

        int index = 0;
        Player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < MaxNumber; i++) 
        {
            objectList.Add(new GameObject());
            objectList[i].transform.SetParent(this.transform);

            objectList[i].name = "attackArea";

            objectList[i].tag = tag.GetPlayerAttackTag();

            objectList[i].AddComponent<Damage>().SetDamage(0);
            

            attackAreaList.Add(objectList[i].AddComponent<AttackArea>());

            objectList[i].transform.gameObject.SetActive(false);
        }

        GameObjects[index] = GameObject.Find("head"); index++;
        GameObjects[index] = GameObject.Find("LeftArm"); index++;
        GameObjects[index] = GameObject.Find("LeftFoot"); index++;
        GameObjects[index] = GameObject.Find("LightArm"); index++;
        GameObjects[index] = GameObject.Find("LightFoot"); index++;
    }


    public void SetAttack(AnimationEvent Event) 
    {
        if (Event.intParameter < 0 || GameObjects.Length <= Event.intParameter) return;

        for(int i = 0; i < MaxNumber; i++) 
        {

            if (objectList[i].activeSelf == false) 
            {

                attackAreaList[i].SetAttackArea(GameObjects[Event.intParameter], Event.floatParameter);

                return;
            }

        }


    }

    public void SetAttackBig(int Damage)
    {


        for (int i = 0; i < MaxNumber; i++)
        {

            if (objectList[i].activeSelf == false)
            {

                attackAreaList[i].SetAttackArea(Player, Damage, 0.3f,-60);

                return;
            }

        }



    }





}
