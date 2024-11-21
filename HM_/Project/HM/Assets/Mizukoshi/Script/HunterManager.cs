using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterManager : MonoBehaviour
{

    private GameObject[] gameObjects;

    int deathCount = 0;

    Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Hunter");
        respawnPosition= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].transform.GetComponent<HunterHPManager>().isDeadFlag)
            {
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
        deathCount++;
        gameObjects[i].transform.GetComponent<HunterHPManager>().hp = 100;
        gameObjects[i].transform.GetComponent<HunterHPManager>().isDeadFlag = false;
        gameObjects[i].transform.position = respawnPosition;
    }
}
