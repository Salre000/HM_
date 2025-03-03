using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using static InputManager;

public class KeyImageChanger : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Image[] _keyImages;

    [SerializeField] private Sprite[] _keySprites;
    //private static readonly string[] _keySpritesName =
    //{
    //    "xbox_button_a_outline","xbox_button_b_outline","xbox_button_x_outline","xbox_button_y_outline",
    //    "xbox_rt_outline","xbox_lt_outline","xbox_rb_outline","xbox_lb_outline",
    //    "xbox_dpad_right_outline","xbox_dpad_left_outline","xbox_dpad_up_outline","xbox_dpad_down_outline",
    //    "xbox_stick_r_press","xbox_stick_l_press","xbox_button_start_outline","xbox_button_back_outline",
    //};

    void Start()
    {
        _inputManager = instance.GetComponent<InputManager>();

        // コンフィグ用画像の読み込み
        //for (int i = 0, max = _keySpritesName.Length; i < max; i++)
        //{
        //    _keySprites = Resources.LoadAll<Sprite>("Config/" + _keySpritesName[i]);
        //}

        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            _keyImages[i].sprite = GetKeyImage(_inputManager.keys[i]);
        }
    }

    private void Update()
    {
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            if (_inputManager.keys[i].type == InputType.None) { _keyImages[i].sprite = null; continue; }
            _keyImages[i].sprite = GetKeyImage(_inputManager.keys[i]);
        }
    }

    /// <summary>
    /// 渡されたキーに対応するスプライトを返す
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Sprite GetKeyImage(Key key)
    {
        switch (key.keyName)
        {
            case "joystick button 2": return _keySprites[0]; // Jump
            case "joystick button 3": return _keySprites[1]; // Jump
            case "joystick button 0": return _keySprites[2]; // X
            case "joystick button 1": return _keySprites[3]; // Y
            case "joystick button 7": return _keySprites[4]; // Normal
            case "joystick button 6": return _keySprites[5]; // Skill
            case "joystick button 5": return _keySprites[6]; // Special1
            case "joystick button 4": return _keySprites[7]; // Special2
            case "D_Pad_H":
                switch (key.type)
                {
                    case InputType.AxisPlus: return _keySprites[8];     // DPad右  
                    case InputType.AxisMinus: return _keySprites[9];    // DPad左
                }
                break;
            case "D_Pad_V":
                switch (key.type)
                {
                    case InputType.AxisPlus: return _keySprites[10];   // DPad上 
                    case InputType.AxisMinus: return _keySprites[11];  // DPad下
                }
                break;
            case "joystick button 9": return _keySprites[12]; // 右スティック押し込み 
            case "joystick button 8": return _keySprites[13]; // 左スティック押し込み

        }
        return null;
    }
}
