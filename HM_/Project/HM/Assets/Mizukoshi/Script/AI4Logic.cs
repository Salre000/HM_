using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 剣の行動論理を表すクラス    行動の基本
/// </summary>
public class AI4Logic : Hunter_AI
{
    
    public bool disappear=false;

    public bool notMoveActive=false;

    public bool attackDistance=false;

    public bool avoidActive=false;

    public bool readyAttack=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 見つかっているかみつかっていないか
        if (!disappear)
        {
            // 拘束状態かどうか
            if (notMoveActive)
            {
                // 何もしない
            }
            else
            {
                // 巡回する
                
            }
        }
        else
        {
            // 拘束状態かどうか
            if (notMoveActive)
            {
                // 何もしない
            }
            else
            {
                // 距離が遠いかどうか
                if (!attackDistance)
                {
                    // 近くに向かう
                    Chase();
                }
                else
                {
                    // 回避する状態であれば
                    if (avoidActive)
                    {
                        // 回避する
                        Avoid();
                    }
                    else
                    {
                        // 攻撃のクールタイムがあるかどうかを確認する
                        if (readyAttack)
                        {
                            // 攻撃する
                            Attack();
                        }
                        else
                        {
                            // クールタイムを待つ
                        }
                    }
                }
            }
            
        }
    }
}
