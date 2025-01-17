using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    protected PlayerAnime _anime;
    protected Vector3 position;

    [SerializeField] protected Tag TagBox;

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


    // Update is called once per frame
    void FixedUpdate()
    {
        //�������U��������{�^��
        if (Input.GetKey(InputManager.instance.config.lt))
        {
            LTAttack();
            _anime.SetLoanAttackFlag(true);

        }
        else
        {
            _anime.SetLoanAttackFlag(false);

        }

        //�U��������{�^��
        if (Input.GetKey(InputManager.instance.config.rt))
        {
            _anime.SetAttackFlag(true);


        }
        else
        {
            _anime.SetAttackFlag(false);

        }

        //LR��������
        if (Input.GetKey(InputManager.instance.config.rb) && Input.GetKey(InputManager.instance.config.lb))
        {
            _anime.SetRoarFlag(true);

        }
        else
        {
            _anime.SetRoarFlag(false);
        }

        //�O�W�����v
        if (Input.GetAxis("Vertical") >= -0.3f && Input.GetKey(InputManager.instance.config.a) && !_anime.GetNowDownFlag() && !_anime.GetAttackFlag())
        {
            _anime.SetJumpFlag(true);
            Jump();

        }
        else
        {
            _anime.SetJumpFlag(false);

        }

        //�o�b�N�W�����v
        if (Input.GetAxis("Vertical") < -0.3f && Input.GetKey(InputManager.instance.config.a) && !_anime.GetNowDownFlag() && !_anime.GetAttackFlag())
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
