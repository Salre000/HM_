using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerSpiderJump : AnimeBase
{
    Vector3 Vec;
    private void Awake()
    {
        AddAnimeName("Armature|Jump");

        // 移動量と回転量を求める
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        JumpAngle = Mathf.Atan2(_horizontal, _vertical) + this.transform.eulerAngles.y * 3.14f / 180;
       Vec = new Vector3(Mathf.Sin(JumpAngle), 0, Mathf.Cos(JumpAngle));
        stopTime();
    }

    const float MaxTime = 0.2f;

    float time = 0;
    float JumpAngle = 0;
    //飛びたくない事前フレームのカウンター
    int FrameCount = 0;

    async UniTask stopTime()
    {
        await UniTask.DelayFrame(100);

        End = false;    
    }

    void FixedUpdate()
    {

        if (End) return;


        AnimeUPDate();



        this.gameObject.transform.position += Vec / 60;


    }

    bool End = true;   
    void JumpStart() 
    {
        End = false;
    }


    override protected void AnimeEnd()
    {
        PlayerSpiderJump jumpAnime = this.gameObject.GetComponent<PlayerSpiderJump>();

        Destroy(jumpAnime);
    }


}