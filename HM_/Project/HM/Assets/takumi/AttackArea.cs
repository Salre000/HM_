using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//�U������̃X�v���N�g
public class AttackArea : MonoBehaviour
{

    //�Ǐ]��̃I�u�W�F�N�g
    [SerializeField] private GameObject Parent;

    //�����蔻��������܂ł̎���
    const int MaxTime = 3;

    int CountTime = 0;


    private void Start()
    {

        SphereCollider collider = this.gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = 1;


    }


    private void FixedUpdate()
    {
        if (CountTime > MaxTime) 
        {
          transform.gameObject.SetActive(false);
        }

        transform.position =Parent.transform.position;

        CountTime++;

        

    }


    public void SetAttackArea(GameObject parent)
    {

        Parent = parent;
        CountTime = 0;
        transform.gameObject.SetActive(true);

    }








}
