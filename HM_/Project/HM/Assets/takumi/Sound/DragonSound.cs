using SceneSound;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragonSound :PlayerSoundBase
{
    [SerializeField]TextMeshProUGUI textMeshProUGUI;
    public override void NormalDown()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip( (int)Dragon.DragonNormalDown, (int)Main.Monster));
    }
    public override void NormalDownVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Dragon.DragonNormalDownVoice, (int)Main.Monster));
    }
    public override void HardDown()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Main.Monster, (int)Dragon.DragonHardDown));
    }
    public override void HardDownVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Dragon.DragonHardDownVoice, (int)Main.Monster));
    }

    public override void MoveSound()
    {
        textMeshProUGUI.text = audioSource.ToString();


        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Dragon.DragonMove, (int)Main.Monster));
    }

    public override void MoveVoice()
    {
        audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)Dragon.DragonMoveVoice, (int)Main.Monster) ,0.3f);
    }
}
