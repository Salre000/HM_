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
    }

    GameObject targetHunter;


    public override void Action()
    {

        if (!finishActionFlag) FinishMove();
        else FinishAction();


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
        pos.y += camera.transform.position.y;
        pos.x = camera.transform.position.x + Mathf.Sin(GameObject.transform.eulerAngles.y*Mathf.Deg2Rad) * _range;
        pos.z = camera.transform.position.z + Mathf.Cos(GameObject.transform.eulerAngles.y * Mathf.Deg2Rad) * _range;

        camera.transform.position = pos;



        camera.transform.LookAt(GameObject.transform.position + Vector3.up / 10);



    }
    private async UniTask HunterUpMove() 
    {
        _range = 1.0f;
        CameraManager cameraManager=camera.GetComponent<CameraManager>();

        cameraManager.SetCameraUseFlag(false);

        Rigidbody rigidbody = targetHunter.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        Vector3 startPos= targetHunter.transform.position;

        Vector3 endPos= targetHunter.transform.position+new Vector3(0,0.5f,0);

        await UniTask.DelayFrame(20);

        for (int i = 0; i < 30; i++) 
        {
            _range -= 0.002f;

            // オブジェクトの移動
            targetHunter.transform.position = Vector3.Lerp(startPos, endPos, (Time.time * 1.0f) / Vector3.Distance(startPos, endPos));

            await UniTask.DelayFrame(1);
        }
        startPos = targetHunter.transform.position;
        endPos= targetHunter.transform.position + new Vector3(0, -0.3f, 0);
        for (int i = 0; i < 20; i++) 
        {
            _range -= 0.002f;

            targetHunter.transform.position = Vector3.Lerp(startPos, endPos, (Time.time * 1.0f) / Vector3.Distance(startPos, endPos));

            await UniTask.DelayFrame(1);


        }




        rigidbody.useGravity = true;
        cameraManager.SetCameraUseFlag(true);
        base.AnimeEnd();


    }


}
