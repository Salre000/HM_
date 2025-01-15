using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 行動論理
/// 
/// ①敵を見つけるまでは徘徊
/// 
/// ②敵を見つけたら2番目に近いところに移動
/// 
/// ③狙撃(cool timeは5秒)
/// 
/// ④敵が近づいてきたら逃げる もしくは近距離攻撃
/// 
/// ⑤以降繰り返し
/// 
/// 
/// 
/// </summary>

// 弓のAIの行動論理
public class AI2Logic : MonoBehaviour
{
    // 定位置(弓を引いて攻撃する位置)
    public Vector3 []homePosition=new Vector3[4];

    // 最低限とりたい距離
    const float BASEDISTANCE = 30;

    // 攻撃のクールタイム
    public float attackCoolTime = 5.0f;

    // モンスターのオブジェクト 
    private GameObject _monster;

    //エージェントとなるオブジェクトのNavMeshAgent格納用 
    private NavMeshAgent agent;

    // 
    private Animator _animator;

    // 
    AnimatorStateInfo animationState;

    // 敵をみつけたかのフラグ
    private bool _diapperFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }




    public void SetDiapperFlag()
    {
        _diapperFlag=true;
    }

    // NavmeshAgentの最短経路の近くに敵がいるかどうかを判断する関数
    void CheckEnemyInPath()
    {

    }

    // 障害物を架空上にセットする
    void SetObstacle()
    {

    }

}
