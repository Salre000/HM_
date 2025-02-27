using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public HPManager hPManager;

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの名前を取得
        string otherObjectName = collision.gameObject.name;

        // コンソールにメッセージを表示
        Debug.Log($"Collider{otherObjectName}と衝突しました！");

        // ここで何か別のアクションを実行できます
        // 例: スコアを加算する、オブジェクトを破壊するなど
    }

    private void OnTriggerEnter(Collider other)
    {
        string ObjName=other.gameObject.name;

        GameObject parent=this.gameObject.transform.parent.gameObject;
       
        Destroy(parent);
    }
}
