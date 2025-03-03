using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerSpiderJump : AnimeBase
{

    public PlayerSpiderJump(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset) : base(Object, source, animator, animeFlagReset)
    {
        AddAnimeName("Armature|Jump");


        _camera = Camera.main.transform.gameObject;
    }

    GameObject _camera;
    Vector3 Vec;
    public override void Start()
    {
        base.Start();

        End = true;
        // �ړ��ʂƉ�]�ʂ����߂�
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

       Vec = GameObject.transform.position - _camera.transform.position;
        float cameraAngle = Mathf.Atan2(Vec.x, Vec.z);

        JumpAngle = Mathf.Atan2(_horizontal, _vertical) + cameraAngle;

        Vec = new Vector3(Mathf.Sin(JumpAngle), 0, Mathf.Cos(JumpAngle));
        stopTime();
        ResetFlag();
    }

    const float MaxTime = 0.2f;

    float time = 0;
    float JumpAngle = 0;
    //��т����Ȃ����O�t���[���̃J�E���^�[
    int FrameCount = 0;

    async UniTask stopTime()
    {
        await UniTask.DelayFrame(100);

        End = false;
    }

    public override void Action()
    {
    

        if (End) return;


        Debug.Log("Jump��");

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
