using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBonePosition : MonoBehaviour
{
    void Start()
    {
        // SkinnedMeshRendererを取得
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();



        if (skinnedMeshRenderer != null)
        {
            // ボーンの数を取得
            Transform[] bones = skinnedMeshRenderer.bones;

            // ボーンのインデックスを出力
            for (int i = 0; i < bones.Length; i++)
            {
                if (i == 8)
                {
                    Debug.Log(bones[i].name);
                    Debug.Log(bones[i].position);
                }
            }
        }
        else
        {
            Debug.LogError("SkinnedMeshRendererが見つかりませんでした！");
        }
    }
}
