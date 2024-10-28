using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTestMove : MonoBehaviour
{

    GameObject []GameObjects=new GameObject[5];

    SphereCollider[]Sphere=new SphereCollider[5];
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {



        GameObjects[index] =GameObject.Find("Bone.024");index++;
        GameObjects[index] =GameObject.Find("Bone.019");index++;
        GameObjects[index] =GameObject.Find("Bone.003");index++;
        GameObjects[index] =GameObject.Find("Bone.022");index++;
        GameObjects[index]=GameObject.Find("Bone.015");index++;



        for (int i = 0; i < GameObjects.Length; i++) 
        {
            Sphere[i]=this.gameObject.AddComponent<SphereCollider>();

            Sphere[i].radius = 1.0f;

            Sphere[i].center = GameObjects[i].transform.position-this.transform.position;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
