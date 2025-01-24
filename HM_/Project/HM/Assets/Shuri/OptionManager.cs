using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public int menuIndex = 1;
    int menuNum = 4;

    private int _sliderIndex;
    private int _buttonIndex;

    [SerializeField] TextAsset _option;

    [SerializeField] GameObject _uiPanel;
    [SerializeField] GameObject _beltText;
    [SerializeField] GameObject _objective;

    [SerializeField] Slider[] _slider;
    [SerializeField] Slider _sensibilityBar;
    [SerializeField] Slider _bgmBar;
    [SerializeField] Slider _seBar;
    [SerializeField] RectTransform _cursor;

    [SerializeField] Button button;
    [SerializeField] Button[] _buttons;

    InputManager _inputManager;

    UIManager _uiManager;

    UniTask _panelMoveTask = UniTask.CompletedTask;
    UniTask _cursorMoveTask = UniTask.CompletedTask;

    void Start()
    {
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

    void Update()
    {
        // オプション画面の開閉
        if (Input.GetKeyDown(_inputManager.config.start)) UISwitch();
        
        // オプション画面が開いていたら
        if (_uiPanel.activeSelf)
        {
            Time.timeScale = 0.0f;

            // RB
            if (Input.GetKeyDown(_inputManager.config.rb) && menuIndex < menuNum && _panelMoveTask.Status.IsCompleted())
            {
                menuIndex++;
                _panelMoveTask = UIMove(Vector3.left);
            }
            // LB
            if (Input.GetKeyDown(_inputManager.config.lb) && menuIndex > 1 && _panelMoveTask.Status.IsCompleted())
            {
                menuIndex--;
                _panelMoveTask = UIMove(Vector3.right);
            }
        }

        switch (menuIndex)
        {
            case 1: Menu(); break;
            case 2: break;
            case 3: Option(); break;
            case 4: break;
        }
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

    void Menu()
    {
        if (Input.GetAxis("D_Pad_V") > 0.3 || Input.GetAxis("Vertical") > 0.3)
        {
            button.navigation.selectOnUp.Select();
            button = button.navigation.selectOnUp.GetComponent<Button>();
        }
        if (Input.GetAxis("D_Pad_V") < -0.3 || Input.GetAxis("Vertical") < -0.3)
        {
            button.navigation.selectOnDown.Select();
            button = button.navigation.selectOnDown.GetComponent<Button>();
        }

        if (Input.GetAxis("D_Pad_V") == 0 && Input.GetAxis("Vertical") == 0) return;
    }

    void Option()
    {
        if (!_cursorMoveTask.Status.IsCompleted()) return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_slider[_sliderIndex].gameObject);

        _slider[_sliderIndex].value += Input.GetAxis("D_Pad_H");

        if (Input.GetAxis("D_Pad_V") > 0.3 || Input.GetAxis("Vertical") > 0.3)
        {
            _sliderIndex--;
        }
        if (Input.GetAxis("D_Pad_V") < -0.3 || Input.GetAxis("Vertical") < -0.3)
        {
            _sliderIndex++;
        }
        if (Input.GetAxis("D_Pad_V") == 0 && Input.GetAxis("Vertical") == 0) return;

        if (_sliderIndex > _slider.Length - 1) _sliderIndex = 0;
        if (_sliderIndex < 0) _sliderIndex = _slider.Length - 1;

        _cursorMoveTask = ChangeSelectSlider();
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

    private void KeyConfig()
    {

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
