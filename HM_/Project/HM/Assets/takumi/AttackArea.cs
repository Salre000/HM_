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
    SphereCollider collider;
    private Damage damage;

    //当たり判定を消すまでの時間
    const int MaxTime = 3;

    int CountTime = 0;

    public void Awake()
    {
        collider = this.gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = 0.1f;
        damage=GetComponent<Damage>();

    }


    private void FixedUpdate()
    {
        if (CountTime >= MaxTime) 
        {
          transform.gameObject.SetActive(false);
          CountTime = 0;
        }

        transform.position =Parent.transform.position;

        CountTime++;

        

    }


    public void SetAttackArea(GameObject parent, float Damage, float radius = 0.1f, int CountTime =0)
    {

        this.CountTime = CountTime;
        Parent = parent;
        CountTime = 0;
        collider.radius=radius;
        transform.gameObject.SetActive(true);
        damage.SetDamage(Damage);

    }








}
