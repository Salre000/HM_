using Den.Tools;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

//�v���C���[���X�e�[�^�X���Ǘ�����N���X
public class PlayerStatus : MonoBehaviour
{
    //���݂̃v���C���[��HP�̕ϐ�
    [SerializeField] float HP = 0.0f;

    //�v���C���[�̍ő�HP�̒萔
   �@const float MAXHP = 100.0f;

    //�v���C���[�̑��x
    [SerializeField] float Speed = 0.0002f;

    //�v���C���[�̉�]�̑��x�̕ϐ�
    float RotateSpeed = 0.5f;

    //�ő�HP��Ԃ��֐�
    public float GetMaxHP() { return MAXHP; }

    public float GetHP() { return HP; }

    public void Damage(float Damage) { HP -= Damage; if (HP <= 0) { _anime.SetDieFlag(true); } }

    //�v���C���[�̃X�s�[�h��Ԃ��֐��֐�
    public float GetSpeed() { return Speed; }

    public float GetRotateSpeed() { return RotateSpeed; }

    private PlayerAnime _anime;

    private AudioSource _audioSource;

    public enum Condition
    {
        Normal,//�ʏ���
        Stun,//�C����
        Anger,//�{����
        Fatigue//��J���
    }
    public Condition GetNowCondition() { return _nowCondition; }

    [SerializeField] int StunGage = 0;
    const int MaxStunGage = 1000;
    public void AddStunGage(int Add)
    {
        if (_nowCondition != Condition.Normal) return;
        StunGage += Add;
        if (MaxStunGage > StunGage) return;
        StunGage = MaxStunGage;
        ChengeCondition(Condition.Stun);

        _anime.SetSpped(1.3f);
    }

    public void SbuStunGage(int Sbu)
    {
        if (_nowCondition != Condition.Stun) return;
        StunGage -= Sbu;
        if (StunGage > 0) return;
        StunGage = 0;
        ChengeCondition(Condition.Fatigue);
        _anime.SetSpped(0.8f);


    }

    [SerializeField] int StunCount = 0;
    int MAXStunCount = 3600;
    //�m�[�}�����[�h�ɕύX����֐�
    private void ChengeNomale()
    {
        if (_nowCondition != Condition.Fatigue) return;
        StunCount++;

        if (StunCount < MAXStunCount) return;

        _anime.SetSpped(1.0f);
        ChengeCondition(Condition.Normal);
        StunCount = 0;

    }


    private Condition _nowCondition;
    private Condition _lostCondition;

    private void ChengeCondition(Condition condition)
    {
        if (_nowCondition == condition) return;
        _lostCondition = _nowCondition;
        _nowCondition = condition;


    }

    public void Start()
    {
        _anime = this.gameObject.GetComponent<PlayerAnime>();
        _audioSource = this.transform.AddComponent<AudioSource>();
        _audioSource.loop = false;
        //���ʂƂ��𒲐�����

        HP = MAXHP;
    }
    public void FixedUpdate()
    {
        AddStunGage(1);
        SbuStunGage(1);
        ChengeNomale();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        _audioSource.clip = clip;
        _audioSource.Play();


    }

}
