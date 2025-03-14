using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//攻撃判定のスプリクト
public class AttackArea : MonoBehaviour
{

    //追従先のオブジェクト
    [SerializeField] private GameObject _parent;
    SphereCollider _collider;
    private Damage _damage;

    //当たり判定を消すまでの時間
    private readonly int MAX_TIME = 3;

    private int _countTime = 0;

    public void Awake()
    {
        //あたり判定を生成
        _collider = this.gameObject.AddComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = 0.1f;

        //攻撃判定に必要なクラスをゲット
        _damage = GetComponent<Damage>();

    }


    private void FixedUpdate()
    {
        //一定時間経つとオブジェクトのアクティブを変更
        if (_countTime >= MAX_TIME)
        {
            transform.gameObject.SetActive(false);
            _countTime = 0;
        }

        //攻撃判定を出すオブジェクトに追従
        transform.position = _parent.transform.position;

        //フレーム計測
        _countTime++;
    }

    //あたり判定を有効にする関数
    public void SetAttackArea(GameObject parent, float Damage, float radius = 0.1f, int CountTime = 0)
    {

        this._countTime = CountTime;
        _parent = parent;
        CountTime = 0;
        _collider.radius = radius;
        transform.gameObject.SetActive(true);
        _damage.SetDamage(Damage);

    }








}
