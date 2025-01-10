using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackRoar :AnimeBase
{

    GameObject Roar;
    RadialBlur radialBlur;
    Shader shader;
    void SetShader(Shader shader) {  this.shader = shader; }    

    private void Awake()
    {
        _AnimeName = "Armature|AttackRoar";
    }
    int FrameCount = 0;
    private void FixedUpdate()
    {
        FrameCount++;
        AnimeUPDate();
        if (FrameCount > 20) 
        {
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

            radialBlur=mainCamera.AddComponent<RadialBlur>();
            radialBlur._shader = shader;
            Roar = new GameObject();
            

        }
    }


    //アニメーションコントローラー
    protected override void AnimeEnd()
    {
        Destroy(Roar);
        Destroy(radialBlur);

    }


}
