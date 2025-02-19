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
            case 0: Debug.Log("A"); break;// A
            case 1: Debug.Log("B"); break;// B
            case 2: Debug.Log("X"); break;// X
            case 3: Debug.Log("Y"); break;// Y
            case 4: Debug.Log("RT"); break;// RT
            case 5: Debug.Log("LT"); break;// LT
            case 6: Debug.Log("RB"); break;// RB
            case 7: Debug.Log("LB"); break;// LB
            case 8 :Debug.Log("Right");break;      // ‰E
            case 9: Debug.Log("Left"); break;                                 // ¶
            case 10: Debug.Log("Up"); break;   // ã
            case 11: Debug.Log("Down"); break;                                // ‰º
        }
    }
}
