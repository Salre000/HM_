using Den.Tools;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

//プレイヤーをステータスを管理するクラス
public class PlayerStatus : MonoBehaviour
{
    //現在のプレイヤーのHPの変数
    [SerializeField] float HP = 0.0f;

    //プレイヤーの最大HPの定数
   　const float MAXHP = 100.0f;

    //プレイヤーの速度
    [SerializeField] float Speed = 0.0002f;

    //プレイヤーの回転の速度の変数
    float RotateSpeed = 0.5f;

    //最大HPを返す関数
    public float GetMaxHP() { return MAXHP; }

    public float GetHP() { return HP; }

    public void Damage(float Damage) { HP -= Damage; if (HP <= 0) { _anime.SetDieFlag(true); } }

    //プレイヤーのスピードを返す関数関数
    public float GetSpeed() { return Speed; }

    public float GetRotateSpeed() { return RotateSpeed; }

    private PlayerAnime _anime;

    private AudioSource _audioSource;

    public enum Condition
    {
        Normal,//通常状態
        Stun,//気絶状態
        Anger,//怒り状態
        Fatigue//疲労状態
    }
    public Condition GetNowCondition() { return _nowCondition; }

    [SerializeField] int StunGage = 0;
    const int MaxStunGage = 1000;
    public void AddStunGage(int Add)
    {
        if (_nowCondition != Condition.Normal) return;
        StunGage += Add;
        if (MaxStunGage > StunGage) return;
        StunGage = MaxStunGage;
        ChengeCondition(Condition.Stun);

        _anime.SetSpped(1.3f);
    }

    public void SbuStunGage(int Sbu)
    {
        if (_nowCondition != Condition.Stun) return;
        StunGage -= Sbu;
        if (StunGage > 0) return;
        StunGage = 0;
        ChengeCondition(Condition.Fatigue);
        _anime.SetSpped(0.8f);


    }

    [SerializeField] int StunCount = 0;
    int MAXStunCount = 3600;
    //ノーマルモードに変更する関数
    private void ChengeNomale()
    {
        if (_nowCondition != Condition.Fatigue) return;
        StunCount++;

        if (StunCount < MAXStunCount) return;

        _anime.SetSpped(1.0f);
        ChengeCondition(Condition.Normal);
        StunCount = 0;

    }


    private Condition _nowCondition;
    private Condition _lostCondition;

    private void ChengeCondition(Condition condition)
    {
        if (_nowCondition == condition) return;
        _lostCondition = _nowCondition;
        _nowCondition = condition;


    }

    public void Start()
    {
        _anime = this.gameObject.GetComponent<PlayerAnime>();
        _audioSource = this.transform.AddComponent<AudioSource>();
        _audioSource.loop = false;
        //音量とかを調整する

        HP = MAXHP;
    }
    public void FixedUpdate()
    {
        AddStunGage(1);
        SbuStunGage(1);
        ChengeNomale();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        _audioSource.clip = clip;
        _audioSource.Play();


    }

}
