using Cysharp.Threading.Tasks;
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


    //�U���̔���𐶐�����\������̃t���O�i�n���^�[�̍U���\���Ɏg�p�j
    [SerializeField]bool predictionAttackFlag = false;

    public void SetPredictionAttackFlag() {predictionAttackFlag = true; ResetPredictionAttackFlag(); }

    private async UniTask ResetPredictionAttackFlag() 
    {

        await UniTask.DelayFrame(14);


        predictionAttackFlag = false;

    }

    public bool GetPredictionAttackFlag() {  return predictionAttackFlag; }

    private bool ULTFlag = true;
    public void SetULTFLag(bool Flag) { ULTFlag = Flag; }
    public void ReSetULTFLag() { ULTFlag = true; }
    public Tag GetTag() { return TagBox; }

    [SerializeField] protected AnimeBase nowAnime = null;
    void Awake()
    {
        _anime = GetComponent<PlayerAnime>();
        _status = GetComponent<PlayerStatus>();

        
        Application.targetFrameRate = 60;
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
    public void IsCap() { _anime.SetRoarFlag(true);}

    protected enum actionMode 
    {
        normal,
        skill,
        special,
        jump,
        backJump,
        max


    }
    [SerializeField]protected actionMode nowMode = actionMode.normal;
    [SerializeField]protected AnimeBase []anime=new AnimeBase[(int)actionMode.max];

    public void NowAnimeEvent() 
    {
        anime[(int)nowMode].AnimeEvent();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (AnimeBase.useFlag) 
        {

            anime[(int)nowMode].Action();

            return;
        }

        //�������U��������{�^��
        if (instance.IsOnButton(InputKeys.LT))
        {
            if (LTAttack() > 0) _anime.SetLoanAttackFlag(true);

        }
        //�U��������{�^��
        if (instance.IsOnButton(InputKeys.RT))
        {
            _anime.SetAttackFlag(true);


        }
        else
        {
            _anime.SetAttackFlag(false);

        }

        //LR��������
        if ((instance.IsOnButton(InputKeys.RB) && instance.IsOnButton(InputKeys.LB) && ULTFlag) || IsCapFlag)
        {
            if (LTRTAttack() > 0) _anime.SetRoarFlag(true);
            Debug.Log("Anime");
        }
        else
        {
            _anime.SetRoarFlag(false);
        }

        //�O�W�����v
        if (Input.GetAxis("Vertical") >= -0.3f && instance.IsOnButton(InputKeys.A) && !_anime.GetNowDownFlag() && !_anime.GetAttackFlag())
        {
            _anime.SetJumpFlag(true);
            Jump();

        }
        else
        {
            _anime.SetJumpFlag(false);

        }

        //�o�b�N�W�����v
        if (Input.GetAxis("Vertical") < -0.3f && instance.IsOnButton(InputKeys.A) && !_anime.GetNowDownFlag() && !_anime.GetAttackFlag())
        {
            _anime.SetBackSteppeFlag(true);
            BarkJump() ;



        }
        else
        {
            _anime.SetBackSteppeFlag(false);



        }

    }
}
