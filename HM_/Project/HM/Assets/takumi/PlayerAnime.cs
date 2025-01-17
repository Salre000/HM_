using UnityEngine;

public class PlayerAnime : MonoBehaviour
{

    private Animator _animator;

    private bool _attackFlag = false;
    private bool _moveFlag = true;
    private bool _dieFlag = false;
    private bool _SkillFlag = false;
    private bool _ULTFlag = false;
    private bool _jumpFlag = false;
    private bool _backSteppeFlag = false;
    private bool _bigRoarFlag = false;
    private bool _downFlag = false;
    [SerializeField] float Speed = 0;

    public void SetSpped(float Spped) { _animator.SetFloat("Speed", Spped);this.Speed = Spped; }

    public bool GetAttackFlag() { return _attackFlag; }
    public void SetAttackFlag(bool Flag) {  _attackFlag = Flag; }

    public bool GetMoveFlag() { return _moveFlag; }
    public void SetMoveFlag(bool Flag) { _moveFlag = Flag; }

    public bool GetDieFlag() { return _moveFlag; }
    public void SetDieFlag(bool Flag) {  _moveFlag = Flag; }

    public bool GetLongAttackFlag() { return _SkillFlag; }
    public void SetLoanAttackFlag(bool Flag) { _SkillFlag = Flag; }

    public bool GetRoarFlag() { return _ULTFlag; }
    public void SetRoarFlag(bool Flag) { _ULTFlag = Flag; }

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
        _SkillFlag = false;
        _ULTFlag = false;
        _jumpFlag = false;
        _backSteppeFlag = false;
        _bigRoarFlag = false;
        _downFlag = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetBigRoar()
    {
        _animator.SetBool("BigRoarFlag", _bigRoarFlag);
    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        _animator.SetBool("MoveFlag",_moveFlag);
        _animator.SetBool("AttackFlag",_attackFlag);
        _animator.SetBool("DieFlag", _dieFlag);
        _animator.SetBool("SkillFlag", _SkillFlag);
        _animator.SetBool("ULTFlag", _ULTFlag);
        _animator.SetBool("JumpFlag", _jumpFlag);
        _animator.SetBool("BackSteppeFlag", _backSteppeFlag);
        if (HardDownCount < 0) return;
        HardDownCount++;
        if (HardDownCount <= 1000) return;
        _animator.SetTrigger("EndHardDown");
        _nowDownFlag = false;
        HardDownCount = -1;
    }

    private int HardDownCount = -1;
    public void HardDownCountStart() { HardDownCount = 0; }

}
