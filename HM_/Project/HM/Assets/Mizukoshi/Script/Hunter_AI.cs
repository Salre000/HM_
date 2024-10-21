using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter_AI : MonoBehaviour
{
    // ① 距離が30以上あるならばナビメッシュによる移動

    // ② 距離が30以下ならばゆっくり移動

    // ③ 敵が攻撃を5回してきたらまたは体力が20以下なら一度離れる

    // ④ 距離が10以下ならば攻撃する。

    float distance = 0;
    int attackNum = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 攻撃が5回目になったらいったんよける
        if (attackNum % 5 == 0)
        {
            // 逃げる
        }
        else
        {

            if (distance >= 30)
            {
                // ナビメッシュによる移動
            }
            else if (distance < 30 && distance > 10)
            {
                // ゆっくり移動
            }

            else if (distance < 10)
            {
                // 攻撃

            }

        }

    }
}
