using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : ScriptableObject
{
    [Header("各シーンに必要なサウンドデータの格納先")]
    [SerializeField]List<AudioClip>soundList = new List<AudioClip>();

    public AudioClip GetAudioSound(int index) { return soundList[index]; }

    public List<AudioClip>GetAudioSoundList() { return soundList; }

}
