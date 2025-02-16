using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerSpiderJump : AnimeBase
{

    public PlayerSpiderJump(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset) : base(Object, source, animator, animeFlagReset)
    {
        AddAnimeName("Armature|Jump");


    }

    Vector3 Vec;
    public override void Start()
    {
        base.Start();
    

        // 移動量と回転量を求める
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        JumpAngle = Mathf.Atan2(_horizontal, _vertical) + this.GameObject.transform.eulerAngles.y * 3.14f / 180;
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

    public override void Action()
    {
    

        if (End) return;


        AnimeUPDate();



        this.GameObject.transform.position += Vec / 60;


    }

    bool End = true;
    public override void AnimeEvent()
    {
        base.AnimeEvent();

        End = false;
    }


    override protected void AnimeEnd()
    {
        base.AnimeEnd();
        useFlag = false;
    }


}
