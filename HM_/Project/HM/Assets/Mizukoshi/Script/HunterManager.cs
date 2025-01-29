using UnityEngine;
public class HunterManager : MonoBehaviour
{

    private GameObject[] gameObjects;
    private GameObject _spear;

    private GameObject _hpManager;

    int deathCount = 0;
    bool deathAnimationNow = false;
    bool[] isDeath = new bool[4];
    float[] time = new float[4];


    Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Hunter");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].GetComponent<Hunter_AI>() == null) continue;
            gameObjects[i].GetComponent<Hunter_ID>().SetHunterID(i);
        }
        _hpManager = GameObject.FindGameObjectWithTag("GameManager");
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
             //CheckDamage(i);
             //CheckDeath(i);
        }
        DebugCommand();

    }

    public int GetHunterDeathAmount()
    {
        return deathCount;
    }

    void Respawn(int i)
    {
        gameObjects[i].transform.GetComponent<HunterHPManager>().hp = 100;
        gameObjects[i].transform.GetComponent<HunterHPManager>().isDeadFlag = false;
        gameObjects[i].transform.position = respawnPosition;

    }

    void CheckDamage(int hunterNum)
    {
       
        if (gameObjects[hunterNum].GetComponent<HunterHPManager>().isHit)
        {
            _hpManager.GetComponent<HPManager>().HunterDamage(AmountDamaged(hunterNum), hunterNum);
            gameObjects[hunterNum].GetComponent<HunterHPManager>().isHit=false;
        }

    }
    /// <summary>
    /// 死にました→GetHunterLostNumber()→n秒後その値が-1   リスポーン後1秒待機
    /// </summary>
    /// <param name="hunterNum"></param>
    void CheckDeath(int hunterNum)
    {
        if (_hpManager.GetComponent<HPManager>().GetHunterLostNumber() == -1)return;
        else
        {
            Respawn(_hpManager.GetComponent<HPManager>().GetHunterLostNumber());
            deathCount++;
            int errorNum = -1;
            _hpManager.GetComponent<HPManager>().SetHunterLostNumber(errorNum);
            _hpManager.GetComponent<HPManager>().HunterHeel(100,hunterNum);
        }

    }

    float AmountDamaged(int hunterNum)
    {
        return gameObjects[hunterNum].GetComponent<HunterHPManager>().collider.gameObject.GetComponent<Damage>().GetDamage();
    }

    /// <summary>
    /// ハンターがモンスターを見つけたときに呼ぶ関数
    /// </summary>
    public void SetDisapper()
    {
        // ハンターについているモンスターを見つける関数を呼び出す。
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<Hunter_AI>().DisappearMonster();
        }
    }

    /// <summary>
    /// ハンターの危険状態を確認
    /// </summary>
    public void SetDengerousState()
    {
        // 
    }



    // 強制的にゲームくりあにする。
    public void ForceDie()
    {
        deathCount = 4;
    }

    public void DebugCommand()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            gameObjects[0].GetComponent<Hunter_AI>().DeathAnimation();
            Debug.Log(gameObjects[0].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            gameObjects[1].GetComponent<Hunter_AI>().DeathAnimation();
            Debug.Log(gameObjects[1].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            gameObjects[2].GetComponent<Hunter_AI>().DeathAnimation();
            Debug.Log(gameObjects[2].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            gameObjects[0].GetComponent<Hunter_AI>().StartRestraining();
            Debug.Log(gameObjects[0].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            gameObjects[1].GetComponent<Hunter_AI>().StartRestraining();
            Debug.Log(gameObjects[1].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            gameObjects[2].GetComponent<Hunter_AI>().StartRestraining();
            Debug.Log(gameObjects[2].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.R))
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].GetComponent<Hunter_AI>().StopRestraining();
                gameObjects[i].GetComponent<Hunter_AI>().ResetAnimation();
            }
        }
    }
}
