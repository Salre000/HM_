using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowtest : MonoBehaviour
{
    public GameObject g1;
    public GameObject g2;
    public GameObject g3;

    public GameObject obj;

    int count = 0;


    float time = 0;

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 5.0f)
        {
            time = 0;
            SetDirection();
        }
    }

    public void SetDirection()
    {
        GameObject go = GameObject.Instantiate(obj) as GameObject;
        go.transform.position = this.transform.position; 
        if (count == 0)
        {
            go.transform.LookAt(g1.transform.position);
        }

        if (count == 1)
        {
            go.transform.LookAt(g2.transform.position);
        }

        if (count == 2)
        {
            go.transform .LookAt(g3.transform.position);
        }

        count++;
        if (count == 3)
        {
            count = 0;
        }
    }
}
