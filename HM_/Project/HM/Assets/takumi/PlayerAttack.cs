using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using static InputManager;
public abstract class PlayerAttack : MonoBehaviour
{
    protected PlayerAnime _anime;
    protected Vector3 position;

    [SerializeField] protected Tag TagBox;


    //攻撃の判定を生成する予備動作のフラグ（ハンターの攻撃予測に使用）
    bool predictionAttackFlag = false;

    public void SetPredictionAttackFlag(bool Flag) { predictionAttackFlag = Flag; }

    private bool ULTFlag=true;
    public void SetULTFLag(bool Flag) { ULTFlag = Flag; }
    public void ReSetULTFLag() { ULTFlag = true; }
    public Tag GetTag() { return TagBox; }
    void Start()
    {
        _anime = GetComponent<PlayerAnime>();

        Application.targetFrameRate = 60;
    }

    
    protected abstract void LTAttack();
    protected abstract void RTAttack();
    protected abstract void LTRTAttack();
    protected abstract void Jump();
    protected abstract void BarkJump();

    public bool IsCapFlag = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(this.transform.position,new Vector3(Mathf.Sin(this.transform.eulerAngles.y*Mathf.Deg2Rad),0,Mathf.Cos(this.transform.eulerAngles.y * Mathf.Deg2Rad)));
        //遠距離攻撃をするボタン
        if (instance.IsOnButton(instance.keys[(int)InputKeys.LT]))
        {
            LTAttack();
            _anime.SetLoanAttackFlag(true);

        }
        else
        {
            _anime.SetLoanAttackFlag(false);

        }

        //攻撃をするボタン
        if (instance.IsOnButton(instance.keys[(int)InputKeys.RT]))
        {
            _anime.SetAttackFlag(true);


        }
        else
        {
            _anime.SetAttackFlag(false);

        }

        //LR同時押し
        if ((instance.IsOnButton(instance.keys[(int)InputKeys.RB]) && instance.IsOnButton(instance.keys[(int)InputKeys.LB])&& ULTFlag)|| IsCapFlag)
        {
            _anime.SetRoarFlag(true);
            LTRTAttack();

        }
        else
        {
            _anime.SetRoarFlag(false);
        }

        //前ジャンプ
        if (Input.GetAxis("Vertical") >= -0.3f && instance.IsOnButton(instance.keys[(int)InputKeys.A]) && !_anime.GetNowDownFlag() && !_anime.GetAttackFlag())
        {
            _anime.SetJumpFlag(true);
            Jump();

        }
        else
        {
            _anime.SetJumpFlag(false);

        }

        //バックジャンプ
        if (Input.GetAxis("Vertical") < -0.3f && instance.IsOnButton(instance.keys[(int)InputKeys.A]) && !_anime.GetNowDownFlag() && !_anime.GetAttackFlag())
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
