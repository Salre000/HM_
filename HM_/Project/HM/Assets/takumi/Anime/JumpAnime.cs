using SceneSound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class JumpAnime : AnimeBase
{

    const float Damages=30.0f;

    const float MaxTime = 0.3f;

    float time = 0;
    float JumpAngle = 0;


    ////�����蔻��𐶐�����I�u�W�F�N�g
    //GameObject [] HitGameObject=new GameObject[3];

    //SphereCollider[] Sphere = new SphereCollider[3];

    //Damage []damage=new Damage[3];

    private void Awake()
    {

        AddAnimeName("Armature|jump");
        PlayerAttackDragon playerAttack = GetComponent<PlayerAttackDragon>();

                // �ړ��ʂƉ�]�ʂ����߂�
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        JumpAngle=Mathf.Atan2(_horizontal, _vertical)+this.transform.eulerAngles.y*3.14f/180;

        startClip = SoundListManager.instance.GetAudioClip((int)main.monster, (int)Dragon.DragonJumpStart);


    }

    //��т����Ȃ����O�t���[���̃J�E���^�[
    int FrameCount = 0;
    void FixedUpdate()
    {
        time += Time.deltaTime;
        FrameCount++;
        if (FrameCount < 15) return;

        Vector3 Vec= new Vector3(Mathf.Sin(JumpAngle), 0.75f, Mathf.Cos(JumpAngle))/20.0f;


        if(FrameCount<=30)
        this.transform.position += Vec;

        AnimeUPDate();

    }

    override protected void AnimeEnd()
    {
        base.AnimeEnd();



        JumpAnime jumpAnime =this.gameObject.GetComponent<JumpAnime>();


        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            this.audioSource.PlayOneShot(SoundListManager.instance.GetAudioClip((int)main.monster, (int)Dragon.DragonJumpEnd));


        Destroy(jumpAnime);

    }
}
