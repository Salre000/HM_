using SceneSound;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] PlayerHitCheck[] hItTest = new PlayerHitCheck[30];
    [SerializeField] GameObject[] sameObject = new GameObject[30];

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();

        hItTest = GameObject.FindObjectsByType<PlayerHitCheck>(FindObjectsSortMode.None);
    }
}
//}
//    int i = 0;
    // Update is called once per frame
//    void Update()
//    {

//        if(Input.GetKeyDown(KeyCode.LeftArrow))audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.Monster, 0));
//        if(Input.GetKeyDown(KeyCode.RightArrow))audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.Monster, 1));
//        if(Input.GetKeyDown(KeyCode.UpArrow))audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.Monster, 3));
//        if(Input.GetKeyDown(KeyCode.DownArrow))audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.Monster, 2));

//        //audioSource.clip=SoundListManager.instance.GetAudioClip((int)Main.Monster,i)
//    }
//}
