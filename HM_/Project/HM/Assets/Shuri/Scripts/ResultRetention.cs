using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultRetention
{
    private static float _clearTime { get; set; } = -1;

    public static float GetClearTime()
    {
        return _clearTime;
    }

    public static void SetClearTime(float time)
    {
        _clearTime = time;
    }

    public static bool ClearCheck()
    {
        if(_clearTime <= 0.0f) return false;
        return true;
    }
}
