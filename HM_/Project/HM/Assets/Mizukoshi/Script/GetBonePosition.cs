using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBonePosition : MonoBehaviour
{
    void Start()
    {
        // SkinnedMeshRenderer���擾
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();



        if (skinnedMeshRenderer != null)
        {
            // �{�[���̐����擾
            Transform[] bones = skinnedMeshRenderer.bones;

            // �{�[���̃C���f�b�N�X���o��
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
            Debug.LogError("SkinnedMeshRenderer��������܂���ł����I");
        }
    }
}
