using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunter_AI_One : MonoBehaviour
{

    public GameObject stage;
    public GameObject player;
    public Vector3[] targetPos = new Vector3[4];
    public float attackDistance;
    public float speed = 2.0f;
    private float _distance;
    private bool _look;
    private bool _attackNow = false;
    NavMeshAgent agent;
    int _destinationNum = 0;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targetPos[0];
        _look = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;

        AnimatorStateInfo animationState = _animator.GetCurrentAnimatorStateInfo(0);

        if (!_look)
        {
            Search();
        }

        if (GetHitRayNone(this.transform.position,player.transform.position,stage))
        {
            _look = true;
            agent.destination = player.transform.position;
        }

        if (PlayerToDistance() <= attackDistance&&agent.isStopped&&!_attackNow)
        {
            agent.isStopped = true;
            // 攻撃アニメーションを流す
            Attack();
        }
        if (PlayerToDistance() > attackDistance)
        {
            Run();
            agent.isStopped = false;
        }

        AnimationFinishInform(animationState);


    }

    bool GetHitRayNone(Vector3 pos, Vector3 targetPos, GameObject obstacle)
    {
        // Rayを飛ばす：1から2に向けて
        Ray ray = new Ray(pos, targetPos - pos);
        RaycastHit hit;

        // Rayの可視化（デバッグ用）
        Debug.DrawRay(pos, targetPos - pos);

        // Rayが何かに衝突したか判定
        if (Physics.Raycast(ray, out hit))
        {
            // 衝突したオブジェクトが壁だった場合
            if (hit.collider.gameObject == obstacle)
            {
                Debug.Log("1から2は壁によって遮られています。");
                return false;
            }
            else
            {
                Debug.Log("1から2は見えます。");
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    void AnimationFinishInform(AnimatorStateInfo inform)
    {
        //animationState.normalizedTime >= 0.75f && animationState.IsName("ataka1")
        if (inform.normalizedTime >= 0.75f && inform.IsName("ataka1"))
        {
            AttackAnimationEnd();
        }

        if (agent.isStopped)
        {
            RunAnimationEnd();
        }
    }

    public void AttackAnimationEnd()
    {
        _animator.SetBool("Attack", false);
        _animator.SetBool("AttackFinish", true);
        _attackNow = false;
    }

    public void RunAnimationEnd()
    {
        _animator.SetBool("Walk", false);
        _animator.SetBool("WalkFinish", true);
    }

    public void Run()
    {
        _animator.SetBool("Walk", true);
        _animator.SetBool("WalkFinish", false);
    }

    void Walk()
    {

    }

    void Attack()
    {
        // 攻撃のアニメーションを流す。
        _animator.SetBool("Attack", true);
        _animator.SetBool("AttackFinish", false);
        _attackNow = true;
    }


    float PlayerToDistance()
    {
        float distance = 0;
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        return distance;
    }

    void Search()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 1f)
        {
            _destinationNum++;
            agent.destination = targetPos[_destinationNum];
        }
    }
}
/*

   // ① 距離が30以上あるならばナビメッシュによる移動

    // ② 距離が30以下ならばゆっくり移動

    // ③ 敵が攻撃を5回してきたらまたは体力が20以下なら一度離れる

    // ④ 距離が10以下ならば攻撃する。

    public int attackDistance = 5;

    public float speed = 3.0f;

    // モンスターとの距離
    float distance = 0;

    // 攻撃してきた回数
    int attackNum = 0;

    // モンスターのオブジェクト 
    private GameObject _monster;

    //エージェントとなるオブジェクトのNavMeshAgent格納用 
    private NavMeshAgent agent;

    // 待つ時間
    float waitTime = 0;

    private Animator _animator;

    public bool attackNow = false;

    // Start is called before the first frame update
    void Start()
    {
        // モンスターのタグ取得
        _monster = GameObject.FindGameObjectWithTag("Player");

        // ナビの取得
        agent=GetComponent<NavMeshAgent>();

        agent.speed = speed;

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animationState = _animator.GetCurrentAnimatorStateInfo(0);
        // モンスターと自分の距離を測る
        distance =Vector3.Distance(this.transform.position,_monster.transform.position);

        // モンスターと自分の距離が20以上であればナビメッシュによる移動を行う
        if (distance>attackDistance)
        {
            agent.isStopped=false;
            agent.destination = _monster.transform.position;
            waitTime = 0;
        }
        else
        {
            agent.isStopped=true;
            waitTime = 1;
        }

        if (waitTime >= 1)
        {

            if (agent.isStopped)
            {
                // 攻撃のアニメーションを流す。
                _animator.SetBool("Attack", true);
                _animator.SetBool("AttackFinish", false);
            }
        }
        if (animationState.normalizedTime >= 0.01f && animationState.IsName("ataka1"))
        {
            attackNow = true;
        }

        if (animationState.normalizedTime >= 0.75f && animationState.IsName("ataka1"))
        {
            AttackAnimationEnd();
            attackNow = false;
        }
        if (!agent.isStopped)
        {
            // 走るアニメーションを再生する
            _animator.SetBool("Walk",true );
            _animator.SetBool("WalkFinish",false );
        }
        else
        {
            // 走るアニメーションを止める
            _animator.SetBool("Walk", false);
            _animator.SetBool("WalkFinish", true);
        }
    }

    public void AttackAnimationEnd()
    {
        _animator.SetBool("Attack", false);
        _animator.SetBool("AttackFinish",true );
    }

*/
