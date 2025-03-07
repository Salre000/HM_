using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnemySight2 : MonoBehaviour
{
    public float headTurnSpeed = 2f;  // 顔を振る速さ
    private Vector3 targetRotation;

    void Start()
    {
        // 初期のターゲット回転を設定
        SetRandomHeadRotation();
    }

    public void Update()
    {
        // 顔を振る処理
        Vector3 direction = targetRotation - transform.eulerAngles;
        if (direction.magnitude > 0.1f)
        {
            // 回転
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetRotation, Time.deltaTime * headTurnSpeed);
        }
        else
        {
            // ランダムな方向に顔を向ける
            SetRandomHeadRotation();
        }
    }

    void SetRandomHeadRotation()
    {
        // ランダムな方向に顔を向ける
        float randomX = Random.Range(-30f, 30f);  
        float randomY = Random.Range(0f, 360f); 
        targetRotation = new Vector3(randomX, randomY, 0f);
    }
}
