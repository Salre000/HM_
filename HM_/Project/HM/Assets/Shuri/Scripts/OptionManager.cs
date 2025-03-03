using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Den.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
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
    private int _menuButtonIndex = 0;

    [SerializeField] private GameObject _uiPanel;

    [SerializeField] private GameObject _beltText;
    [SerializeField] private GameObject _objective;

    [SerializeField] private Slider[] _slider;
    [SerializeField] private Slider _sensibilityBar;
    [SerializeField] private Slider _bgmBar;
    [SerializeField] private Slider _seBar;
    [SerializeField] private TextMeshProUGUI _sensivilityValue;
    [SerializeField] private TextMeshProUGUI _bgmValue;
    [SerializeField] private TextMeshProUGUI _seValue;
    [SerializeField] private RectTransform _cursor;

    [SerializeField] private Text _noteText;
    [SerializeField] private Text _objectiveText;

    [SerializeField] private Button[] _menuButtons;

    [SerializeField] private Button _configButton;

    private InputManager _inputManager;
    [SerializeField] private HunterManager _hunterManager;

    bool _selected = false;

    private const float DeadZone = 0.5f;

    private UniTask _panelMoveTask = UniTask.CompletedTask;
    private UniTask _cursorMoveTask = UniTask.CompletedTask;

    private string _filepath = "/Option.json";

    void Start()
    {
        _filepath = Application.streamingAssetsPath + _filepath;

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

    private async void Update()
    {
        await UniTask.WaitUntil(() => CameraManager.setupFlag);

        if (_selected) return;

        if (SceneManager.GetActiveScene().buildIndex != 2 && SceneManager.GetActiveScene().buildIndex != 3) return;

        // オプション画面の開閉
        if (Input.GetKeyDown(KeyCode.JoystickButton11)) UISwitch();

        // オプション画面が開いていたら
        if (_uiPanel.activeSelf)
        {
            Time.timeScale = 0.0f;

            // Special1
            if (Input.GetKey(KeyCode.JoystickButton5) && menuIndex < _menuNum && _panelMoveTask.Status.IsCompleted())
            {
                menuIndex++;
                _panelMoveTask = UIBeltMove(Vector3.left);
            }
            // Special2
            if (Input.GetKey(KeyCode.JoystickButton4) && menuIndex > 1 && _panelMoveTask.Status.IsCompleted())
            {
                menuIndex--;
                _panelMoveTask = UIBeltMove(Vector3.right);
            }
        }
        else { menuIndex = 1;return; };

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
        else
        {
            _objectiveText.text = string.Format("▼ハンターを4体倒す {0}/4", _hunterManager.GetHunterDeathAmount());
            _beltText.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            _objective.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Time.timeScale = 0.0f;
        }
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
        EventSystem.current.SetSelectedGameObject(_menuButtons[0].gameObject);

        while (true)
        {
            await UniTask.DelayFrame(1);

            if (Input.GetAxis("D_Pad_H") > DeadZone || Input.GetAxis("Horizontal") > DeadZone) _menuButtonIndex = 1;
            if (Input.GetAxis("D_Pad_H") < -DeadZone || Input.GetAxis("Horizontal") < -DeadZone) _menuButtonIndex = 0;
            
            _menuButtons[_menuButtonIndex].Select();
            if (Input.GetKeyDown(KeyCode.JoystickButton10)) break;

            if (!Input.GetKeyDown(KeyCode.JoystickButton3)) continue;

            switch (_menuButtonIndex)
            {
                case 0: OnBackToTheGame(); break;
                case 1: OnReturnToSelect(); break;
            }
            break;
        }
        EventSystem.current.SetSelectedGameObject(null);
        _selected = false;
    }

    private async void Option()
    {
        _sliderIndex = 0;

        while (true)
        {
            await UniTask.DelayFrame(1);
            if (Input.GetKeyDown(KeyCode.JoystickButton10)) break;

            if (!_cursorMoveTask.Status.IsCompleted()) continue;

            _sensivilityValue.text = _sensibilityBar.value.ToString();
            _bgmValue.text = _bgmBar.value.ToString();
            _seValue.text = _seBar.value.ToString();

            _slider[_sliderIndex].value += Input.GetAxis("D_Pad_H");

            if (Input.GetAxis("D_Pad_V") > DeadZone)
            {
                _sliderIndex--;
            }
            else if (Input.GetAxis("D_Pad_V") < -DeadZone)
            {
                _sliderIndex++;
            }
            else continue;

            if (_sliderIndex > _slider.Length - 1) _sliderIndex = 0;
            if (_sliderIndex < 0) _sliderIndex = _slider.Length - 1;

            _cursorMoveTask = ChangeSelectSlider();
        }
        data.sensibility = (int)_sensibilityBar.value;
        data.volumeBGM = (int)_bgmBar.value;
        data.volumeSE = (int)_seBar.value;

        Save<OptionData>(data, _filepath);

        PlayerStatus.Instance.SetData(data);
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
                _noteText.gameObject.SetActive(true);
                continue;
            }
            _noteText.gameObject.SetActive(false);

            if (Input.GetKeyDown(KeyCode.JoystickButton10)) break;
        }
        EventSystem.current.SetSelectedGameObject(null);

        _selected = false;
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
