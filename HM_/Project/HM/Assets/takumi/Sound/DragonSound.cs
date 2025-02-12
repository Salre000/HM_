using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSound :PlayerSoundBase
{
    public override void NormalDown()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)main.monster, (int)Dragon.DragonNormalDown));
    }
    public override void NormalDownVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)main.monster, (int)Dragon.DragonNormalDownVoice));
    }
    public override void HardDown()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)main.monster, (int)Dragon.DragonHardDown));
    }
    public override void HardDownVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)main.monster, (int)Dragon.DragonHardDownVoice));
    }



}
