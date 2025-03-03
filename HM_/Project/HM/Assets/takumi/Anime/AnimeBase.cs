using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class AnimeBase
{

    public static bool useFlag = false; 
    protected System.Action<bool> _AnimeFlagReset;

    public AnimeBase(GameObject Object,AudioSource source,Animator animator,System.Action<bool> animeFlagReset) 
    {

        audioSource = source;

        _animator = animator;

        this.GameObject = Object;

        _AnimeFlagReset = animeFlagReset;



    }

    protected async UniTask ResetFlag() 
    {


        await UniTask.DelayFrame(10);

        _AnimeFlagReset(false);
    } 


    protected Animator _animator;

    public Tag TagBox;

    protected List<string> _AnimeName = new List<string>(1);

    private System.Func<int> EndAnimation;

    protected GameObject GameObject;
   
    public void SetEndAnimation(System.Func<int> EndAnimation) { this.EndAnimation = EndAnimation; }

    public void AddAnimeName(string Name) { _AnimeName.Add(Name); }

    protected AudioClip startClip;

    protected AudioSource audioSource;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if(startClip != null)audioSource.PlayOneShot(startClip);
    }
   

    public virtual void Action() { }

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

    virtual public void AnimeEvent() { }

    virtual protected void AnimeEnd()
    {
        TimeCount = 0;
    }

}
