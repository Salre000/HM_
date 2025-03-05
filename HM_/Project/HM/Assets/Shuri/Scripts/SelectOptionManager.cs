using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    [SerializeField] private Slider _bgmBarSystem;
    [SerializeField] private Slider _seBarSystem;
    [SerializeField] private Slider _sensibilityBar;
    [SerializeField] private Slider _bgmBarMain;
    [SerializeField] private Slider _seBarMain;

    void Start()
    {
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
        _bgmBarSystem.value = optionDataSystem.volumeBGM;
        _seBarSystem.value = optionDataSystem.volumeSE;
        _sensibilityBar.value = optionDataMain.sensibility;
        _bgmBarMain.value = optionDataMain.volumeBGM;
        _seBarMain.value = optionDataMain.volumeSE;

        SoundListManager.instance.SetSoundVolume(optionDataSystem.volumeSE, optionDataSystem.volumeBGM);
    }

    void Update()
    {
        if (!_uiPanel.activeSelf) return;

        SoundListManager.instance.SetSoundVolume(optionDataSystem.volumeSE, optionDataSystem.volumeBGM);

    }
}
