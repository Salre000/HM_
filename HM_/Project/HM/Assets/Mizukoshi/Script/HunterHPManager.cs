using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHPManager : MonoBehaviour
{
    // �n���^�[��HP
    public int hp = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �^�O�̖��O�ɂ���ă_���[�W�������s��
        if (collision.gameObject.tag == "Player")
        {
            Damage(10/**/);
        }
    }

    // HP�̌������� 
    void Damage(int damage)
    {
        hp -= damage;
        // HP��0�ɂȂ�����
        if (hp < 0)
        {
            hp = 0;
        }
    }
}
