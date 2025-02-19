using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST : MonoBehaviour
{

    AudioSource source;
    
    public SoundListManager soundListManager;

    private void Start()
    {
        //source = GetComponent<AudioSource>();
        soundListManager.PlaySound((int)HunterSE.WalkSE, (int)Main.Hunter);
        //SoundListManager.instance.GetAudioClip((int)HunterSE.WalkSE, (int)Main.Hunter);

        //// 
        //source.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.WalkSE, (int)Main.Hunter));

    }
}
