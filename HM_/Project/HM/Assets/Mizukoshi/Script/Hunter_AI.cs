using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// ハンターの基底クラス
/// </summary>
public abstract class Hunter_AI : MonoBehaviour
{
    private GameObject _monster;

    private GameObject[] _monsters;

    private NavMeshAgent _agent;

    AnimatorStateInfo animationState;

    private Animator _animator;

    public Damage damage;

    // モンスターの位置を発見したかどうかのフラグ
    public bool monsterDisplay=false;

    public HunterManager manager;

    public HPManager hpManager;

    public int HP = 100;

    public bool attackReady = false;

    private 

    // Start is called before the first frame update
    void Start()
    {
        // モンスターのタグ取得
        _monster = GameObject.FindGameObjectWithTag("Player");
        _monsters=GameObject.FindGameObjectsWithTag("Player");
        _animator =GetComponent<Animator>();
       
        _agent= GetComponent<NavMeshAgent>();
    }

    //------------------------------------------------
    //                    処理
    //------------------------------------------------

    public void SetDestination(Vector3 pos)
    {
        _agent.destination = pos;
    }

    /// <summary>
    /// 探索関数
    /// </summary>
    public virtual void Search()
    {
        
    }

    // モンスターの発見した時に呼ぶ関数
    public void DisappearMonster()
    {
        manager.SetDisapper();
    }

    /// <summary>
    /// 攻撃できる距離にいるか
    /// </summary>
    /// <param name="acceptDistance"></param>
    /// <returns>攻撃できる距離ならtrue,できないならばfalse</returns>
    public bool CheckAttackDistance(float acceptDistance, GameObject AIType)
    {
        float calculate = Vector3.Distance(_monster.transform.position, AIType.transform.position);
        return calculate < acceptDistance;
    }

    public bool CheckKeepDistance(float acceptDistance, GameObject AIType)
    {
       
        float calculate = Vector3.Distance(_monster.transform.position, AIType.transform.position);
        //Debug.Log(calculate);
        return calculate > acceptDistance;
    }
    /// <summary>
    /// モンスターが見える位置かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsMonsterInSight()
    {
        return true;
    }

    //-------------------------------------------------------------------------
    //                           行動関係関数
    //-------------------------------------------------------------------------
   
    /// <summary>
    /// 
    /// </summary>
    public void Attack()
    {
        AttackAnimation();
        Debug.Log("攻撃");
    }
    /// <summary>
    /// 追跡関数
    /// </summary>
    public void Chase()
    {
        if(!_agent.enabled) return;
       _agent.destination=_monster.transform.position;
    }

    public void Run()
    {

    }

    public void Avoid()
    {

    }

    public bool CheckAttackCoolTime()
    {
        return true;
    }

    //-------------------------------------------------------------------------
    //                     アニメーション関係関数
    //-------------------------------------------------------------------------

    /// <summary>
    /// 現在のアニメーションの状態を取得
    /// </summary>
    /// <returns></returns>
    public AnimatorStateInfo GetAnimState()
    {
        return animationState;
    }

    // 拘束状態の開始 アニメーションの開始
    public void StartRestraining()
    {
        
        _agent.enabled = false;
        _animator.SetTrigger("FlatterStartTrigger");
       
    }

    // 拘束状態の終了　アニメーションの終了
    public void StopRestraining()
    {
        _agent.enabled = true;
        _animator.SetTrigger("FlatterFinishTrigger");
    }
    
    // 攻撃アニメーション再生関数
    public void AttackAnimation()
    {
        _animator.SetTrigger("AttackTrigger");
    }

    // 走るアニメーション再生関数
    public void RunAnimation()
    {

    }
    
    // 死亡アニメーション再生関数
    public void DeathAnimation()
    {
        _animator.SetTrigger("Death");
        _agent.isStopped = true;

    }

    // 怯みアニメーション再生関数
    public void FlatterAnimation()
    {

    }

    public void AvoidAnimation()
    {
       
    }


    // モンスターオブジェクトの取得
    public GameObject GetMonster()
    {
        return _monster;
    }
    

    public bool GetFlat()
    {
        return (_animator.GetBool("FlatterStartTrigger") && !_animator.GetBool("FlatterFinishTrigger"));
    }

    public int GetHunterID()
    {
        return this.GetComponent<Hunter_ID>().GetHunterID(); 
    }

    public void ResetAnimation()
    {
        _agent.isStopped = true;
        _animator.SetTrigger("Reset");
    }

    private void OnTriggerEnter(Collider other)
    {
        hpManager.HunterDamage(damage.GetDamage(), this.GetHunterID());
    }
}
