using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultRetention : MonoBehaviour
{
    float elapsedTime;
    bool clearFlag;

    static ResultRetention resultRetention;

    void Start()
    {
            DontDestroyOnLoad(this.gameObject);

        if (!(SceneManager.GetActiveScene().name == "Result" || SceneManager.GetActiveScene().name == "Main")) Destroy(this.gameObject);

        if(SceneManager.GetActiveScene().name == "Result")
        {
            TextMeshProUGUI resultTime = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();

            if (clearFlag) resultTime.text = "Time : " + elapsedTime.ToString();
            else resultTime.text = "Time : --:--";
        }
       
    }

    public void SetResultData(bool flag, float time)
    {
        clearFlag = flag;
        elapsedTime = time;
    }
}
