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
            // �U���A�j���[�V�����𗬂�
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
        // Ray���΂��F1����2�Ɍ�����
        Ray ray = new Ray(pos, targetPos - pos);
        RaycastHit hit;

        // Ray�̉����i�f�o�b�O�p�j
        Debug.DrawRay(pos, targetPos - pos);

        // Ray�������ɏՓ˂���������
        if (Physics.Raycast(ray, out hit))
        {
            // �Փ˂����I�u�W�F�N�g���ǂ������ꍇ
            if (hit.collider.gameObject == obstacle)
            {
                Debug.Log("1����2�͕ǂɂ���ĎՂ��Ă��܂��B");
                return false;
            }
            else
            {
                Debug.Log("1����2�͌����܂��B");
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
        // �U���̃A�j���[�V�����𗬂��B
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

   // �@ ������30�ȏ゠��Ȃ�΃i�r���b�V���ɂ��ړ�

    // �A ������30�ȉ��Ȃ�΂������ړ�

    // �B �G���U����5�񂵂Ă�����܂��̗͑͂�20�ȉ��Ȃ��x�����

    // �C ������10�ȉ��Ȃ�΍U������B

    public int attackDistance = 5;

    public float speed = 3.0f;

    // �����X�^�[�Ƃ̋���
    float distance = 0;

    // �U�����Ă�����
    int attackNum = 0;

    // �����X�^�[�̃I�u�W�F�N�g 
    private GameObject _monster;

    //�G�[�W�F���g�ƂȂ�I�u�W�F�N�g��NavMeshAgent�i�[�p 
    private NavMeshAgent agent;

    // �҂���
    float waitTime = 0;

    private Animator _animator;

    public bool attackNow = false;

    // Start is called before the first frame update
    void Start()
    {
        // �����X�^�[�̃^�O�擾
        _monster = GameObject.FindGameObjectWithTag("Player");

        // �i�r�̎擾
        agent=GetComponent<NavMeshAgent>();

        agent.speed = speed;

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animationState = _animator.GetCurrentAnimatorStateInfo(0);
        // �����X�^�[�Ǝ����̋����𑪂�
        distance =Vector3.Distance(this.transform.position,_monster.transform.position);

        // �����X�^�[�Ǝ����̋�����20�ȏ�ł���΃i�r���b�V���ɂ��ړ����s��
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
                // �U���̃A�j���[�V�����𗬂��B
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
            // ����A�j���[�V�������Đ�����
            _animator.SetBool("Walk",true );
            _animator.SetBool("WalkFinish",false );
        }
        else
        {
            // ����A�j���[�V�������~�߂�
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
