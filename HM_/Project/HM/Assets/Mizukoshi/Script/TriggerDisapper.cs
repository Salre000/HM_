using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisapper : MonoBehaviour
{
    public GameObject spearHuman;

    public GameObject objectToClone;  // 生成するオブジェクト
    private GameObject clonedObject;  // 生成したオブジェクトの参照

    public bool condition = false;  // 条件をチェックするためのフラグ

    void Update()
    {
        condition = spearHuman.GetComponent<Hunter_AI>().GetAttackState();
        // 条件が真ならオブジェクトを生成
        if (condition && clonedObject == null)
        {
            // オブジェクトを生成（位置は (0, 0, 0) に設定）
            clonedObject = Instantiate(objectToClone, new Vector3(0, 0, 0), Quaternion.identity);
            clonedObject.transform.SetParent(this.transform, false);
        }
        // 条件が偽ならオブジェクトを破壊
        else if (!condition && clonedObject != null)
        {
            // オブジェクトを破壊
            Destroy(clonedObject);
            clonedObject = null;  // 参照をクリア
        }
    }
}


//spearHuman.GetComponent<Hunter_AI>().GetAttackState()