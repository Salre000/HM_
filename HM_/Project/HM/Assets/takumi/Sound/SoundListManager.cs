using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundListManager : MonoBehaviour
{
    [Header("�e�V�[���̃T�E���h���X�g������")]
    [SerializeField]List<SoundList> soundList;


    AudioSource _audioSource;

    public static SoundListManager instance;


    private void Start()
    {
        _audioSource = this.transform.AddComponent<AudioSource>();

        _audioSource.spatialBlend = 0;


    }

    //���Ă��̃T�E���h���Đ�����֐�
    public void PlaySound(int type ,int index) 
    {
        if (soundList[type].GetAudioSoundList().Count < index) return;
        _audioSource.clip= soundList[type].GetAudioSound(index);

        _audioSource?.Play();

    }

    //�w��̃T�E���h��Ԃ��֐�
    public AudioClip GetAudioClip(int type, int index)
    {
        if (soundList[type].GetAudioSoundList().Count < index) return null;
        return soundList[type].GetAudioSound(index); 
    }




}
