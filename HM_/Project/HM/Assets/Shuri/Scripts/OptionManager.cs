using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Den.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static JsonDataModule;

public class OptionManager : MonoBehaviour
{
    [HideInInspector] public OptionData data;

    [SerializeField] private GameObject selectPanel;
    private RectTransform _panelRect;

    private readonly Vector2 _SelectRectPos = new(0, -380);
    private readonly Vector2 _SelectRectSize = new(1300, 640);

    private Vector2 _baseRectPos;
    private Vector2 _baseRectSize;

    public int menuIndex = 1;
    private int _menuNum;

    private int _sliderIndex;

    [SerializeField] private GameObject _uiPanel;

    [SerializeField] private GameObject _beltText;
    [SerializeField] private GameObject _objective;

    [SerializeField] private Slider[] _slider;
    [SerializeField] private Slider _sensibilityBar;
    [SerializeField] private Slider _bgmBar;
    [SerializeField] private Slider _seBar;
    [SerializeField] private RectTransform _cursor;

    [SerializeField] private Text _noteText;

    [SerializeField] private Button _menuButton;

    [SerializeField] private Button _configButton;

    private InputManager _inputManager;

    bool _selected = false;

    const int UISwitchDelayFrame = 10;

    private UniTask _panelMoveTask = UniTask.CompletedTask;
    private UniTask _cursorMoveTask = UniTask.CompletedTask;

    private string _filepath = "/Option.json";

    void Start()
    {
        _filepath = Application.dataPath + _filepath;

        _panelRect = selectPanel.GetComponent<RectTransform>();
        _baseRectPos = _panelRect.anchoredPosition;
        _baseRectSize = _panelRect.sizeDelta;

        _menuNum = _objective.transform.childCount;

        _uiPanel.SetActive(false);

        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        _cursor.anchoredPosition = new(_cursor.anchoredPosition3D.x, (1 - _sliderIndex) * 100);

        data.sensibility = data.volumeBGM = data.volumeSE = 50;

        // ファイルがないとき、ファイル作成
        if (!File.Exists(_filepath)) Save(data, _filepath);

        // ファイルを読み込んでdataに格納
        data = Load<OptionData>(_filepath);

        _sensibilityBar.value = data.sensibility;
        _bgmBar.value = data.volumeBGM;
        _seBar.value = data.volumeSE;
    }

    private void Update()
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

        _panelRect.anchoredPosition = _baseRectPos;
        _panelRect.sizeDelta = _baseRectSize;

        if (!Input.GetKeyDown(KeyCode.JoystickButton3)) return;

        _panelRect.sizeDelta = _SelectRectSize;
        _panelRect.anchoredPosition = _SelectRectPos;

        _selected = true;

        switch (menuIndex)
        {
            case 1: Menu(); break;
            case 2: Option(); break;
            case 3: KeyConfig(); break;
        }
    }

    /// <summary>
    /// オプションUIの開閉切り替え
    /// </summary>
    void UISwitch()
    {
        if (_uiPanel.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            _selected = false;

            _panelRect.anchoredPosition = _baseRectPos;
            _panelRect.sizeDelta = _baseRectSize;

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

    private async void Menu()
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

    private async void Option()
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

    private async void KeyConfig()
    {
        _configButton.Select();

        while (true)
        {
            await UniTask.DelayFrame(1);

            if (!_inputManager.EnableAllKey())
            {
                if (_noteText.color.a == 0) DisplayNoteText();
                continue;
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton10)) break;
        }
        EventSystem.current.SetSelectedGameObject(null);

        _selected = false;
    }

    private async void DisplayNoteText()
    {
        for (int alpha = 255; alpha > 0; alpha--)
        {
            _noteText.color = new(255, 255, 255, alpha);
            await UniTask.DelayFrame(1);
        }
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
