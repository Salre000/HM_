using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class HunterManager : MonoBehaviour
{

    private GameObject[] gameObjects;
    private Animator _animator;

    int deathCount = 0;
    bool deathAnimationNow = false;
    bool[]isDeath = new bool[4];
    float []time=new float[4];
    

    Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Hunter");
        respawnPosition= transform.position;

        _animator = gameObjects[0].GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].transform.GetComponent<HunterHPManager>().isDeadFlag&&!deathAnimationNow)
            {
                _animator.SetBool("isDead",true);
                isDeath[i] = true;
                deathAnimationNow=true;
                deathCount++;
            }
        }

        for (int i = 0; i <4; i++)
        {
           if (isDeath[i])
           {
                time[i] += Time.deltaTime;
                if (time[i] > 0.5f)
                {
                    time[i] = 0f;
                    isDeath[i] = false;
                    _animator.SetBool("isDead", false);
                    deathAnimationNow=false;
                    Respawn(i);
                }
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
