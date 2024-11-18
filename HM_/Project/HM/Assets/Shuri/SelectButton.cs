using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] Button button;
    InputManager _inputManager;

    void Start()
    {
        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update()
    {
        if (Input.GetAxis("D_Pad_V") > 0)
        {
            button.navigation.selectOnUp.Select();
            button = button.navigation.selectOnUp.GetComponent<Button>();
        }
        if (Input.GetAxis("D_Pad_V") < 0)
        {
            button.navigation.selectOnDown.Select();
            button = button.navigation.selectOnDown.GetComponent<Button>();
        }
    }
}
