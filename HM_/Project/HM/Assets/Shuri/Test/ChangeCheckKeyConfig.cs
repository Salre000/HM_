using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static InputManager;

public static class ChangeCheckKeyConfig
{
    public static async UniTask<Key> ChangeKey()
    {
        Key key;
        while (true)
        {
            await UniTask.DelayFrame(1);

            // 十字キー右判定
            if (Input.GetAxis("D_Pad_H") > 0.5f)
            {
                key.keyName = "D_Pad_H";
                key.type = KeyType.AxisPlus;
                return key;
            }
            // 十字キー左判定
            if (Input.GetAxis("D_Pad_H") < -0.5f)
            { 
                key.keyName = "D_Pad_H"; 
                key.type = KeyType.AxisMinus; 
                return key;
            }
            // 十字キー上判定
            if (Input.GetAxis("D_Pad_V") > 0.5f)
            {
                key.keyName = "D_Pad_V";
                key.type = KeyType.AxisPlus;
                return key;
            }
            // 十字キー下判定
            if (Input.GetAxis("D_Pad_V") < -0.5f)
            {
                key.keyName = "D_Pad_V";
                key.type = KeyType.AxisMinus;
                return key;
            }

            if (!Input.anyKeyDown) continue;

            // 
            for (int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown("joystick _menuButton " + i.ToString()))
                {
                    key.keyName = "joystick _menuButton " + i.ToString();
                    key.type = KeyType.Key;
                    return key;
                }
            }
        }
    }
}
