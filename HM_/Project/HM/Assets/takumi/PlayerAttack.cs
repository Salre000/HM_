using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using static InputManager;
public abstract class PlayerAttack : MonoBehaviour
{
    protected PlayerAnime _anime;
    protected Vector3 position;
    protected PlayerStatus _status;

    [SerializeField] protected Tag TagBox;


    //�U���̔���𐶐�����\������̃t���O�i�n���^�[�̍U���\���Ɏg�p�j
    bool predictionAttackFlag = false;

    public void SetPredictionAttackFlag(bool Flag) { predictionAttackFlag = Flag; }

    private bool ULTFlag = true;
    public void SetULTFLag(bool Flag) { ULTFlag = Flag; }
    public void ReSetULTFLag() { ULTFlag = true; }
    public Tag GetTag() { return TagBox; }
    void Start()
    {
        _anime = GetComponent<PlayerAnime>();
        _status = GetComponent<PlayerStatus>();

        Application.targetFrameRate = 60;
    }


    protected abstract int LTAttack();
    protected abstract int RTAttack();
    protected abstract int LTRTAttack();
    protected abstract int Jump();
    protected abstract int BarkJump();

    public bool IsCapFlag = false;
    public void IsCap() { _anime.SetRoarFlag(true);}
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(this.transform.position, new Vector3(Mathf.Sin(this.transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(this.transform.eulerAngles.y * Mathf.Deg2Rad)));
        //�������U��������{�^��
        if (instance.IsOnButton(instance.keys[(int)InputKeys.LT]))
        {
            if (LTAttack() > 0) _anime.SetLoanAttackFlag(true);

        }
        else
        {
            _anime.SetLoanAttackFlag(false);

        }

        //�U��������{�^��
        if (instance.IsOnButton(instance.keys[(int)InputKeys.RT]))
        {

            _anime.SetAttackFlag(true);


        }
        else
        {
            _anime.SetAttackFlag(false);

        }

        //LR��������
        if ((instance.IsOnButton(instance.keys[(int)InputKeys.RB]) && instance.IsOnButton(instance.keys[(int)InputKeys.LB]) && ULTFlag) || IsCapFlag)
        {
            if (LTRTAttack() > 0) _anime.SetRoarFlag(true);
            Debug.Log("Anime");
        }
        else
        {
            _anime.SetRoarFlag(false);
        }

        //�O�W�����v
        if (Input.GetAxis("Vertical") >= -0.3f && instance.IsOnButton(instance.keys[(int)InputKeys.A]) && !_anime.GetNowDownFlag() && !_anime.GetAttackFlag())
        {
            if (Jump() > 0) _anime.SetJumpFlag(true);

        }
        else
        {
            _anime.SetJumpFlag(false);

        }

        //�o�b�N�W�����v
        if (Input.GetAxis("Vertical") < -0.3f && instance.IsOnButton(instance.keys[(int)InputKeys.A]) && !_anime.GetNowDownFlag() && !_anime.GetAttackFlag())
        {
            if (BarkJump() > 0) _anime.SetBackSteppeFlag(true);



        }
        else
        {
            _anime.SetBackSteppeFlag(false);



        }

    }

}
