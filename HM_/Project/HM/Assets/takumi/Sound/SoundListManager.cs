using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundListManager : MonoBehaviour
{
    [Header("各シーンのサウンドリストを入れる")]
    [SerializeField] List<SoundList> soundList;


    AudioSource _audioSource;
    AudioSource _audioSourceBGM;

    public static SoundListManager instance;
    private void Start()
    {
        _audioSource = this.transform.AddComponent<AudioSource>();
        _audioSourceBGM = this.transform.AddComponent<AudioSource>();

        instance = this;
        //距離をなくす
        _audioSource.spatialBlend = 0;
        _audioSourceBGM.spatialBlend = 0;

    }

    //していのサウンドを再生する関数
    public void PlaySound(int type, int index)
    {
        if (soundList[type].GetAudioSoundList().Count < index) return;
        

        _audioSource?.PlayOneShot(soundList[type].GetAudioSound(index));

    }

    //指定のサウンドを返す関数
    public AudioClip GetAudioClip(int type, int index)
    {
        if (soundList[type].GetAudioSoundList().Count < index) return null;
        return soundList[type].GetAudioSound(index);
    }






}
