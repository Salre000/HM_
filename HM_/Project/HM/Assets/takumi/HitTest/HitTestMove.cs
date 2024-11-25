using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTestMove : MonoBehaviour
{

    public GameObject []GameObjects=new GameObject[5];

    public SphereCollider[]Sphere=new SphereCollider[5];

    public BoxCollider[]BoxCollider=new BoxCollider[5];
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
            Sphere[i]= GameObjects[i].AddComponent<SphereCollider>();

            Sphere[i].radius = 0.005f;

            Vector3 Game = Vector3.zero;//GameObjects[i].transform.position;

            // Game -= this.transform.position;

            //Game*=10.0f;

            Sphere[i].center = Game;

        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
