using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHPManager : MonoBehaviour
{
    // ÉnÉìÉ^Å[ÇÃHP
    public float hp = 100;

    public float maxhp = 100;

    public bool isDeadFlag = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "PlayerAttack")
        {
            Damage(other.GetComponent<Damage>().GetDamage());
        }
    }

    private void Update()
    {
        // HPÇ™0Ç…Ç»Ç¡ÇΩÇÁ
        if (hp < 0)
        {
            hp = 0;
            // éÄñSîªíË
            isDeadFlag = true;
        }
    }

    // HPÇÃå∏è≠èàóù 
    void Damage(float damage)
    {
        hp -= damage;
    }

    // HPÇï€éù
    public float GetHP() { return hp; }

  
}
