using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetArcher : MonoBehaviour
{
    private GameObject archer;
    TestCollision test;
    // Start is called before the first frame update
    void Start()
    {
        
        archer = GameObject.Find("Last");
        test = this.transform.GetChild(1).GetComponent<TestCollision>();

    }

    // Update is called once per frame
    void Update()
    {
        test.SetGameObject(archer);
    }
}
