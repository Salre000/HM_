using SceneSound;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerHitCheck : MonoBehaviour
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
           int hitCheck= _status.MonsterDamage(_damage.GetDamage()* DamageRatio, ref Hp,_playerAnime.GetNowDownFlag());

            TestCollision test = other.GetComponent<TestCollision>();

            Hunter_AI AI = test.GetGameObject().GetComponentInParent<Hunter_AI>();

            int ID = AI.GetHunterID();

            if(hitCheck>=0) HitEffectManager.instance.HitEffectShow(other.transform.position, (HitEffectManager.CharacterType)ID + 1);

            //�n���^�[���Ƃ̍U���𓖂Ă����̉����Ăяo��

            if(hitCheck>=0)SoundListManager.instance.GetAudioClip((int)Dragon.DragonAttackHit, (int)Main.Hunter);


            SoundListManager.instance.GetAudioClip( (int)HunterSE.PreArechSE+(ID+1), (int)Main.Monster);

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
