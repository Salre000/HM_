using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �s���_��
/// 
/// �@�G��������܂ł͜p�j
/// 
/// �A�G����������2�Ԗڂɋ߂��Ƃ���Ɉړ�
/// 
/// �B�_��(cool time��5�b)
/// 
/// �C�G���߂Â��Ă����瓦���� �������͋ߋ����U��
/// 
/// �D�ȍ~�J��Ԃ�
/// 
/// 
/// 
/// </summary>

// �|��AI�̍s���_��
public class AI2Logic : MonoBehaviour
{
    // ��ʒu(�|�������čU������ʒu)
    public Vector3 []homePosition=new Vector3[4];

    // �Œ���Ƃ肽������
    const float BASEDISTANCE = 30;

    // �U���̃N�[���^�C��
    public float attackCoolTime = 5.0f;

    // �����X�^�[�̃I�u�W�F�N�g 
    private GameObject _monster;

    //�G�[�W�F���g�ƂȂ�I�u�W�F�N�g��NavMeshAgent�i�[�p 
    private NavMeshAgent agent;

    // 
    private Animator _animator;

    // 
    AnimatorStateInfo animationState;

    // �G���݂������̃t���O
    private bool _diapperFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }




    public void SetDiapperFlag()
    {
        _diapperFlag=true;
    }

    // NavmeshAgent�̍ŒZ�o�H�̋߂��ɓG�����邩�ǂ����𔻒f����֐�
    void CheckEnemyInPath()
    {

    }

    // ��Q�����ˋ��ɃZ�b�g����
    void SetObstacle()
    {

    }

}
