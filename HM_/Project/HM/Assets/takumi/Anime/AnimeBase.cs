using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class AnimeBase : MonoBehaviour
{

    protected Animator _animator;

    public Tag TagBox;

    protected List<string> _AnimeName = new List<string>(1);

    private System.Func<int> EndAnimation;

    private GameObject GameObject;
   
    public void SetEndAnimation(System.Func<int> EndAnimation) { this.EndAnimation = EndAnimation; }

    public void AddAnimeName(string Name) { _AnimeName.Add(Name); }

    protected AudioClip startClip;

    protected AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
        this.GameObject = this.gameObject;
        AudioSource=GetComponent<AudioSource>();

        if(startClip != null)AudioSource.PlayOneShot(startClip);
       // DestroyThis();
    }



    async UniTask DestroyThis()
    {


        await UniTask.WaitForSeconds(10);



        AnimeBase animeBase = this.GameObject.GetComponent<AnimeBase>();

     
    }
    float TimeCount = 0;
    // Update is called once per frame
    protected void AnimeUPDate()
    {
        TimeCount += Time.deltaTime;

        if (TimeCount < 0.5f) return;

        string NowAnime = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;



        //何かの理由でアニメーションが終了したとき
        if (!_AnimeName.Contains(NowAnime))
        {

            AnimeEnd();

        }



    }

    virtual protected void AnimeEnd()
    {
        //EndAnimation();
    }

}
