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
    SphereCollider collider;
    private Damage damage;

    //�����蔻��������܂ł̎���
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
