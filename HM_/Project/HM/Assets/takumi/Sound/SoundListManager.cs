using SceneSound;
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

    float soundVolumeBGM = 0.5f;
    [SerializeField]float soundVolumeSE = 0.5f;

    private void Awake()
    {
        _audioSource = this.transform.AddComponent<AudioSource>();
        _audioSourceBGM = this.transform.AddComponent<AudioSource>();

        instance = this;

        //音量を取得




        //距離をなくす
        _audioSource.spatialBlend = 0;
        _audioSourceBGM.spatialBlend = 0;

        _audioSourceBGM.loop = true;

      
    }

    public void SetSoundVolume(int volumeSE,int volumeBGM )
    {
        soundVolumeSE = (float)volumeSE/100.0f;
        soundVolumeBGM = (float)volumeBGM/100.0f;

        _audioSourceBGM.volume = soundVolumeBGM;
        _audioSource.volume = soundVolumeSE;
    }

    public float GetSoundVolume() {return soundVolumeSE; }

    //していのサウンドを再生する関数
    public void PlaySound( int index, int type=0)
    {
        

        if (soundList[type].GetAudioSoundList().Count < index) return;
        

        _audioSource?.PlayOneShot(soundList[type].GetAudioSound(index));

    }

    //指定のサウンドを返す関数
    public AudioClip GetAudioClip(int index, int type=0)
    {
        if (soundList[type].GetAudioSoundList().Count < index) return null;
        return soundList[type].GetAudioSound(index);
    }


    public void PlayBGM(int index, int type = 0)
    {

        _audioSourceBGM.clip = GetAudioClip(index, type);

        _audioSourceBGM.Play();



    }
}
