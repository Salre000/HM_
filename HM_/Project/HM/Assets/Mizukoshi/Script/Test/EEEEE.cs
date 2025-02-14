using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEEEE : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        // ParticleSystemコンポーネントを取得
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            RestartEffect();
        }
    }

    void RestartEffect()
    {
        // エフェクトを停止してから再度再生
        particleSystem.Stop();
        particleSystem.Clear();  // パーティクルをリセット（必要な場合）
        particleSystem.Play();
    }
}
