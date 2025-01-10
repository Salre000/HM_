using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�A�^�b�N�G���A�̃I�u�W�F�N�g�v�[��
public class AttackAreaPool : MonoBehaviour
{
    const int MaxNumber =  30;
    List<GameObject> objectList = new List<GameObject>(MaxNumber);
    List<AttackArea> attackAreaList = new List<AttackArea>(MaxNumber);
    [SerializeField] Tag tag;
    [Header("���A���r�A�����A�E�r�A�E��")]
    GameObject []GameObjects=new GameObject[5];

    private void Awake()
    {
        int index = 0;

        for(int i = 0; i < MaxNumber; i++) 
        {
            objectList.Add(new GameObject());

            objectList[i].tag = tag.GetPlayerAttackTag();

            objectList[i].AddComponent<Damage>().SetDamage(0);
            

            attackAreaList.Add(objectList[i].AddComponent<AttackArea>());

            objectList[i].transform.gameObject.SetActive(false);
        }

        GameObjects[index] = GameObject.Find("Bone.024"); index++;
        GameObjects[index] = GameObject.Find("Bone.019"); index++;
        GameObjects[index] = GameObject.Find("Bone.003"); index++;
        GameObjects[index] = GameObject.Find("Bone.022"); index++;
        GameObjects[index] = GameObject.Find("Bone.015"); index++;
    }

    public void SetAttack(int Number) 
    {
        if (Number < 0 || GameObjects.Length <= Number) return;

        for(int i = 0; i < MaxNumber; i++) 
        {

            if (objectList[i].activeSelf == false) 
            {

                attackAreaList[i].SetAttackArea(GameObjects[Number]);

                return;
            }

        }


    }





}
