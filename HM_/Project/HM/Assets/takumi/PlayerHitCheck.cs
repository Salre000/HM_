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
    [Header("攻撃を受ける時に使うダメージの倍率")]
    [SerializeField] float DamageRatio = 1.0f;
    [SerializeField] AudioSource source;

    public enum Type
    {
        Normal,
        Hard,
        None
    }

    [SerializeField] Type _type = Type.Hard;


    // Start is called before the first frame update
    void Start()
    {
        _playerAnime = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnime>();
        _status = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    //何かに当たった時
    //(トリガー同士も獲得してくれる)
    private void OnTriggerEnter(Collider other)
    {


        //敵の攻撃を受けた
        if (other.gameObject.tag == "EnemyAttack")
        {

            //敵の攻撃力を利用した挙動
            Damage _damage = other.GetComponent<Damage>();

            ////HPを減らす

            TestCollision test = other.GetComponent<TestCollision>();

            Hunter_AI AI = test.GetGameObject().GetComponentInParent<Hunter_AI>();

            int ID = AI.GetHunterID();

            _status.MonsterDamage(_damage.GetDamage() * DamageRatio, ref Hp, _playerAnime.GetNowDownFlag(), () =>
            {

                HitEffectManager.instance.HitEffectShow(other.transform.position, (HitEffectManager.CharacterType)ID + 1);

                //ハンターごとの攻撃を当てた時の音を呼び出す

                source.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Dragon.DragonAttackHit, (int)Main.Hunter), SoundListManager.instance.GetSoundVolume());

                source.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.PreArechSE + (ID + 1), (int)Main.Monster), SoundListManager.instance.GetSoundVolume());

            });


            if (Hp <= 0)
            {
                switch (_type)
                {
                    case Type.Normal: _playerAnime.SetDownFlag(); break;
                    case Type.Hard: _playerAnime.SetStartHardDownFlag(); break;
                    case Type.None: break;


                }

                Hp = 300;
            }
        }
    }
}
