using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//攻撃判定のスプリクト
public class AttackArea : MonoBehaviour
{

    //追従先のオブジェクト
    [SerializeField] private GameObject Parent;

    //当たり判定を消すまでの時間
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
