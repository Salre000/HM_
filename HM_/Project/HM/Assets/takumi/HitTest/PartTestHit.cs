using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [Header("攻撃を受ける時に使うダメージの倍率")]
    [SerializeField] float DamageRatio = 1.0f;

    private void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        tagBox = Player.GetComponent<PlayerAttackDragon>().GetTag();
        animator = Player.GetComponent<Animator>();

        hpManager = GameObject.FindWithTag("GameManager").GetComponent<HPManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != tagBox.GetEnemyAttackTag()) return;

        Damage damage = other.GetComponent<Damage>();

        Hp -= damage.GetDamage();

        hpManager.MonsterDamage(damage.GetDamage()*DamageRatio, ref Hp,true);

        if (Hp > 0) return;
        Hp = 100;

        animator.SetBool("DownFlag",true);




    }

}
