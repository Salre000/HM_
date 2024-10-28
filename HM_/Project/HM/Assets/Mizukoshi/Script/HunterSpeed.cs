using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSpeed : MonoBehaviour
{
    // 割合
    public float ratio = 5;

    public float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeed/ratio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // プレイヤーの移動スピード
    float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
