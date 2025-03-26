using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpiderFinish : AnimeBase
{
    GameObject camera;
    PlayerStatus _status;

    bool finishActionFlag = false;

    System.Func<GameObject> GetHunterObject;
    private GameObject capObject;
    private GameObject hunterParentObject;

    float z = 1.2f;
    float y = 2.9f;

    public PlayerSpiderFinish(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset, System.Func<GameObject> setGetHunterObject,GameObject setCapObject) : base(Object, source, animator, animeFlagReset)
    {
        camera = Camera.main.gameObject;
        GetHunterObject = setGetHunterObject;
        capObject = setCapObject;
    }

    public override void Start()
    {
        finishActionFlag = false;
        _status = GameObject.GetComponent<PlayerStatus>();
        targetHunter = GetHunterObject();

        hunterParentObject = targetHunter.transform.parent != null ? targetHunter.transform.parent.gameObject : null;

        targetHunter.transform.parent = capObject.transform;

        Hunter_AI _targetHunter = targetHunter.GetComponent<Hunter_AI>();
        if (_targetHunter == null) return;

        //ハンターを怯み状態に変更する
        _targetHunter.StartRestraining();


        targetHunter.transform.localPosition = Vector3.zero;



        GameObject.transform.LookAt(targetHunter.transform);

        Vector3 angle = GameObject.transform.eulerAngles;

        angle.x = 0;
        angle.z = 0;

        GameObject.transform.eulerAngles = angle;
        HPManager.instance.SetMonsterUseFlag(false);


    }

    GameObject targetHunter;


    public override void Action()
    {

        if (!finishActionFlag) FinishMove();
        else FinishAction();


    }
    bool startFlag = false;
    public override void AnimeEvent()
    {
        rigidbody.useGravity = true;

        HitEffectManager.instance.HitEffectBloodShow(targetHunter.transform.position);
        DaleyTask().Forget();



    }
    private float actionRange = 0.2f;
    void FinishMove()
    {
        if (actionRange > Vector3.Distance(GameObject.transform.position, targetHunter.transform.position))
        {
            finishActionFlag = true;
            _AnimeFlagReset(false);

            return;
        }

        Vector3 pos = GameObject.transform.position;

        //プレイヤーの移動
        pos.x += Mathf.Sin(GameObject.transform.eulerAngles.y * Mathf.Deg2Rad) * (_status.GetSpeed());
        pos.z += Mathf.Cos(GameObject.transform.eulerAngles.y * Mathf.Deg2Rad) * (_status.GetSpeed());
        GameObject.transform.position = pos;
    }
    float _cameraPositionAngle = 0;
    float _range = 0;

    void FinishAction()
    {


        Vector3 pos;

        //角度を使いカメラの座標を変更
        pos.y = ((_range / 3) * 2);
        pos.y += targetHunter.transform.position.y + 0.3f;
        pos.x = targetHunter.transform.position.x + Mathf.Sin(GameObject.transform.eulerAngles.y * Mathf.Deg2Rad) * _range + 0.2f;
        pos.z = targetHunter.transform.position.z + Mathf.Cos(GameObject.transform.eulerAngles.y * Mathf.Deg2Rad) * _range + 0.2f;

        camera.transform.position = pos;



        camera.transform.LookAt(targetHunter.transform.position);




    }
    Rigidbody rigidbody;
    CameraManager cameraManager;



    private async UniTask DaleyTask()
    {

        await UniTask.DelayFrame(15);

        cameraManager.SetCameraUseFlag(true);
        HunterManager hunterManager = UnityEngine.GameObject.Find("GameManager").GetComponent<HunterManager>();

        hunterManager.Respawn(targetHunter.GetComponent<Hunter_ID>().GetHunterID());
        HPManager.instance.SetMonsterUseFlag(true);

        AnimeEnd();
    }

}