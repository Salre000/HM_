using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartTestHit : MonoBehaviour
{
    public enum Part
    {
        Head,
        Body,
        WingLeft,
        WingRight,
        FootLeft,
        FootRight,
        None,

    }

    [SerializeField] Tag tagBox;
    [SerializeField]Animator animator;
    [SerializeField]HPManager hpManager;
    //このスプリクトを貼り付けるオブジェクトの属性
    [SerializeField] Part ThisPart = Part.None;

    [SerializeField] float Hp = 100;

    private void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        tagBox = Player.GetComponent<PlayerAttack>().GetTag();
        animator = Player.GetComponent<Animator>();

        hpManager = GameObject.FindWithTag("GameManager").GetComponent<HPManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != tagBox.GetEnemyAttackTag()) return;

        Damage damage = other.GetComponent<Damage>();

        Hp -= damage.GetDamage();

        hpManager.MonsterDamage(damage.GetDamage());

        if (Hp > 0) return;
        Hp = 100;

        animator.SetBool("DownFlag",true);




    }

}
