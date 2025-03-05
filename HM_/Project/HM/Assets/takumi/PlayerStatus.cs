using Den.Tools;
using JetBrains.Annotations;
using SceneSound;
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

    public static bool isLife = true;
    public static PlayerStatus Instance { get; private set; }

    [HideInInspector] public OptionDataMain data;
    public enum Condition
    {
        Normal,//�ʏ���
        Stun,//�C����
        Anger,//�{����
        Fatigue//��J���
    }
    public Condition GetNowCondition() { return _nowCondition; }

    [SerializeField] int AngerGage = 0;
    const int MaxAngerGage = 1000;
    public void AddAngerGage(int Add)
    {
        if (_nowCondition != Condition.Normal) return;
        //UI�̃Q�[�W��ύX���鏈��������
        AngerGage += Add;
        if (MaxAngerGage > AngerGage) return;
        AngerGage = MaxAngerGage;
        ChengeCondition(Condition.Anger);

        _anime.SetSpped(1.3f);
    }

    public void SbuAngerGage(int Sbu)
    {
        if (_nowCondition != Condition.Anger) return;
        AngerGage -= Sbu;
        if (AngerGage > 0) return;
        AngerGage = 0;
        ChengeCondition(Condition.Fatigue);
        _anime.SetSpped(0.8f);


    }

    [SerializeField] int FatigueGage = 0;
    int MAXFatigueGage = 3600;
    //�m�[�}�����[�h�ɕύX����֐�
    private void ChengeNomale()
    {
        if (_nowCondition != Condition.Fatigue) return;
        FatigueGage++;

        if (FatigueGage < MAXFatigueGage) return;

        _anime.SetSpped(1.0f);
        ChengeCondition(Condition.Normal);
        FatigueGage = 0;

    }


    [SerializeField]private Condition _nowCondition;
    private Condition _lostCondition;

    System.Action<Condition> ChengeConditionMode; 

    public void SetCallBackCondition(System.Action<Condition>action) { ChengeConditionMode = action; }

    private void ChengeCondition(Condition condition)
    {
        if (_nowCondition == condition) return;
        _lostCondition = _nowCondition;
        _nowCondition = condition;
        ChengeConditionMode(condition);

    }

    public void SetData(OptionDataMain data) { this.data=data; }

    public void Start()
    {
        data = JsonDataModule.Load<OptionDataMain>(Application.streamingAssetsPath + "/OptionMain.json");

        Instance = this;
        isLife = true;
        _anime = this.gameObject.GetComponent<PlayerAnime>();
        _audioSource = this.transform.AddComponent<AudioSource>();
        _audioSource.loop = false;
        //���ʂƂ��𒲐�����

        HP = MAXHP;

        _anime.SetCallback(StartStu, condition =>
        {

            ChengeCondition(condition);



        });

    }

    private Condition StartStu() 
    {
        Condition condition=_nowCondition;

        ChengeCondition(Condition.Stun);

        return condition;


    }
    public void FixedUpdate()
    {
        AddAngerGage(1);
        SbuAngerGage(1);
        ChengeNomale();
    }

    public void NormalVoice() 
    {
        _audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip(3, (int)Main.Monster), SoundListManager.instance.GetSoundVolume());


    }

}
