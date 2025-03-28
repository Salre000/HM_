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
    public PlayerSpiderFinish(GameObject Object, AudioSource source, Animator animator, System.Action<bool> animeFlagReset, System.Func<GameObject> setGetHunterObject,GameObject setCapObject) : base(Object, source, animator, animeFlagReset)
    {
        camera = Camera.main.gameObject;
        GetHunterObject = setGetHunterObject;
        capObject = setCapObject;
    }

    Vector3 offset = Vector3.zero;
    int targetID = -1;

    public override void Start()
    {
        offset=camera.transform.position- GameObject.transform.position;
     
        finishActionFlag = false;
        _status = GameObject.GetComponent<PlayerStatus>();
        targetID=GetHunterObject().GetComponent<Hunter_ID>().GetHunterID();

        targetHunter = HunterObjectDami.instance.HuntersObject[targetID];

        targetHunter.transform.position = GetHunterObject().transform.position;

        HunterManager hunterManager = UnityEngine.GameObject.Find("GameManager").GetComponent<HunterManager>();

        CameraManager.useFlag = false;

        hunterManager.Respawn(targetID);

        hunterParentObject = targetHunter.transform.parent != null ? targetHunter.transform.parent.gameObject : null;

        capObject.SetActive(true);
        targetHunter.transform.parent = capObject.transform;

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
        targetHunter.transform.localPosition = Vector3.zero;


        if (!finishActionFlag) FinishMove();
        else FinishAction();


    }
    public override void AnimeEvent()
    {

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

            cameraManager = camera.GetComponent<CameraManager>();

            cameraManager.SetCameraUseFlag(false);


            return;
        }

        Vector3 pos = GameObject.transform.position;

        //ÉvÉåÉCÉÑÅ[ÇÃà⁄ìÆ
        pos.x += Mathf.Sin(GameObject.transform.eulerAngles.y * Mathf.Deg2Rad) * (_status.GetSpeed());
        pos.z += Mathf.Cos(GameObject.transform.eulerAngles.y * Mathf.Deg2Rad) * (_status.GetSpeed());
        GameObject.transform.position = pos;
    }

    void FinishAction()
    {


        Vector3 pos;


        pos = targetHunter.transform.position+ offset;




        camera.transform.position = pos;



        camera.transform.LookAt(targetHunter.transform.position);




    }
    CameraManager cameraManager;



    private async UniTask DaleyTask()
    {

        await UniTask.DelayFrame(1);

        //cameraManager.SetCameraUseFlag(true);

        HPManager.instance.SetMonsterUseFlag(true);

        if (targetHunter != null)
        {
            targetHunter.transform.parent = hunterParentObject != null ? hunterParentObject.transform : null;
        }

        cameraManager.SetCameraUseFlag(true);


        capObject.gameObject.SetActive(false);

        targetHunter.transform.position = Vector3.zero;
        CameraManager.useFlag = true;

        AnimeEnd();
    }

}