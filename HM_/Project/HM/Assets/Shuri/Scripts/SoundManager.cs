using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using static JsonDataModule;

public class SoundManager : MonoBehaviour
{
    private readonly string _FilePathMain = Application.streamingAssetsPath + "/OptionMain.json";
    private readonly string _FilePathOther = Application.streamingAssetsPath + "/OptionOther.json";

    [HideInInspector] public OptionDataMain optionDataMain;
    [HideInInspector] public OptionDataOther optionDataOther;

    [SerializeField] private Slider _bgmBarOther;
    [SerializeField] private Slider _seBarOther;
    [SerializeField] private Slider _sensibilityBar;
    [SerializeField] private Slider _bgmBarMain;
    [SerializeField] private Slider _seBarMain;

    void Start()
    {
        // �����l
        optionDataMain.sensibility = optionDataMain.volumeBGM = optionDataMain.volumeSE = 50;
        optionDataOther.volumeBGM = optionDataOther.volumeSE = 50;

        // �t�@�C�����Ȃ��Ƃ��A�t�@�C���쐬
        if (!File.Exists(_FilePathMain)) Save(optionDataMain, _FilePathMain);
        if (!File.Exists(_FilePathOther)) Save(optionDataOther, _FilePathOther);

        // �t�@�C����ǂݍ����OptionData�Ɋi�[
        optionDataMain = Load<OptionDataMain>(_FilePathMain);
        optionDataOther = Load<OptionDataOther>(_FilePathOther);

        // Slider�ɓK�p
        _bgmBarOther.value = optionDataOther.volumeBGM;
        _seBarOther.value = optionDataOther.volumeSE;
        _sensibilityBar.value = optionDataMain.sensibility;
        _bgmBarMain.value = optionDataMain.volumeBGM;
        _seBarMain.value = optionDataMain.volumeSE;

        
    }

    void Update()
    {
        if (false) return;


    }
}
