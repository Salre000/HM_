using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputManager;

public class KeyCheckTest : MonoBehaviour
{
    InputManager _inputManager;

    void Start()
    {
        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update()
    {
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            if (_inputManager.keys[i].type == InputType.Key)
            {
                if (Input.GetKeyDown(_inputManager.keys[i].keyName)) GetKey(i);
            }
            if (_inputManager.keys[i].type == InputType.AxisPlus)
            {
                if (Input.GetAxis(_inputManager.keys[i].keyName)>0.5f) GetKey(i);
            }
            if (_inputManager.keys[i].type == InputType.AxisMinus)
            {
                if (Input.GetAxis(_inputManager.keys[i].keyName)<-0.5f) GetKey(i);
            }

        }
    }

    private void GetKey(int i)
    {
        switch (i)
        {
            case 0: Debug.Log("Jump"); break;// Jump
            case 1: Debug.Log("Jump"); break;// Jump
            case 2: Debug.Log("X"); break;// X
            case 3: Debug.Log("Y"); break;// Y
            case 4: Debug.Log("Normal"); break;// Normal
            case 5: Debug.Log("Skill"); break;// Skill
            case 6: Debug.Log("Special1"); break;// Special1
            case 7: Debug.Log("Special2"); break;// Special2
            case 8 :Debug.Log("Right");break;      // ‰E
            case 9: Debug.Log("Left"); break;                                 // ¶
            case 10: Debug.Log("Up"); break;   // ã
            case 11: Debug.Log("Down"); break;                                // ‰º
        }
    }
}
