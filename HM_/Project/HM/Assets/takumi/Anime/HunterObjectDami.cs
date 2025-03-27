using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterObjectDami : MonoBehaviour
{
    public GameObject[] Hunters =new GameObject[4];
    public GameObject[] HuntersObject =new GameObject[4];

    public static HunterObjectDami instance;

    public void Start()
    {
        instance = this;
        for (int i = 0; i < 4; i++) 
        {
            HuntersObject[i]=Instantiate(Hunters[i]);

            HuntersObject[i].transform.position = Vector3.zero;

        }
        
    }

}
