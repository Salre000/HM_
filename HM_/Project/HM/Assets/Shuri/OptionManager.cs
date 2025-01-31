using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static InputManager;

public class OptionManager : MonoBehaviour
{
    [SerializeField] GameObject panelll;
    public int menuIndex = 1;
    int menuNum = 4;

    private int _sliderIndex;

    [SerializeField] TextAsset _option;

    [SerializeField] GameObject _uiPanel;
    [SerializeField] GameObject _beltText;
    [SerializeField] GameObject _objective;

    [SerializeField] Slider[] _slider;
    [SerializeField] Slider _sensibilityBar;
    [SerializeField] Slider _bgmBar;
    [SerializeField] Slider _seBar;
    [SerializeField] RectTransform _cursor;

    [SerializeField] Button _menuButton;

    [SerializeField] Button _configButton;

    InputManager _inputManager;

    UIManager _uiManager;

    UniTask _panelMoveTask = UniTask.CompletedTask;
    UniTask _cursorMoveTask = UniTask.CompletedTask;

    void Start()
    {
        panelll.SetActive(false);
        _uiPanel.SetActive(false);

        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        string jsonText = _option.ToString();

        JsonNode json = JsonNode.Parse(jsonText);

        _cursor.anchoredPosition = new(_cursor.anchoredPosition3D.x, (1 - _sliderIndex) * 100);

        _sensibilityBar.value = float.Parse(json["sensibility"].Get<string>());
        _bgmBar.value = float.Parse(json["BGMvolume"].Get<string>());
        _seBar.value = float.Parse(json["SEvolume"].Get<string>());

        _uiManager.SetSliderValue(
            (int)_sensibilityBar.value,
            (int)_bgmBar.value,
            (int)_seBar.value);
    }

    async void Update()
    {
        // オプション画面の開閉
        if (Input.GetKeyDown(KeyCode.JoystickButton11)) UISwitch();

        // オプション画面が開いていたら
        if (_uiPanel.activeSelf)
        {
            Time.timeScale = 0.0f;

            // RB
            if (_inputManager.IsOnButton(_inputManager.keys[(int)InputKeys.RB]) && menuIndex < menuNum && _panelMoveTask.Status.IsCompleted())
            {
                menuIndex++;
                _panelMoveTask = UIMove(Vector3.left);
            }
            // LB
            if (_inputManager.IsOnButton(_inputManager.keys[(int)InputKeys.LB]) && menuIndex > 1 && _panelMoveTask.Status.IsCompleted())
            {
                menuIndex--;
                _panelMoveTask = UIMove(Vector3.right);
            }
        }
        else menuIndex = 1;

        EventSystem.current.SetSelectedGameObject(null);

        panelll.SetActive(false);
        if (!Input.GetKeyDown(KeyCode.JoystickButton3)) return;

        panelll.SetActive(true);

        while (true)
        {
            switch (menuIndex)
            {
                case 1: Menu().Forget(); break;
                case 2: break;
                case 3: Option().Forget(); break;
                case 4: KeyConfig().Forget(); break;
            }
        }
        int a = 0;
    }

    void UISwitch()
    {
        if (_uiPanel.activeSelf)
        {
            _uiManager.SetSliderValue(
                (int)_sensibilityBar.value,
                (int)_bgmBar.value,
                (int)_seBar.value);

            Time.timeScale = 1.0f;
        }
        else Time.timeScale = 0.0f;

        _uiPanel.SetActive(!_uiPanel.activeSelf);
    }

    private async UniTask UIMove(Vector3 dir)
    {
        Vector3 beltPos = _beltText.transform.position;
        Vector3 objectivePos = _objective.transform.position;

        for (int i = 0; i < 10; i++)
        {
            _beltText.transform.position += dir * 250 / 10;
            _objective.transform.position += dir * 1500 / 10;
            await UniTask.DelayFrame(1);
        }

        _beltText.transform.position = beltPos + dir * 250;
        _objective.transform.position = objectivePos + dir * 1500;
    }

    private async UniTask Menu()
    {
        EventSystem.current.SetSelectedGameObject(_menuButton.gameObject);

        while (true)
        {
            if (Input.GetAxis("D_Pad_V") > 0.3 || Input.GetAxis("Vertical") > 0.3)
            {
                _menuButton.navigation.selectOnUp.Select();
                _menuButton = _menuButton.navigation.selectOnUp.GetComponent<Button>();
            }
            if (Input.GetAxis("D_Pad_V") < -0.3 || Input.GetAxis("Vertical") < -0.3)
            {
                _menuButton.navigation.selectOnDown.Select();
                _menuButton = _menuButton.navigation.selectOnDown.GetComponent<Button>();
            }

            await UniTask.DelayFrame(1);

            if (Input.GetAxis("D_Pad_V") == 0 && Input.GetAxis("Vertical") == 0) continue;
        }
    }

    private async UniTask Option()
    {
        EventSystem.current.SetSelectedGameObject(_slider[0].gameObject);

        while (true)
        {
            await UniTask.DelayFrame(1);
            if (!_cursorMoveTask.Status.IsCompleted()) continue;

            _slider[_sliderIndex].value += Input.GetAxis("D_Pad_H");

            if (Input.GetAxis("D_Pad_V") > 0.3 || Input.GetAxis("Vertical") > 0.3)
            {
                _sliderIndex--;
            }
            if (Input.GetAxis("D_Pad_V") < -0.3 || Input.GetAxis("Vertical") < -0.3)
            {
                _sliderIndex++;
            }

            if (Input.GetAxis("D_Pad_V") == 0 && Input.GetAxis("Vertical") == 0) continue;

            if (_sliderIndex > _slider.Length - 1) _sliderIndex = 0;
            if (_sliderIndex < 0) _sliderIndex = _slider.Length - 1;

            _cursorMoveTask = ChangeSelectSlider();
        }
    }

    private async UniTask ChangeSelectSlider()
    {
        Vector2 startPos = _cursor.anchoredPosition;
        Vector2 goalPos = new(_cursor.anchoredPosition3D.x, (1 - _sliderIndex) * 100);

        for (float i = 0; i < 10; i++)
        {
            _cursor.anchoredPosition = Vector2.Lerp(startPos, goalPos, (i + 1 / 10.0f));
            await UniTask.DelayFrame(1);
        }
        _cursor.anchoredPosition = goalPos;
    }

    private async UniTask KeyConfig()
    {
        EventSystem.current.SetSelectedGameObject(_configButton.gameObject);

        while (true)
        {
            if (Input.GetAxis("D_Pad_V") > 0.3 || Input.GetAxis("Vertical") > 0.3)
            {
                _configButton.navigation.selectOnUp.Select();
                _configButton = _configButton.navigation.selectOnUp.GetComponent<Button>();
            }
            if (Input.GetAxis("D_Pad_V") < -0.3 || Input.GetAxis("Vertical") < -0.3)
            {
                _configButton.navigation.selectOnDown.Select();
                _configButton = _configButton.navigation.selectOnDown.GetComponent<Button>();
            }

            await UniTask.DelayFrame(1);
        }
    }

    public void OnBackToTheGame()
    {
        UISwitch();
    }

    public void OnReturnToSelect()
    {
        UISwitch();
        SceneManager.LoadScene("Select");
    }
}
