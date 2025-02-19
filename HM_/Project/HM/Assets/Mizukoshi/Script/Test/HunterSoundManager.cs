using UnityEngine;


/// <summary>
/// ハンターの音を再生する関数
/// </summary>
public class HunterSoundManager : MonoBehaviour
{

    public AudioClip clipHunmerAttackSE;
    public AudioClip clipSwordAttackSE;
    public AudioClip clipSpearAttackSE;
    public AudioClip clipArchAttackSE;
    public AudioClip clippreArchAttackSE;
    public AudioClip walkSE;

    private AudioSource audioSourceHunmerAttack; 
    private AudioSource audioSourceSwordAttack; 
    private AudioSource audioSourceSpearAttack;
    private AudioSource audioSourceArchAttack;
    private AudioSource audiopreArchAttack;
    private AudioSource audioSourceWalk;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceHunmerAttack= gameObject.AddComponent<AudioSource>();
        audioSourceSwordAttack= gameObject.AddComponent<AudioSource>();
        audioSourceSpearAttack= gameObject.AddComponent<AudioSource>();
        audiopreArchAttack= gameObject.AddComponent<AudioSource>();
        audiopreArchAttack=gameObject.AddComponent<AudioSource>();
        audioSourceWalk=gameObject.AddComponent<AudioSource>();
    }

    
    public void SoundHunmerAttack(float volume=1.0f)
    {
        PlaySound(audioSourceHunmerAttack, clipHunmerAttackSE);

        /// シーンと鳴らしたい音を選ぶ　オブジェクトプール
        /// 

        
    }

    public void SoundSwordAttack(float volume = 1.0f)
    {
        PlaySound(audioSourceSwordAttack, clipSwordAttackSE);
    }

    public void SoundSpearAttack(float volume = 1.0f)
    {
        PlaySound(audioSourceSpearAttack, clipSpearAttackSE);
    }

    public void SoundArchAttack(float volume = 1.0f)
    {
        PlaySound(audiopreArchAttack, clipArchAttackSE);
    }

    public void SoundPreArchAttack(float volume = 1.0f)
    {
        PlaySound(audiopreArchAttack, clippreArchAttackSE);
    }

    public void SoundWalk(float volume = 1.0f)
    {
        PlaySound(audioSourceWalk, walkSE);
    }


    // 音声を再生するメソッド（引数でAudioSourceを指定）
    void PlaySound(AudioSource audioSource, AudioClip clip,float valume=1.0f)
    {
        audioSource.clip = clip;
        audioSource.volume=valume;
        audioSource.Play();
    }
}
