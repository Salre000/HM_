using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavObstacle : MonoBehaviour
{
    private NavMeshObstacle navMeshObstacle;
    /// <summary>
    /// ナビメッシュにobstaclコンポーネントをアタッチする関数
    /// </summary>
    public void AttachNavmeshObstacle(GameObject targetObject)
    {
        // NavMeshObstacleコンポーネントが既にアタッチされていない場合のみ追加
        if (GetComponent<NavMeshObstacle>() == null)
        {
            // NavMeshObstacleをオブジェクトに追加
            NavMeshObstacle navMeshObstacle = targetObject.AddComponent<NavMeshObstacle>();

            // 必要に応じて設定を変更
            navMeshObstacle.carving = true;
            navMeshObstacle.shape = NavMeshObstacleShape.Capsule;
            navMeshObstacle.radius = 0.5f;
            navMeshObstacle.height = 3.0f;
            navMeshObstacle.center = new Vector3(0, 0, 0);
        }
    }
}
