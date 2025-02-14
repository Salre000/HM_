using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenSet : MonoBehaviour
{
    public GameObject objectA; // オブジェクトAをインスペクタで設定
    public GameObject objectB; // オブジェクトBのプレハブ

    void Start()
    {
        if (objectA != null && objectB != null)
        {
            // ObjectAの下にObjectBを生成
            GameObject newObjectB = Instantiate(objectB, objectA.transform);

            // 必要に応じて新しく生成したBオブジェクトの位置を調整
            newObjectB.transform.localPosition = Vector3.zero; // 位置は親の位置に合わせる
        }
        else
        {
            Debug.LogError("ObjectAまたはObjectBが設定されていません");
        }
    }
}
