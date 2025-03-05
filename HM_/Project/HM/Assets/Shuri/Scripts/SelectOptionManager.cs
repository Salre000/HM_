using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static JsonDataModule;

public class SelectOptionManager : MonoBehaviour
{
    private readonly string _FilePathMain = Application.streamingAssetsPath + "/OptionMain.json";
    private readonly string _FilePathSystem = Application.streamingAssetsPath + "/OptionSystem.json";

    [HideInInspector] public OptionDataMain optionDataMain;
    [HideInInspector] public OptionDataSystem optionDataSystem;

    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private Slider[] _slider;

    [SerializeField] private TextMeshProUGUI[] _valueText;

    [SerializeField] private Button[] _decisionButtons;

    [SerializeField] private RectTransform _cursor;

    private int _sliderIndex;

    private const float DeadZone = 0.5f;

    SelectCharacter _selectCharacter;

    public struct Option
    {
        public Slider slider;
        public TextMeshProUGUI value;
        public Option(Slider slider, TextMeshProUGUI value) { this.slider = slider; this.value = value; }
    }

    enum OptionType
    {
        InValid = -1,
        SystemBGM,
        SystemSE,
        Sensibility,
        MainBGM,
        MainSE,
        Max,
    }

    Option[] _optionGroup = new Option[(int)OptionType.Max];

    private UniTask _cursorMoveTask = UniTask.CompletedTask;

    void Start()
    {
        for (int i = 0; i < (int)OptionType.Max; i++)
        {
            _optionGroup[i] = new Option(_slider[i], _valueText[i]);
        }

        _selectCharacter = GetComponent<SelectCharacter>();

        _uiPanel.SetActive(false);

        // 初期値
        optionDataMain.sensibility = optionDataMain.volumeBGM = optionDataMain.volumeSE = 50;
        optionDataSystem.volumeBGM = optionDataSystem.volumeSE = 50;

        // ファイルがないとき、ファイル作成
        if (!File.Exists(_FilePathMain)) Save(optionDataMain, _FilePathMain);
        if (!File.Exists(_FilePathSystem)) Save(optionDataSystem, _FilePathSystem);

        // ファイルを読み込んでOptionDataに格納
        optionDataMain = Load<OptionDataMain>(_FilePathMain);
        optionDataSystem = Load<OptionDataSystem>(_FilePathSystem);

        // Sliderに適用
        _optionGroup[(int)OptionType.SystemBGM].slider.value = optionDataSystem.volumeBGM;
        _optionGroup[(int)OptionType.SystemSE].slider.value = optionDataSystem.volumeSE;
        _optionGroup[(int)OptionType.Sensibility].slider.value = optionDataMain.sensibility;
        _optionGroup[(int)OptionType.MainBGM].slider.value = optionDataMain.volumeBGM;
        _optionGroup[(int)OptionType.MainSE].slider.value = optionDataMain.volumeSE;

        for (int i = 0; i < (int)OptionType.Max; i++) ChangeSliderValue(_optionGroup[i]);

        // コールバック呼び出し
        _optionGroup[(int)OptionType.SystemBGM].slider.onValueChanged.AddListener(delegate { ChangeSliderValue(_optionGroup[(int)OptionType.SystemBGM]); });
        _optionGroup[(int)OptionType.SystemSE].slider.onValueChanged.AddListener(delegate { ChangeSliderValue(_optionGroup[(int)OptionType.SystemSE]); });
        _optionGroup[(int)OptionType.Sensibility].slider.onValueChanged.AddListener(delegate { ChangeSliderValue(_optionGroup[(int)OptionType.Sensibility]); });
        _optionGroup[(int)OptionType.MainBGM].slider.onValueChanged.AddListener(delegate { ChangeSliderValue(_optionGroup[(int)OptionType.MainBGM]); });
        _optionGroup[(int)OptionType.MainSE].slider.onValueChanged.AddListener(delegate { ChangeSliderValue(_optionGroup[(int)OptionType.MainSE]); });

        SoundListManager.instance.SetSoundVolume(optionDataSystem.volumeSE, optionDataSystem.volumeBGM);
    }

    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton11)) UISwitch();
        if (_uiPanel.activeSelf && Input.GetKeyDown(KeyCode.JoystickButton10)) UISwitch();

        if (!_uiPanel.activeSelf) return;

        if (!_cursorMoveTask.Status.IsCompleted()) return;

        _slider[_sliderIndex].value += Input.GetAxis("D_Pad_H");

        if (Input.GetAxis("D_Pad_V") > DeadZone)
        {
            _sliderIndex--;
        }
        else if (Input.GetAxis("D_Pad_V") < -DeadZone)
        {
            _sliderIndex++;
        }
        else return;

        if (_sliderIndex > _slider.Length - 1) _sliderIndex = 0;
        if (_sliderIndex < 0) _sliderIndex = _slider.Length - 1;

        _cursorMoveTask = ChangeSelectSlider();
    }

    private async UniTask ChangeSelectSlider()
    {
        Vector2 startPos = _cursor.anchoredPosition;
        Vector2 goalPos = new(_cursor.anchoredPosition.x, ((1 - _sliderIndex) * 100) - ((_sliderIndex > 1) ? 50 : 0));

        for (float i = 0; i < 10; i++)
        {
            _cursor.anchoredPosition = Vector2.Lerp(startPos, goalPos, (i + 1 / 10.0f));
            await UniTask.DelayFrame(1);
        }
        _cursor.anchoredPosition = goalPos;
    }

    void UISwitch()
    {
        _uiPanel.SetActive(!_uiPanel.activeSelf);

        _sliderIndex = 0;
        _optionGroup[(int)OptionType.SystemBGM].slider.Select();
        _selectCharacter.ReSelectButton();

        for (int i = 0; i < _decisionButtons.Length; i++) _decisionButtons[i].interactable = !_decisionButtons[i].interactable;

        optionDataSystem.volumeBGM = (int)_optionGroup[(int)OptionType.SystemBGM].slider.value;
        optionDataSystem.volumeSE = (int)_optionGroup[(int)OptionType.SystemSE].slider.value;
        optionDataMain.sensibility = (int)_optionGroup[(int)OptionType.Sensibility].slider.value;
        optionDataMain.volumeBGM = (int)_optionGroup[(int)OptionType.MainBGM].slider.value;
        optionDataMain.volumeSE = (int)_optionGroup[(int)OptionType.MainSE].slider.value;
        // セーブ＆リロード
        Save(optionDataMain, _FilePathMain);
        Save(optionDataSystem, _FilePathSystem);
        optionDataMain = Load<OptionDataMain>(_FilePathMain);
        optionDataSystem = Load<OptionDataSystem>(_FilePathSystem);
    }

    public void ChangeSliderValue(Option optionData)
    {
        optionData.value.text = optionData.slider.value.ToString();

        SoundListManager.instance.SetSoundVolume(optionDataSystem.volumeSE, optionDataSystem.volumeBGM);
    }
}
