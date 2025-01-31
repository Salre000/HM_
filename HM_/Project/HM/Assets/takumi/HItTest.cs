using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HItTest : MonoBehaviour
{
    private HPManager _status;
    private PlayerAnime _playerAnime;
    [SerializeField] float Hp = 300;
    [Header("�U�����󂯂鎞�Ɏg���_���[�W�̔{��")]
    [SerializeField] float DamageRatio = 1.0f;

    public enum Type 
    {
        Normal,
        Hard,
        None
    }

   [SerializeField] Type _type=Type.Hard;
    

    // Start is called before the first frame update
    void Start()
    {
        _playerAnime=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnime>();
        _status = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
    }

    //�����ɓ���������
    //(�g���K�[���m���l�����Ă����)
    private void OnTriggerEnter(Collider other)
    {


        //�G�̍U�����󂯂�
        if (other.gameObject.tag == "EnemyAttack")
        {

            //�G�̍U���͂𗘗p��������
            Damage _damage=other.GetComponent<Damage>();

            ////HP�����炷
            _status.MonsterDamage(_damage.GetDamage()* DamageRatio, ref Hp,_playerAnime.GetNowDownFlag());

            int ID = other.GetComponent<TestCollision>().GetGameObject().GetComponentInParent<Hunter_AI>().GetHunterID();

            if (other == null) 
            {

                int ss = 0;
            }
            HitEffectManager.instance.HitEffectShow(other.transform.position,(HitEffectManager.CharacterType)ID+1);



            if (Hp <= 0) 
            {
                switch (_type) 
                {
                    case Type.Normal: _playerAnime.SetDownFlag(); break;
                    case Type.Hard: _playerAnime.SetStartHardDownFlag(); break;
                    case Type.None:break;


                }

                Hp = 300;
            }
        }
    }
}
