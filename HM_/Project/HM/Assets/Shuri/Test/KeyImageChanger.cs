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

    [SerializeField] Image[] lamps = new Image[(int)InputKeys.Max];

    private void Awake()
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
            if (inputManager.keys[i].type == KeyType.None) continue;
            keyImages[i].sprite = GetKeyImage(inputManager.keys[i]);
        }
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            if (inputManager.keys[i].keyName == null || inputManager.keys[i].keyName == "") continue;
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
                if(key.type == KeyType.AxisPlus) return keySprites[8];      // ‰E
                else return keySprites[9];                                  // ¶
            case "D_Pad_V":
                if (key.type == KeyType.AxisPlus) return keySprites[10];    // ã
                else return keySprites[11];                                 // ‰º
            case "joystick button 9": return keySprites[12];
            case "joystick button 8": return keySprites[13];
                
        }
        return null;
    }
}
