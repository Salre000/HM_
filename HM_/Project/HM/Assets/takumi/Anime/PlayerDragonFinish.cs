using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDragonFinish : AnimeBase
{
    GameObject camera;
    PlayerStatus _status;

    bool finishActionFlag = false;

    System.Func<GameObject> GetHunterObject;
    float z = 1.2f;
    float y = 2.9f;

    public PlayerDragonFinish(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset, System.Func<GameObject> setGetHunterObject) : base(Object, source, animator, animeFlagReset)
    {
        camera = Camera.main.gameObject;
        GetHunterObject = setGetHunterObject;
    }

    public override void Start()
    {
        finishActionFlag = false;
        _status = GameObject.GetComponent<PlayerStatus>();
        targetHunter = GetHunterObject();
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
    bool startFlag=false;
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
            HunterUpMove().Forget();

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
        pos.y += targetHunter.transform.position.y+0.3f;
        pos.x = targetHunter.transform.position.x + Mathf.Sin(GameObject.transform.eulerAngles.y*Mathf.Deg2Rad) * _range + 0.2f;
        pos.z = targetHunter.transform.position.z + Mathf.Cos(GameObject.transform.eulerAngles.y * Mathf.Deg2Rad) * _range+0.2f;

        camera.transform.position = pos;



        camera.transform.LookAt(targetHunter.transform.position);




    }
    Rigidbody rigidbody;
    CameraManager cameraManager;
    private async UniTask HunterUpMove() 
    {
        _range = 1.0f;
        cameraManager=camera.GetComponent<CameraManager>();

        cameraManager.SetCameraUseFlag(false);

        rigidbody = targetHunter.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        Vector3 startPos= targetHunter.transform.position;

        Vector3 endPos= targetHunter.transform.position+new Vector3(0,0.5f,0);

        Vector3 MoveVex;

        MoveVex = startPos - endPos;

        await UniTask.DelayFrame(20);

        for (int i = 0; i < 30; i++) 
        {
            _range -= 0.02f;

            // オブジェクトの移動
            targetHunter.transform.position -= MoveVex/30;
            Debug.Log(targetHunter.transform.position + "GGG");
            await UniTask.DelayFrame(1);
        }
        startPos = targetHunter.transform.position;
        endPos = GameObject.transform.position + new Vector3(Mathf.Sin(GameObject.transform.eulerAngles.y*Mathf.Deg2Rad)*0.1f, y/10, Mathf.Cos(GameObject.transform.eulerAngles.y * Mathf.Deg2Rad) * 0.1f);
        
        MoveVex = startPos - endPos;
        new GameObject().transform.position = endPos;

        for (int i = 0; i < 20; i++) 
        {
            targetHunter.transform.position -= MoveVex/20;
            Debug.Log(targetHunter.transform.position + "GGG");

            await UniTask.DelayFrame(1);


        }


    }



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
