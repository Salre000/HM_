using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    enum eSelectCharacter
    {
        Invalid = -1,
        Dragon,
        Spider,
        Max
    }

    enum eChangeButton
    {
        Invalid = -1,
        Left,
        Right,
        Max,
    }

    eSelectCharacter _selectCharacter;
    eSelectCharacter _selectBuf;

    const float DeadZone = 0.5f;

    // �L�����N�^�[�I��p�{�^��
    [SerializeField] private Button[] _charaSelectButtons;

    // �L�����N�^�[�ύX�p�{�^��
    [SerializeField] private Button[] _CharaChangeButtons;

    // �L�����N�^�[���\���e�L�X�g
    [SerializeField] private GameObject[] _nameTexts;

    // �\��������e�N�X�`��
    [SerializeField] private RenderTexture _renderTexture;

    // �f���V�[�����ʂ��J����
    [SerializeField] private Camera[] _demoCamera;

    // �f���A�j���[�V�����A�N�Z�X�p
    [SerializeField] private DemoPlayer[] _demoPlayer;

    // �f���ύX�p�p�[�e�B�N��
    [SerializeField] GameObject _changePerticle;

    // �p�[�e�B�N���̐����ʒu
    [SerializeField] private Transform[] _generateTransforms;

    private void Start()
    {
        _selectCharacter = eSelectCharacter.Dragon;
        _selectBuf = _selectCharacter;
    }

    void Update()
    {
        _selectBuf = _selectCharacter;

        if (Input.GetAxis("D_Pad_H") > DeadZone || Input.GetAxis("Horizontal") > DeadZone)
        {
            if (_selectCharacter != eSelectCharacter.Max - 1)
                _selectCharacter++;
        }
        if (Input.GetAxis("D_Pad_H") < -DeadZone || Input.GetAxis("Horizontal") < -DeadZone)
        {
            if (_selectCharacter != eSelectCharacter.Dragon)
                _selectCharacter--;
        }
        if (_selectCharacter != _selectBuf) ChangeDemoCharacter();
    }

    private async void ChangeDemoCharacter()
    {
        // �G�t�F�N�g�̐���
        for (int i = 0; i < _generateTransforms.Length; i++) Instantiate(_changePerticle, _generateTransforms[i]);
        await UniTask.DelayFrame(5);

        // �A�j���[�V�����̃��Z�b�g
        _demoPlayer[(int)_selectCharacter].ResetAnime();

        // �f���V�[�����ʂ��J�����̕ύX
        _demoCamera[(int)_selectBuf].targetTexture = null;
        _demoCamera[(int)_selectCharacter].targetTexture = _renderTexture;

        for(int i = 0; i < _nameTexts.Length; i++) _nameTexts[i].SetActive(false);
        _nameTexts[(int)_selectCharacter].SetActive(true);

        // �{�^���̑I��ύX
        _charaSelectButtons[(int)_selectCharacter].Select();

        for (int i = 0; i < _CharaChangeButtons.Length; i++) _CharaChangeButtons[i].interactable = true;
        if (_selectCharacter == eSelectCharacter.Max - 1) _CharaChangeButtons[(int)eChangeButton.Right].interactable = false;
        else if (_selectCharacter == eSelectCharacter.Dragon) _CharaChangeButtons[(int)eChangeButton.Left].interactable = false;
    }
}
