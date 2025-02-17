using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSound :PlayerSoundBase
{
    public override void NormalDown()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.monster, (int)Dragon.DragonNormalDown));
    }
    public override void NormalDownVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.monster, (int)Dragon.DragonNormalDownVoice));
    }
    public override void HardDown()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.monster, (int)Dragon.DragonHardDown));
    }
    public override void HardDownVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.monster, (int)Dragon.DragonHardDownVoice));
    }

    public override void MoveSound()
    {
    audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int) Main.monster, (int)Dragon.DragonMove));
    }

    public override void MoveVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.monster, (int)Dragon.DragonMoveVoice) ,0.3f);
    }
}
