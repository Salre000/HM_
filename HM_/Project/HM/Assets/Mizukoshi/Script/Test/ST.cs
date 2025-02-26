using SceneSound;
using UnityEngine;

public class ST : MonoBehaviour
{

    AudioSource audioSource;

    float time = 0;
    
    public SoundListManager soundListManager;

    private void Start()
    {
        //source = GetComponent<AudioSource>();
        soundListManager.PlaySound((int)HunterSE.WalkSE, (int)Main.Hunter);
        //SoundListManager.instance.GetAudioClip((int)HunterSE.WalkSE, (int)Main.Hunter);
        audioSource = this.gameObject.AddComponent<AudioSource>();

        //// 
        //source.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.WalkSE, (int)Main.Hunter));

    }

    public void Update()
    {
        //time += Time.deltaTime;
        //if (time >= 3.0f)
        //{
        //    time = 0;
        //    audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)HunterSE.SpearSE, (int)Main.Hunter));
        //}
       
    }
}
