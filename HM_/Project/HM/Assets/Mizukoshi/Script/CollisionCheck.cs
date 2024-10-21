using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの名前を取得
        string otherObjectName = collision.gameObject.name;

        // コンソールにメッセージを表示
        Debug.Log($"{otherObjectName}と衝突しました！");

        // ここで何か別のアクションを実行できます
        // 例: スコアを加算する、オブジェクトを破壊するなど
    }
}
