using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigButton : MonoBehaviour
{
    Button _button;
    [SerializeField] InputManager _inputManager;

    [SerializeField] InputManager.InputKeys _keys;

    private void Awake()
    {
        _inputManager = InputManager.instance.GetComponent<InputManager>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() => _inputManager.KeyChange((int)_keys));
    }
}
