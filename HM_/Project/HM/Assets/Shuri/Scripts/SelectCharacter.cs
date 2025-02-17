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

    // キャラクター選択用ボタン
    [SerializeField] private Button[] _charaSelectButtons;

    // キャラクター変更用ボタン
    [SerializeField] private Button[] _CharaChangeButtons;

    // キャラクター名表示テキスト
    [SerializeField] private GameObject[] _nameTexts;

    // 表示させるテクスチャ
    [SerializeField] private RenderTexture _renderTexture;

    // デモシーンを写すカメラ
    [SerializeField] private Camera[] _demoCamera;

    // デモアニメーションアクセス用
    [SerializeField] private DemoPlayer[] _demoPlayer;

    // デモ変更用パーティクル
    [SerializeField] GameObject _changePerticle;

    // パーティクルの生成位置
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
        // エフェクトの生成
        for (int i = 0; i < _generateTransforms.Length; i++) Instantiate(_changePerticle, _generateTransforms[i]);
        await UniTask.DelayFrame(5);

        // アニメーションのリセット
        _demoPlayer[(int)_selectCharacter].ResetAnime();

        // デモシーンを写すカメラの変更
        _demoCamera[(int)_selectBuf].targetTexture = null;
        _demoCamera[(int)_selectCharacter].targetTexture = _renderTexture;

        for(int i = 0; i < _nameTexts.Length; i++) _nameTexts[i].SetActive(false);
        _nameTexts[(int)_selectCharacter].SetActive(true);

        // ボタンの選択変更
        _charaSelectButtons[(int)_selectCharacter].Select();

        for (int i = 0; i < _CharaChangeButtons.Length; i++) _CharaChangeButtons[i].interactable = true;
        if (_selectCharacter == eSelectCharacter.Max - 1) _CharaChangeButtons[(int)eChangeButton.Right].interactable = false;
        else if (_selectCharacter == eSelectCharacter.Dragon) _CharaChangeButtons[(int)eChangeButton.Left].interactable = false;
    }
}
