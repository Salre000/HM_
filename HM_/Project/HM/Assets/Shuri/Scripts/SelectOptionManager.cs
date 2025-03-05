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

        // �����l
        optionDataMain.sensibility = optionDataMain.volumeBGM = optionDataMain.volumeSE = 50;
        optionDataSystem.volumeBGM = optionDataSystem.volumeSE = 50;

        // �t�@�C�����Ȃ��Ƃ��A�t�@�C���쐬
        if (!File.Exists(_FilePathMain)) Save(optionDataMain, _FilePathMain);
        if (!File.Exists(_FilePathSystem)) Save(optionDataSystem, _FilePathSystem);

        // �t�@�C����ǂݍ����OptionData�Ɋi�[
        optionDataMain = Load<OptionDataMain>(_FilePathMain);
        optionDataSystem = Load<OptionDataSystem>(_FilePathSystem);

        // Slider�ɓK�p
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
