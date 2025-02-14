using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter_ID:MonoBehaviour
{
    private int _ID;

    public int GetHunterID()
    {
        return _ID;
    }

    public void SetHunterID(int hunterID)
    {
        _ID = hunterID;
    }
}
