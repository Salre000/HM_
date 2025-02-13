using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundBase : MonoBehaviour
{
    protected AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public virtual void NormalDown()
    {

    }
    public virtual void NormalDownVoice()
    {

    }
    public virtual void HardDown()
    {

    }
    public virtual void HardDownVoice()
    {

    }
    public virtual void MoveSound() { }
    public virtual void MoveVoice() { }
}
