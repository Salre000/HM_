using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigSave : MonoBehaviour
{
    [HideInInspector] public ConfigData data;
    public void OnButtonSave()
    {
        InputManager.instance.Save(data);
    }

    public void OnButtonLoad()
    {
        InputManager.instance.Load(Application.dataPath + "/KeyConfig2.json");
    }
}
