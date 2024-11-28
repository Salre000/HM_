using UnityEngine;
public class HunterManager : MonoBehaviour
{

    private GameObject[] gameObjects;
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
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].transform.GetComponent<HunterHPManager>().isDeadFlag)
            {

                deathCount++;
                Respawn(i);
            }
        }
    }

    public int GetHunterDeathAmount()
    {
        return deathCount;
    }

    void Respawn(int i)
    {
        gameObjects[i].transform.GetComponent<HunterHPManager>().hp = 110;
        gameObjects[i].transform.GetComponent<HunterHPManager>().isDeadFlag = false;
        gameObjects[i].transform.GetComponent<Hunter_AI>().deathAnimationFinish = false;
        gameObjects[i].transform.position = respawnPosition;
    }
}
