using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using static InputManager;

public class KeyImageChanger : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Image[] keyImages;

    [SerializeField] Sprite[] keySprites;

    private void Start()
    {
        inputManager = instance.GetComponent<InputManager>();

        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            keyImages[i].sprite = GetKeyImage(inputManager.keys[i]);
        }
    }

    private void Update()
    {
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            if (inputManager.keys[i].type == KeyType.None) { keyImages[i].sprite = null; continue; }
            keyImages[i].sprite = GetKeyImage(inputManager.keys[i]);
        }
    }

    Sprite GetKeyImage(Key key)
    {
        switch(key.keyName)
        {
            case "joystick button 2": return keySprites[0]; // A
            case "joystick button 3": return keySprites[1]; // B
            case "joystick button 0": return keySprites[2]; // X
            case "joystick button 1": return keySprites[3]; // Y
            case "joystick button 7": return keySprites[4]; // RT
            case "joystick button 6": return keySprites[5]; // LT
            case "joystick button 5": return keySprites[6]; // RB
            case "joystick button 4": return keySprites[7]; // LB
            case "D_Pad_H": 
                if(key.type == KeyType.AxisPlus) return keySprites[8];      // 右
                else return keySprites[9];                                  // 左
            case "D_Pad_V":
                if (key.type == KeyType.AxisPlus) return keySprites[10];    // 上
                else return keySprites[11];                                 // 下
            case "joystick button 9": return keySprites[12]; // 右スティック押し込み 
            case "joystick button 8": return keySprites[13]; // 左スティック押し込み
                
        }
        return null;
    }
}
