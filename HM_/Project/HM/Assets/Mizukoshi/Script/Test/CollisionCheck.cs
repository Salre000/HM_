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

        GameObject parent=this.gameObject.transform.parent.parent.gameObject;

        PlayerStatus ste = other.transform.gameObject.GetComponentInParent<PlayerStatus>();
        if(ste!= null)
        {
            //hPManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HPManager>();
            //int damege = 30;
            //float part = damege / 2;
            //hPManager.MonsterDamage(damege, ref part, false);
            parent.SetActive(false);
        }

        if (CheckCollisionTerrain(other)) parent.SetActive(false);




    }

    bool  CheckCollisionTerrain(Collider co)
    {
        Transform check=GetTopLevelParent(co.transform);
        if(check.name== "Terrain")return true;
        return false;
    }

    Transform GetTopLevelParent(Transform currentTransform)
    {
        // 現在の親がnullでない限り、親をたどり続ける
        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
        }
        return currentTransform;  // 最上位の親を返す
    }
}
