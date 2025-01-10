using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHPManager : MonoBehaviour
{
    // ハンターのHP
    public float hp = 100;

    public float maxhp = 100;

    public bool isDeadFlag = false;

    public bool isHit = false;

    public new Collider collider;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Damage>() == null) return;

        if (other.gameObject.tag == "PlayerAttack" && "Hunter" == this.tag)
        {
            //Damage(other.GetComponent<Damage>().GetDamage());
            isHit = true;
            collider = other;
        }
    }

    private void Update()
    {
        
        // HPが0かつ死亡アニメーションを終了したら
        if (hp < 0&&this.gameObject.GetComponent<Hunter_AI>().deathAnimationFinish)
        {
            hp = 0;
            // 死亡判定
            isDeadFlag = true;
        }
    }

    // HPの減少処理 
    void Damage(float damage)
    {
        hp -= damage;
    }

    // HPを保持
    public float GetHP() { return hp; }

  
}
