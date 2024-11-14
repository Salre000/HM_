using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHPManager : MonoBehaviour
{
    // ƒnƒ“ƒ^[‚ÌHP
    public float hp = 100;

    public float maxhp = 100;

    public bool isDeadFlag=false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "PlayerAttack")
        {
            Damage(other.GetComponent<Damage>().GetDamage());
        }
    }

    // HP‚ÌŒ¸­ˆ— 
    void Damage(float damage)
    {
        hp -= damage;
        // HP‚ª0‚É‚È‚Á‚½‚ç
        if (hp < 0)
        {
            hp = 0;

            // €–S”»’è
            isDeadFlag = true;
        }
    }

    // HP‚ğ•Û
    public float GetHP() { return hp; }

  
}
