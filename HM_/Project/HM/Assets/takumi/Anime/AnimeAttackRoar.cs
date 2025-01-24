using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAttackRoar :AnimeBase
{

    GameObject Roar;
    RadialBlur radialBlur;
    Shader shader;
    GameObject mainCamera;
    void SetShader(Shader shader) {  this.shader = shader; }    

    private void Awake()
    {
        AddAnimeName("Armature|AttackRoar");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    public void SetRadialBlur(RadialBlur radial) {radialBlur = radial; }
    int FrameCount = 0;
    private void FixedUpdate()
    {
        AnimeUPDate();
    }
    void AnimeRoarStart() 
    {
        radialBlur.enabled = true;

    }


    void AnimeRoarEnd()
    {
        radialBlur.enabled = false;
    }

    //アニメーションコントローラー
    protected override void AnimeEnd()
    {
        AnimeAttackRoar animeAttackRoar=GetComponent<AnimeAttackRoar>();
        Destroy(animeAttackRoar);

    }


}
