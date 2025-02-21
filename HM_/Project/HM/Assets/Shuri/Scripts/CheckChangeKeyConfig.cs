using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static InputManager;

public static class CheckChangeKeyConfig
{
    public static async UniTask<Key> ChangeKey()
    {
        Key key;
        while (true)
        {
            await UniTask.DelayFrame(1);

            // �\���L�[�E����
            if (Input.GetAxis("D_Pad_H") > 0.5f)
            {
                key.keyName = "D_Pad_H";
                key.type = InputType.AxisPlus;
                return key;
            }
            // �\���L�[������
            if (Input.GetAxis("D_Pad_H") < -0.5f)
            { 
                key.keyName = "D_Pad_H"; 
                key.type = InputType.AxisMinus; 
                return key;
            }
            // �\���L�[�㔻��
            if (Input.GetAxis("D_Pad_V") > 0.5f)
            {
                key.keyName = "D_Pad_V";
                key.type = InputType.AxisPlus;
                return key;
            }
            // �\���L�[������
            if (Input.GetAxis("D_Pad_V") < -0.5f)
            {
                key.keyName = "D_Pad_V";
                key.type = InputType.AxisMinus;
                return key;
            }

            if (!Input.anyKeyDown) continue;
            
            // 
            for (int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown("joystick button " + i.ToString()))
                {
                    key.keyName = "joystick button " + i.ToString();
                    key.type = InputType.Key;
                    return key;
                }
            }
        }
    }
}
