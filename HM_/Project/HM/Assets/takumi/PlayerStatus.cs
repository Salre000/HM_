using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[���X�e�[�^�X���Ǘ�����N���X
public class PlayerStatus : MonoBehaviour
{
    //���݂̃v���C���[��HP�̕ϐ�
    [SerializeField] float HP = 0.0f;

    //�v���C���[�̍ő�HP�̕ϐ�
   �@float MAXHP = 0.0f;

    //�v���C���[�̑��x
    [SerializeField] float Speed = 0.0f;

    //�v���C���[�̉�]�̑��x�̕ϐ�
    [SerializeField] float RotateSpeed = 0.0f;

    //�ő�HP��Ԃ��֐�
    public float GetMaxHP() { return MAXHP; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
