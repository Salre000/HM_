using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static InputManager;

public class CaptorAttackSpider : AnimeBase
{

    //この関数を読んだらアニメーションがジャンプに変わる関数
    System.Action _NestJump;

    //このアクションは拘束攻撃を辞めるアクション二つめはジャンプに切り替えるアクション
    public CaptorAttackSpider(GameObject Object, AudioSource source, Animator animator,
        System.Action<bool> animeFlagReset, System.Action nestJump, GameObject setPosition)
        : base(Object, source, animator, animeFlagReset)
    {

        //指定の名前のアニメーションの時に攻撃を辞めない名前
        AddAnimeName("Armature|RestraintAttackStart");
        AddAnimeName("Armature|RestraintAttackSuccess");
        AddAnimeName("Armature|RestraintAttackLoop");

        //捕まえる場所を獲得
        _captorPosition = setPosition;

        //HPManagerを取得
        _hpManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
        

        //クラスを取得
        _captorHunter = _captorPosition.GetComponent<CaptorHunter>();
        
        //関数のコールバックを与える
        _captorHunter.SetGameObject(SetTarget);
        
        //コールバックを獲得
        _NestJump = nestJump;

    }

    public override void Start()
    {
        base.Start();

        //アニメーションイベントの順番をリセット
        eventNumber = 0;
        
    }

    //ハンターを捕まえるオブジェクトの格納先
    private GameObject _captorPosition;

    //捕まえたハンターのオブジェクトの格納先
    private GameObject _captorTarget;

    //ハンターを捕まえるクラス
    private CaptorHunter _captorHunter;
    
    //捕まえたハンターのロジックの格納先
    private Hunter_AI _targetHunter = null;
    
    //HPを管理するクラスを取得
    private HPManager _hpManager = null;

    //ハンターを捕まえたときにハンターを受け取る関数
    public void SetCaptorObject(GameObject gameObject) { _captorTarget = gameObject; }

    //ハンターを捕まえることが可能になる関数
    public void StartCaptor()
    {
        _captorPosition.gameObject.SetActive(true);
        _captorHunter.SetActiveFlag(true);
    }

    //捕まえたハンターを離す関数
    public void EndTarget()
    {
        if (_captorTarget != null)
        {
            _captorTarget.transform.parent = null;
        }
        _captorPosition.gameObject.SetActive(false);

    }
    //ハンターを捕まえられたかどうかを判断する関数
    public void CheckHitHunter()
    {
        if (_captorTarget == null)
        {
            _AnimeFlagReset(false);
        }
        else 
        {
            _AnimeFlagReset(true);
        }

    }
    //複数のアニメーションイベントを使う際に番号を指定する変数
    int eventNumber = 0;

    //このクラスのアニメーションイベントに使う関数
    public override void AnimeEvent()
    {
        switch (eventNumber)
        {

            case 0:
                StartCaptor();
                break;
            case 1:
                CheckHitHunter();
                break;
        }

        eventNumber++;
    }

    //ハンターを捕まえる関数
    private void SetTarget(GameObject gameObject)
    {
        _captorTarget = gameObject;
        _captorTarget.transform.parent = _captorPosition.transform;

        _targetHunter = _captorTarget.GetComponent<Hunter_AI>();
        if (_targetHunter == null) return;
        
        //ハンターを怯み状態に変更する
        _targetHunter.StartRestraining();

        _captorTarget.transform.localPosition = Vector3.zero;
    }

    //このスプリクトの行動関数
    public override void Action()
    {  
        AnimeUPDate();

        //攻撃ループに入ったら指摘のキーを離さない限り続けるように変更
        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Armature|RestraintAttackLoop" && !instance.IsOnButton(InputKeys.Special1)&& !instance.IsOnButton(InputKeys.Special2))
            _AnimeFlagReset(false);

        //掴んでいるハンターが死んでいるかを判断
        if (_targetHunter == null) return;

        Hunter_AI hunter = _targetHunter.GetComponent<Hunter_AI>();
        if (hunter == null) return;

        //掴んでいるハンターが死んだかどうかの判定
        if (hunter.GetHunterID() != _hpManager.GetHunterLostNumber()) return;
    }

    //このクラスの行動が終わる時に呼ばれる関数
    override protected void AnimeEnd()
    {
        base.AnimeEnd();

        //もしも今のアニメーションがジャンプだったジャンプの行動を行う
        if ("Armature|Jump" == _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name)
        {
            useFlag = true;

            _NestJump();
        }
        else
        {
            useFlag = false;
        }

        //ハンターを手放す処理
        EndTarget();

        //捕まえているハンターがいたら拘束状態を解除する
        if (_targetHunter != null)
            _targetHunter.StopRestraining();



    }
}
