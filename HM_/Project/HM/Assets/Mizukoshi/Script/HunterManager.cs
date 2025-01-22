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

    enum HunterState
    {
        None=0,
        Com
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Hunter");
        _hpManager = GameObject.FindGameObjectWithTag("GameManager");
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
             CheckDamage(i);
             CheckDeath(i);
        }

    }

    public int GetHunterDeathAmount()
    {
        return deathCount;
    }

    void Respawn(int i)
    {
        gameObjects[i].transform.GetComponent<HunterHPManager>().hp = 100;
        gameObjects[i].transform.GetComponent<HunterHPManager>().isDeadFlag = false;
        gameObjects[i].transform.GetComponent<Hunter_AI>().deathAnimationFinish = false;
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

    void SetDisapper()
    {
        // ハンターについているモンスターを見つける関数を呼び出す。

    }
    // 強制的にゲームくりあにする。
    public void ForceDie()
    {
        deathCount = 4;
    }
}
