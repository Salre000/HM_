using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHPManager : MonoBehaviour
{
    // �n���^�[��HP
    public float hp = 1000;

    public float maxhp = 1000;

    public bool isDeadFlag = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Damage>() == null) return;

        if(other.gameObject.tag== "PlayerAttack"&&"Hunter"==this.tag)
        {
            Damage(other.GetComponent<Damage>().GetDamage());

        }
    }

    private void Update()
    {
        // HP��0�ɂȂ�����
        if (hp < 0)
        {
            hp = 0;
            // ���S����
            isDeadFlag = true;
        }
    }

    // HP�̌������� 
    void Damage(float damage)
    {
        hp -= damage;
    }

    // HP��ێ�
    public float GetHP() { return hp; }

  
}
