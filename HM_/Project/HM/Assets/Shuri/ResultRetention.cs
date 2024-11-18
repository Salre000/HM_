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
        if (resultRetention == null)
        {
            resultRetention = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

        if (!(SceneManager.GetActiveScene().name == "Result" || SceneManager.GetActiveScene().name == "Main")) Destroy(this.gameObject);
    }

    public void SetResultData(bool flag, float time)
    {
        clearFlag = flag;
        elapsedTime = time;
    }

    public bool GetClearFlag()
    {
        return clearFlag;
    }

    public float GetClearTime()
    {
        return elapsedTime;
    }
}
