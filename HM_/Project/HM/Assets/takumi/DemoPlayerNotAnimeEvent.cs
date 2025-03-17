using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DemoPlayerNotAnimeEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //対象のオブジェクトのアニメーションイベントを全て無効化する
        GetComponent<Animator>().fireEvents = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
