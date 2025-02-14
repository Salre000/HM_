using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 障害物を設定　　
/// 案1      罠生成時点で確率
/// 案2      一定乱数周期時間でObstacleコンポーネントをOnOffする
/// 案3　　　自力でNavmeshを完成させ、独自の行動を行わせる
/// 案4      罠生成をAI視点からみて視認角度かつ視認距離が一定未満で時間計測開始
/// 　　　　 一定時間経過できたらObstacleコンポーネントをつける。
/// 　　　　 視認角度、視認距離
/// </summary>
public class TestObstacle : MonoBehaviour
{

    private NavMeshObstacle navMeshObstacle;
    /// <summary>
    /// ナビメッシュにobstaclコンポーネントをアタッチする関数
    /// </summary>
    public void AttachNavmeshObstacle()
    {
        // NavMeshObstacleコンポーネントが既にアタッチされていない場合のみ追加
        if (GetComponent<NavMeshObstacle>() == null)
        {
            // NavMeshObstacleをオブジェクトに追加
            NavMeshObstacle navMeshObstacle = gameObject.AddComponent<NavMeshObstacle>();

            // 必要に応じて設定を変更
            navMeshObstacle.carving = true; 
            navMeshObstacle.shape = NavMeshObstacleShape.Capsule;
            navMeshObstacle.radius = 0.5f; 
            navMeshObstacle.height = 3.0f; 
            navMeshObstacle.center = new Vector3(0,0,0);    
        }
    }
}
