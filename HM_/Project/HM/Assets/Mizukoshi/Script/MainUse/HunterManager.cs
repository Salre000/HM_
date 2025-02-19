using Cysharp.Threading.Tasks;
using UnityEngine;
public class HunterManager : MonoBehaviour
{
    private GameObject[] gameObjects;
    private HPManager _hpManager;
    public UIManager uiManager;
    public int deathCount = 0;
    private int preDeathNum = -1;
    bool deathAnimationNow = false;
    private float deathResetTime = 0f;
    private float deathResetDelay = 3f;
   


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
        respawnPosition = new Vector3(-0.766783059f, 0.951499999f, 10.727313f);
        _hpManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
        //ResetPreDeathNum();
        DebugCommand();
        
        if (_hpManager == null)
        {
            Debug.Log("AAA");
        }
    }

    public int GetHunterDeathAmount()
    {
        return deathCount;
    }

    public void Respawn(int i)
    {
        _hpManager.HunterHeel(100, i);
        gameObjects[i].transform.position = respawnPosition;
        gameObjects[i].GetComponent<Hunter_AI>().WaitForCount();
        gameObjects[i].GetComponent<Hunter_AI>().SetNavmesh();
        deathCount++;
        //uiManager.ObjectiveText();
    }

    /// <summary>
    /// 死にました→GetHunterLostNumber()→n秒後その値が-1   リスポーン後1秒待機
    /// </summary>
    /// <param name="hunterNum"></param>
    void CheckDeath()
    {
        int deathNum = _hpManager.GetHunterLostNumber();
        if (deathNum == -1) return;
        if(preDeathNum==deathNum) return;
        preDeathNum = deathNum;
        Hunter_AI sss =gameObjects[deathNum].GetComponent<Hunter_AI>();
        sss.Death();
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
            //gameObjects[i].GetComponent<Hunter_AI>().monsterDisplay = true;
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
        if (Input.GetKeyUp(KeyCode.L))
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
               _hpManager.HunterDamage(90,i);
            }
        }
    }

    private void ResetPreDeathNum()
    {
        if (preDeathNum != 0)
        {
            deathResetTime += Time.deltaTime;
            if (deathResetTime > deathResetDelay)
            {
                preDeathNum = 0;
            }
        }
        
    }
}
