using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        pos.x += Mathf.Sin(GameObject.transform.eulerAngles.y) * (_status.GetSpeed());
        pos.z += Mathf.Cos(GameObject.transform.eulerAngles.y) * (_status.GetSpeed());
        GameObject.transform.position = pos;
    }

    void FinishAction()
    {


        base.AnimeEnd();



    }
    private async UniTask HunterUpMove() 
    {
        Rigidbody rigidbody = targetHunter.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        Vector3 startPos= targetHunter.transform.position;

        Vector3 endPos= targetHunter.transform.position+new Vector3(0,0.5f,0);

        await UniTask.DelayFrame(20);

        for (int i = 0; i < 30; i++) 
        {
           

            // オブジェクトの移動
            targetHunter.transform.position = Vector3.Lerp(startPos, endPos, (Time.time * 1.0f) / Vector3.Distance(startPos, endPos));

            await UniTask.DelayFrame(1);
        }
        for(int i = 0; i < 20; i++) 
        {


            await UniTask.DelayFrame(1);
        }




        rigidbody.useGravity = true;


    }


}
