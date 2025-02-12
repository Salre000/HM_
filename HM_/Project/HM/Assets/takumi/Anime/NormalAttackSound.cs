using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackSound : MonoBehaviour
{
    [SerializeField]AudioSource audioSource;
    private void Start()
    {
        audioSource=GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

     public void BiteSound() 
    {

        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)main.monster,(int)Dragon.DragonBite));

    }
}
