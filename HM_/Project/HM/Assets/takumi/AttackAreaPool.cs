using JetBrains.Annotations;
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
    private void Awake()
    {
        for(int i = 0; i < MaxNumber; i++) 
        {
            objectList.Add(new GameObject());

            objectList[i].tag = tag.GetPlayerAttackTag();


            attackAreaList.Add(objectList[i].AddComponent<AttackArea>());

            objectList[i].transform.gameObject.SetActive(false);
        }

    }  

    public void SetAttack(GameObject gameObject) 
    {
        for(int i = 0; i < MaxNumber; i++) 
        {

            if (objectList[i].activeSelf == false) 
            {

                attackAreaList[i].SetAttackArea(gameObject);

                return;
            }

        }


    }





}
