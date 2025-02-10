using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Den.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static InputManager;

public class OptionManager : MonoBehaviour
{
    [SerializeField] GameObject selectPanel;
    private RectTransform _panelRect;

    Vector2 baseRectPos;
    Vector2 baseRectSize;

    public int menuIndex = 1;
    int _menuNum;

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

    bool _selected = false;

    UniTask _panelMoveTask = UniTask.CompletedTask;
    UniTask _cursorMoveTask = UniTask.CompletedTask;

    void Start()
    {
        _panelRect = selectPanel.GetComponent<RectTransform>();
        baseRectPos = _panelRect.anchoredPosition;
        baseRectSize = _panelRect.sizeDelta;

        _menuNum = _objective.transform.childCount;

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

    private async void Update()
    {
        if (_selected) return;
     
        // オプション画面の開閉
        if (Input.GetKeyDown(KeyCode.JoystickButton11)) UISwitch();

        // オプション画面が開いていたら
        if (_uiPanel.activeSelf)
        {
            Time.timeScale = 0.0f;

            // RB
            if (Input.GetKey(KeyCode.JoystickButton5) && menuIndex < _menuNum && _panelMoveTask.Status.IsCompleted())
            {
                menuIndex++;
                _panelMoveTask = UIBeltMove(Vector3.left);
            }
            // LB
            if (Input.GetKey(KeyCode.JoystickButton4) && menuIndex > 1 && _panelMoveTask.Status.IsCompleted())
            {
                menuIndex--;
                _panelMoveTask = UIBeltMove(Vector3.right);
            }
        }
        else menuIndex = 1;

        _panelRect.anchoredPosition = baseRectPos;
        _panelRect.sizeDelta = baseRectSize;

        if (!Input.GetKeyDown(KeyCode.JoystickButton3)) return;

        _panelRect.sizeDelta = new(1300, 700);
        _panelRect.anchoredPosition = new(0, -380);

        _selected = true;

        switch (menuIndex)
        {
            case 1: await Menu(); break;
            case 2: await Option(); break;
            case 3: await KeyConfig(); break;
        }

        await UniTask.WaitWhile(() => _selected);
    }

    /// <summary>
    /// オプションUIの開閉切り替え
    /// </summary>
    void UISwitch()
    {
        if (_uiPanel.activeSelf)
        {
            _uiManager.SetSliderValue(
                (int)_sensibilityBar.value,
                (int)_bgmBar.value,
                (int)_seBar.value);

            EventSystem.current.SetSelectedGameObject(null);
            _selected = false;

            _panelRect.anchoredPosition = baseRectPos;
            _panelRect.sizeDelta = baseRectSize;

            Time.timeScale = 1.0f;
        }
        else Time.timeScale = 0.0f;

        _uiPanel.SetActive(!_uiPanel.activeSelf);
    }

    /// <summary>
    /// ベルトの移動処理
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private async UniTask UIBeltMove(Vector3 dir)
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
            await UniTask.DelayFrame(1);

            if (Input.GetAxis("D_Pad_H") > 0.3 || Input.GetAxis("Horizontal") > 0.3)
            {
                _menuButton = _menuButton.navigation.selectOnRight.GetComponent<Button>();
            }
            if (Input.GetAxis("D_Pad_H") < -0.3 || Input.GetAxis("Horizontal") < -0.3)
            {
                _menuButton = _menuButton.navigation.selectOnLeft.GetComponent<Button>();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton10)) break;
        }
        EventSystem.current.SetSelectedGameObject(null);
        _selected = false;
    }

    private async UniTask Option()
    {
        _sliderIndex = 0;

        _slider[0].Select();

        while (true)
        {
            await UniTask.DelayFrame(1);
            if (Input.GetKeyDown(KeyCode.JoystickButton10)) break;

            if (!_cursorMoveTask.Status.IsCompleted()) continue;

            _slider[_sliderIndex].value += Input.GetAxis("D_Pad_H");

            if (Input.GetAxis("D_Pad_V") > 0.3 || Input.GetAxis("Vertical") > 0.3)
            {
                _sliderIndex--;
            }
            else if (Input.GetAxis("D_Pad_V") < -0.3 || Input.GetAxis("Vertical") < -0.3)
            {
                _sliderIndex++;
            }
            else continue;

            if (_sliderIndex > _slider.Length - 1) _sliderIndex = 0;
            if (_sliderIndex < 0) _sliderIndex = _slider.Length - 1;

            _cursorMoveTask = ChangeSelectSlider();
        }
        EventSystem.current.SetSelectedGameObject(null);

        _selected = false;
    }

    private async UniTask ChangeSelectSlider()
    {
        Vector2 startPos = _cursor.anchoredPosition;
        Vector2 goalPos = new(_cursor.anchoredPosition.x, (1 - _sliderIndex) * 100);

        for (float i = 0; i < 10; i++)
        {
            _cursor.anchoredPosition = Vector2.Lerp(startPos, goalPos, (i + 1 / 10.0f));
            await UniTask.DelayFrame(1);
        }
        _cursor.anchoredPosition = goalPos;
    }

    private async UniTask KeyConfig()
    {
        _configButton.Select();

        float scrollValue = 11;

        while (true)
        {
            //if (Input.GetAxis("D_Pad_V") > 0.3 || Input.GetAxis("Vertical") > 0.3)
            //{
            //    _configButton.navigation.selectOnUp.Select();
            //    _configButton = _configButton.navigation.selectOnUp.GetComponent<Button>();
            //    if (scrollValue < 12) scrollValue++;
            //    await UniTask.DelayFrame(10);
            //}
            //if (Input.GetAxis("D_Pad_V") < -0.3 || Input.GetAxis("Vertical") < -0.3)
            //{
            //    _configButton.navigation.selectOnDown.Select();
            //    _configButton = _configButton.navigation.selectOnDown.GetComponent<Button>();
            //    if (scrollValue >= 0) scrollValue--;
            //    await UniTask.DelayFrame(10);
            //}
            await UniTask.DelayFrame(1);

            if (!_inputManager.EnableAllKey()) continue;

            if (Input.GetKeyDown(KeyCode.JoystickButton10)) break;
        }
        EventSystem.current.SetSelectedGameObject(null);

        _selected = false;

        instance.Save();
    }

    public void OnBackToTheGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        _selected = false;
        UISwitch();
    }

    public void OnReturnToSelect()
    {
        UISwitch();
        SceneManager.LoadScene("Select");
    }
}
