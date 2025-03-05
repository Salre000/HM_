using Den.Tools;
using JetBrains.Annotations;
using SceneSound;
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

    public static bool isLife = true;
    public static PlayerStatus Instance { get; private set; }

    [HideInInspector] public OptionDataMain data;
    public enum Condition
    {
        Normal,//通常状態
        Stun,//気絶状態
        Anger,//怒り状態
        Fatigue//疲労状態
    }
    public Condition GetNowCondition() { return _nowCondition; }

    [SerializeField] int AngerGage = 0;
    const int MaxAngerGage = 1000;
    public void AddAngerGage(int Add)
    {
        if (_nowCondition != Condition.Normal) return;
        //UIのゲージを変更する処理を入れる
        AngerGage += Add;
        if (MaxAngerGage > AngerGage) return;
        AngerGage = MaxAngerGage;
        ChengeCondition(Condition.Anger);

        _anime.SetSpped(1.3f);
    }

    public void SbuAngerGage(int Sbu)
    {
        if (_nowCondition != Condition.Anger) return;
        AngerGage -= Sbu;
        if (AngerGage > 0) return;
        AngerGage = 0;
        ChengeCondition(Condition.Fatigue);
        _anime.SetSpped(0.8f);


    }

    [SerializeField] int FatigueGage = 0;
    int MAXFatigueGage = 3600;
    //ノーマルモードに変更する関数
    private void ChengeNomale()
    {
        if (_nowCondition != Condition.Fatigue) return;
        FatigueGage++;

        if (FatigueGage < MAXFatigueGage) return;

        _anime.SetSpped(1.0f);
        ChengeCondition(Condition.Normal);
        FatigueGage = 0;

    }


    [SerializeField]private Condition _nowCondition;
    private Condition _lostCondition;

    System.Action<Condition> ChengeConditionMode; 

    public void SetCallBackCondition(System.Action<Condition>action) { ChengeConditionMode = action; }

    private void ChengeCondition(Condition condition)
    {
        if (_nowCondition == condition) return;
        _lostCondition = _nowCondition;
        _nowCondition = condition;
        ChengeConditionMode(condition);

    }

    public void SetData(OptionDataMain data) { this.data=data; }

    public void Start()
    {
        data = JsonDataModule.Load<OptionDataMain>(Application.streamingAssetsPath + "/OptionMain.json");

        Instance = this;
        isLife = true;
        _anime = this.gameObject.GetComponent<PlayerAnime>();
        _audioSource = this.transform.AddComponent<AudioSource>();
        _audioSource.loop = false;
        //音量とかを調整する

        HP = MAXHP;

        _anime.SetCallback(StartStu, condition =>
        {

            ChengeCondition(condition);



        });

    }

    private Condition StartStu() 
    {
        Condition condition=_nowCondition;

        ChengeCondition(Condition.Stun);

        return condition;


    }
    public void FixedUpdate()
    {
        AddAngerGage(1);
        SbuAngerGage(1);
        ChengeNomale();
    }

    public void NormalVoice() 
    {
        _audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip(3, (int)Main.Monster), SoundListManager.instance.GetSoundVolume());


    }

}
