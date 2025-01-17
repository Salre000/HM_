using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWin : MonoBehaviour
{
    HunterManager _hunterManager;
    // Start is called before the first frame update
    void Start()
    {
        _hunterManager = GameObject.Find("GameManager").GetComponent<HunterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))_hunterManager.ForceDie();
    }
}
