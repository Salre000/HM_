using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHPManager : MonoBehaviour
{
    // �n���^�[��HP
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
        
        // HP��0�����S�A�j���[�V�������I��������
        if (hp < 0&&this.gameObject.GetComponent<Hunter_AI>().deathAnimationFinish)
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
