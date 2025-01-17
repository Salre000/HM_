using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{

    private Animator _animator;

    private bool _attackFlag = false;
    private bool _moveFlag = true;
    private bool _dieFlag = false;
    private bool _longAttack = false;
    private bool _roarFlag = false;
    private bool _jumpFlag = false;
    private bool _backSteppeFlag = false;
    private bool _bigRoarFlag = false;
    private bool _endHardDown = false;
    private bool _downFlag = false;
    [SerializeField] float Speed = 0;

    public void SetSpped(float Spped) { _animator.SetFloat("Speed", Spped);this.Speed = Spped; }

    public bool GetAttackFlag() { return _attackFlag; }
    public void SetAttackFlag(bool Flag) {  _attackFlag = Flag; }

    public bool GetMoveFlag() { return _moveFlag; }
    public void SetMoveFlag(bool Flag) { _moveFlag = Flag; }

    public bool GetDieFlag() { return _moveFlag; }
    public void SetDieFlag(bool Flag) {  _moveFlag = Flag; }

    public bool GetLongAttackFlag() { return _longAttack; }
    public void SetLoanAttackFlag(bool Flag) { _longAttack = Flag; }

    public bool GetRoarFlag() { return _roarFlag; }
    public void SetRoarFlag(bool Flag) { _roarFlag = Flag; }

    public bool GetJumpFlag() { return _jumpFlag; }
    public void SetJumpFlag(bool Flag) { _jumpFlag = Flag; }

    public bool GetBackSteppeFlag() { return _backSteppeFlag; }
    public void SetBackSteppeFlag(bool Flag) { _backSteppeFlag = Flag; }

    public bool GetBigRoarFlag() { return _bigRoarFlag; }
    public void SetBigRoarFlag(bool Flag) { _bigRoarFlag = Flag; }


    public void SetStartHardDownFlag()
    {
        _animator.SetTrigger("StartHardDown");
        _nowDownFlag = true;
    }

    public bool GetEndHardDownFlag() { return _endHardDown; }
    public void SetEndHardDownFlag(bool Flag)
    {
        _endHardDown = Flag;
    }
    public void ReSetEndHardDownFlag() { _endHardDown = false; }

    public bool GetDownFlag() { return _downFlag; }
    public void SetDownFlag() { _animator.SetTrigger("DownFlag"); }

    private bool _nowDownFlag = false;  
    //ÉÇÉìÉXÉ^Å[
    public bool GetNowDownFlag() { return _nowDownFlag; }

    public void ResetFlag()
    {
        _attackFlag = false;
        _moveFlag = true;
        _dieFlag = false;
        _longAttack = false;
        _roarFlag = false;
        _jumpFlag = false;
        _backSteppeFlag = false;
        _bigRoarFlag = false;
        _endHardDown = false;
        _downFlag = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        _animator.SetBool("MoveFlag",_moveFlag);
        _animator.SetBool("AttackFlag",_attackFlag);
        _animator.SetBool("DieFlag", _dieFlag);
        _animator.SetBool("LongAttack",_longAttack);
        _animator.SetBool("RoarFlag", _roarFlag);
        _animator.SetBool("JumpFlag", _jumpFlag);
        _animator.SetBool("BackSteppeFlag", _backSteppeFlag);
        _animator.SetBool("BigRoarFlag", _bigRoarFlag);
        _animator.SetBool("EndHardDown", _endHardDown);
        if (HardDownCount < 0) return;
        HardDownCount++;
        Debug.Log(HardDownCount);
        if (HardDownCount <= 1000) return;
        _endHardDown = true;
        _nowDownFlag = false;
        HardDownCount = -1;
    }

    private int HardDownCount = -1;
    public void HardDownCountStart() { HardDownCount = 0; }

}
