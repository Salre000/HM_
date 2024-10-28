using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{

    private Animator _animator;

    private bool _attackFlag = false;
    private bool _moveFlag = true;
    private bool _dieFlag = false;

    public bool GetAttackFlag() { return _attackFlag; }
    public void SetAttackFlag(bool Flag) {  _attackFlag = Flag; }
    public bool GetMoveFlag() { return _moveFlag; }
    public void SetMoveFlag(bool Flag) { _moveFlag = Flag; }
    public bool GetDieFlag() { return _moveFlag; }
    public void SetDieFlag(bool Flag) {  _moveFlag = Flag; }

    public void ResetFlag()
    {
        _attackFlag = false;
        _dieFlag = false;
        _moveFlag = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("MoveFlag",_moveFlag);
        _animator.SetBool("AttackFlag",_attackFlag);





        
    }
}
