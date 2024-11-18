using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHPManager : MonoBehaviour
{
    // �n���^�[��HP
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
