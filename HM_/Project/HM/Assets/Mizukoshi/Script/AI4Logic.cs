using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 剣の行動論理を表すクラス    行動の基本
/// </summary>
public class AI4Logic : Hunter_AI
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 見つかっているかみつかっていないか
        if (!monsterDisplay)
        {
            // 拘束状態かどうか
            if (CheckRest())
            {
                // 何もしない
                return;
            }
            else
            {
                // 巡回する
                Search();
            }
        }
        else
        {
            // 拘束状態かどうか
            if (CheckRest())
            {
                // 何もしない
                return ;
            }
            else
            {
                // 距離が遠いかどうか
                if (CheckAttackDistance(this.gameObject))
                {
                    // 近くに向かう
                    Chase();
                }
                else
                {
                    // 回避する状態であれば
                    if (attackReady)
                    {
                        // 回避する
                        Avoid();
                    }
                    else
                    {
                        // 攻撃のクールタイムがあるかどうかを確認する
                        if (attackReady)
                        {
                            // 攻撃する
                            Attack();
                        }
                        else
                        {
                           
                            // 後退する
                            Back();
                        }
                    }
                }
            }
            
        }
    }
}
