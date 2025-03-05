using SceneSound;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragonSound :PlayerSoundBase
{
    public override void NormalDown()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip( (int)Dragon.DragonNormalDown, (int)Main.Monster),SoundListManager.instance.GetSoundVolume());
    }
    public override void NormalDownVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Dragon.DragonNormalDownVoice, (int)Main.Monster), SoundListManager.instance.GetSoundVolume());
    }
    public override void HardDown()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Dragon.DragonHardDown, (int)Main.Monster), SoundListManager.instance.GetSoundVolume());
    }
    public override void HardDownVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Dragon.DragonHardDownVoice, (int)Main.Monster), SoundListManager.instance.GetSoundVolume());
    }

    public override void MoveSound()
    {


        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Dragon.DragonMove, (int)Main.Monster), SoundListManager.instance.GetSoundVolume());
    }

    public override void MoveVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Dragon.DragonMoveVoice, (int)Main.Monster) ,0.3f*SoundListManager.instance.GetSoundVolume());
    }
}
