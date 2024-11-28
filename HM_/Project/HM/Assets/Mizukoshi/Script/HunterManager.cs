using UnityEngine;
public class HunterManager : MonoBehaviour
{

    private GameObject[] gameObjects;
    private Animator[] _animator = new Animator[1];

    private GameObject _spear;

    int deathCount = 0;
    bool deathAnimationNow = false;
    bool[] isDeath = new bool[4];
    float[] time = new float[4];


    Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Hunter");
        respawnPosition = transform.position;
        for (int i = 0; i < _animator.Length; i++)
        {
            _animator[i] = gameObjects[i].GetComponent<Animator>();
        }
        _spear = GameObject.FindGameObjectWithTag("EnemyAttack");
    }

    // Update is called once per frame
    void Update()
    {
        

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (!gameObjects[i].GetComponent<Hunter_AI>().GetAnimState().IsName("ataka1"))
            {
                _spear.GetComponent<TriggerDisapper>().DestoryTrigger();
            }

            if (gameObjects[i].transform.GetComponent<HunterHPManager>().isDeadFlag && !deathAnimationNow)
            {
                _animator[i].SetBool("isDead", true);
                isDeath[i] = true;
                deathAnimationNow = true;
                deathCount++;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (!isDeath[i]) continue;
            isDeath[i] = false;
 
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
        isDeath[i]=false;
        _animator[i].SetBool("isDead", false);
        deathAnimationNow = false;
        gameObjects[i].transform.position = respawnPosition;
    }
}
