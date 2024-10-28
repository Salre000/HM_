using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[���X�e�[�^�X���Ǘ�����N���X
public class PlayerStatus : MonoBehaviour
{
    //���݂̃v���C���[��HP�̕ϐ�
    [SerializeField] float HP = 0.0f;

    //�v���C���[�̍ő�HP�̒萔
   �@const float MAXHP = 100.0f;

    //�v���C���[�̑��x
    [SerializeField] float Speed = 0.02f;

    //�v���C���[�̉�]�̑��x�̕ϐ�
     float RotateSpeed = 0.5f;

    //�ő�HP��Ԃ��֐�
    public float GetMaxHP() { return MAXHP; }

    public float GetHp() { return HP; }

    public void Damage(float Damage) { HP -= Damage;if (HP <= 0) { _anime.SetDieFlag(true); } }

    //�v���C���[�̃X�s�[�h��Ԃ��֐��֐�
    public float GetSpeed() { return Speed; }

    public float GetRotateSpeed() { return RotateSpeed; }

    private PlayerAnime _anime;

    public void Start()
    {
        _anime=this.gameObject.GetComponent<PlayerAnime>();

        HP=MAXHP;
    }

}
