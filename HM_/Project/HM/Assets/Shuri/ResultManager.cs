using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultTime;

    ResultRetention result;

    void Start()
    {
        result = GameObject.Find("RetentionObject").GetComponent<ResultRetention>();

        if (result.GetClearFlag()) resultTime.text = "Time : " + result.GetClearTime().ToString();
        else resultTime.text = "Time : --:--";
    }
}
