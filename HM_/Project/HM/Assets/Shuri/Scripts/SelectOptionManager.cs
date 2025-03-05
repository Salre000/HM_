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

    Option[] _optionGroup=new Option[(int)OptionType.Max];

    void Start()
    {
        for (int i = 0; i < (int)OptionType.Max; i++)
        {
            _optionGroup[i] = new Option(_slider[i], _valueText[i]);
        }

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

        _optionGroup[0].slider.onValueChanged.AddListener(delegate { ValueChangeCheck(_optionGroup[0]); });
        _optionGroup[1].slider.onValueChanged.AddListener(delegate { ValueChangeCheck(_optionGroup[1]); });
        _optionGroup[2].slider.onValueChanged.AddListener(delegate { ValueChangeCheck(_optionGroup[2]); });
        _optionGroup[3].slider.onValueChanged.AddListener(delegate { ValueChangeCheck(_optionGroup[3]); });
        _optionGroup[4].slider.onValueChanged.AddListener(delegate { ValueChangeCheck(_optionGroup[4]); });

        SoundListManager.instance.SetSoundVolume(optionDataSystem.volumeSE, optionDataSystem.volumeBGM);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton10)) _uiPanel.SetActive(!_uiPanel.activeSelf);

        if (!_uiPanel.activeSelf) return;

        SoundListManager.instance.SetSoundVolume(optionDataSystem.volumeSE, optionDataSystem.volumeBGM);

    }

    public void ValueChangeCheck(Option optionData)
    {
        optionData.value.text = optionData.slider.value.ToString();
    }
}
