using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeBase : MonoBehaviour
{

    protected Animator _animator;

    public Tag TagBox;

    protected string _AnimeName="";
    // Start is called before the first frame update
    void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    protected void AnimeUPDate() 
    {

        string NowAnime = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;



        //何かの理由でアニメーションが終了したとき
        if (NowAnime != _AnimeName)
        {

            AnimeEnd();

        }



    }

    virtual protected void AnimeEnd()
    {
        

    }
}
