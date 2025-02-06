using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : ScriptableObject
{
    [Header("�e�V�[���ɕK�v�ȃT�E���h�f�[�^�̊i�[��")]
    [SerializeField]List<AudioClip>soundList = new List<AudioClip>();

    public AudioClip GetAudioSound(int index) { return soundList[index]; }

    public List<AudioClip>GetAudioSoundList() { return soundList; }

}
