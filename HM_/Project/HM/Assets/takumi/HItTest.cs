using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItTest : MonoBehaviour
{
    private HPManager _status;
    

    // Start is called before the first frame update
    void Start()
    {
        _status = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
    }

    //�����ɓ���������
    //(�g���K�[���m���l�����Ă����)
    private void OnTriggerEnter(Collider other)
    {

      Debug.Log("��������");


        //�G�̍U�����󂯂�
        if (other.gameObject.tag == "EnemyAttack")
        {


            //�G�̍U���͂𗘗p��������
            Damage _damage=other.GetComponent<Damage>();

            ////HP�����炷
            _status.MonsterDamage(_damage.GetDamage());
        }
    }
}
