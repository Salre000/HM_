using UnityEngine;

public class PlayerSpiderJump : AnimeBase
{
    private void Awake()
    {
        AddAnimeName("Armature|Jump");

        // 移動量と回転量を求める
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        JumpAngle = Mathf.Atan2(_horizontal, _vertical) + this.transform.eulerAngles.y * 3.14f / 180;
        Debug.Log(JumpAngle);
    }

    const float MaxTime = 0.2f;

    float time = 0;
    float JumpAngle = 0;
    //飛びたくない事前フレームのカウンター
    int FrameCount = 0;


    void FixedUpdate()
    {
        AnimeUPDate();


        if (End) return;

        Vector3 Vec = new Vector3(Mathf.Sin(JumpAngle), 0, Mathf.Cos(JumpAngle));

        this.gameObject.transform.position += Vec / 6;


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