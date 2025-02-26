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

        Debug.Log($"Trigger{ObjName}と衝突しました！");

        if (ObjName == "ドラゴン1")
        {
            float d = 200;
            hPManager = GameObject.Find("GameManager").GetComponent<HPManager>();
            hPManager.MonsterDamage(10, ref d, false);
            SoundListManager.instance.PlaySound((int)HunterSE.ArechSE, (int)Main.Hunter);
        }
    }
}
