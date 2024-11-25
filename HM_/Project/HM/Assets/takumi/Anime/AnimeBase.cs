using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    float TimeCount =0;
    // Update is called once per frame
    protected void AnimeUPDate() 
    {
        TimeCount += Time.deltaTime;

        if (TimeCount < 1.5f) return;

        string NowAnime = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;



        //�����̗��R�ŃA�j���[�V�������I�������Ƃ�
        if (NowAnime != _AnimeName)
        {

            AnimeEnd();

        }



    }

    virtual protected void AnimeEnd()
    {
        
    }

    //�G�ɍU�������Ă��Ƃ��ɂ����蔻����폜���鉼�z�֐�
    virtual protected void DestroyHitObject() 
    {

    }

    private void OnTriggerStay(Collider other)
    {


        if (other.transform.tag == TagBox.GetEnemyTag()) 
        {

        DestroyHitObject();
            this.tag = "Player";

        }





    }


}
