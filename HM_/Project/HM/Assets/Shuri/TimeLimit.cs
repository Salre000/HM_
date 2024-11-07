using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
    const float LimitTime = 600;

    float remainingTime;

    [SerializeField] Image timer;

    void Start()
    {
        remainingTime = LimitTime;
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;

        timer.fillAmount = remainingTime/LimitTime;

        if (remainingTime <= 0) Debug.Log("I—¹");
    }
}
