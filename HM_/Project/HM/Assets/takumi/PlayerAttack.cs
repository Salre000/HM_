using Cysharp.Threading.Tasks;
using SceneSound;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

using static InputManager;

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerAttack))]
#endif
public abstract class PlayerAttack : MonoBehaviour
{
    protected PlayerAnime _anime;
    protected Vector3 position;
    protected PlayerStatus _status;

    [SerializeField] protected Tag TagBox;

    protected AudioClip HitAttackSound()
    {
        switch (nowMode)
        {
            case actionMode.normal:
                return SoundListManager.instance.GetAudioClip((int)Dragon.DragonAttackHit, (int)Main.Monster);


            case actionMode.skill:

            case actionMode.special:


            case actionMode.jump:


            case actionMode.backJump:

                return SoundListManager.instance.GetAudioClip((int)Dragon.DragonLongAttack, (int)Main.Monster);


        }

        return null;

    }

    //攻撃の判定を生成する予備動作のフラグ（ハンターの攻撃予測に使用）
    [SerializeField] bool predictionAttackFlag = false;

    public void SetPredictionAttackFlag() { predictionAttackFlag = true; ResetPredictionAttackFlag(); }

    private async UniTask ResetPredictionAttackFlag()
    {

        await UniTask.DelayFrame(14);


        predictionAttackFlag = false;

    }

    public bool GetPredictionAttackFlag() { return predictionAttackFlag; }

    private bool ULTFlag = true;
    public void SetULTFLag(bool Flag) { ULTFlag = Flag; }
    public void ReSetULTFLag() { ULTFlag = true; }
    public Tag GetTag() { return TagBox; }

    [SerializeField] protected AnimeBase nowAnime = null;
    void Awake()
    {
        _anime = GetComponent<PlayerAnime>();
        _status = GetComponent<PlayerStatus>();

        activeFlag = true;

        HunterManager hunterManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HunterManager>();

        AnimeBase.useFlag = false;


        Application.targetFrameRate = 60;

        DelayTask(() =>
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject gameObject = hunterManager.GetHunterObject(i);
                Hunter_AI ai = gameObject.GetComponent<Hunter_AI>();
                ai.SetMonsterHitSound(HitAttackSound);

            }

        }, 10).Forget();
    }

    async UniTask DelayTask(System.Action action, int delayTime)
    {
        await UniTask.DelayFrame(delayTime);

        action();
    }

    protected async UniTask ResetFlag(System.Action<bool> action)
    {
        await UniTask.DelayFrame(10);
        action(false);

    }


    protected abstract int LTAttack();
    protected abstract int RTAttack();
    protected abstract int LTRTAttack();
    protected abstract int Jump();
    protected abstract int BarkJump();

    public bool IsCapFlag = false;
    public void IsCap() { _anime.SetRoarFlag(true); }

    protected enum actionMode
    {
        normal,
        skill,
        special,
        jump,
        backJump,
        max


    }
    [SerializeField] protected actionMode nowMode = actionMode.normal;
    [SerializeField] protected AnimeBase[] anime = new AnimeBase[(int)actionMode.max];

    public void NowAnimeEvent()
    {
        anime[(int)nowMode].AnimeEvent();
    }

    public static bool activeFlag = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activeFlag) return;

        if (AnimeBase.useFlag)
        {

            anime[(int)nowMode].Action();

            return;
        }

        //遠距離攻撃をするボタン
        if (instance.IsOnButton(InputKeys.LT))
        {
            if (LTAttack() > 0) _anime.SetLoanAttackFlag(true);

        }
        //攻撃をするボタン
        if (instance.IsOnButton(InputKeys.RT))
        {

            _anime.SetAttackFlag(true);


        }
        else
        {
            _anime.SetAttackFlag(false);

        }

        //LR同時押し
        if ((instance.IsOnButton(InputKeys.RB) && instance.IsOnButton(InputKeys.LB) && ULTFlag) || IsCapFlag)
        {
            if (LTRTAttack() > 0) _anime.SetRoarFlag(true);
            Debug.Log("Anime");
        }
        else
        {
            _anime.SetRoarFlag(false);
        }

        //前ジャンプ
        if (Input.GetAxis("Vertical") >= -0.3f && instance.IsOnButton(InputKeys.A) && !_anime.GetNowDownFlag() && !_anime.GetAttackFlag())
        {
            _anime.SetJumpFlag(true);
            Jump();

        }
        else
        {
            _anime.SetJumpFlag(false);

        }

        //バックジャンプ
        if (Input.GetAxis("Vertical") < -0.3f && instance.IsOnButton(InputKeys.A) && !_anime.GetNowDownFlag() && !_anime.GetAttackFlag())
        {
            _anime.SetBackSteppeFlag(true);
            BarkJump();



        }
        else
        {
            _anime.SetBackSteppeFlag(false);



        }

    }
}
