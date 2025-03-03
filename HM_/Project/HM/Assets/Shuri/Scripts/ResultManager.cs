using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static ResultRetention;
using static ResultConst;
using static JsonDataModule;
using Cysharp.Threading.Tasks;

public class ResultManager : MonoBehaviour
{
    // ���݂̃����N�ƃ^�C����\������e�L�X�g
    [SerializeField] Text _resultTime;
    [SerializeField] TextMeshProUGUI _rankText;
    [SerializeField] Text _toNextRankTimeText;

    // �n�C�X�R�A��\������e�L�X�g
    [SerializeField] Text[] _bestResultTimes;
    [SerializeField] TextMeshProUGUI[] _bestRankTexts;

    [SerializeField] Animator _timeAnimator;
    [SerializeField] Animator _nextTimeAnimator;
    [SerializeField] Animator _rankAnimator;

    struct Rank
    {
        public string rankText;
        public TMP_ColorGradient rankColor;
        public Rank(string rank, TMP_ColorGradient rankColor) { this.rankText = rank; this.rankColor = rankColor; }
    }

    Rank rank;

    Rank rankS;
    Rank rankA;
    Rank rankB;
    Rank rankC;
    Rank rankD;
    Rank rankE;

    [HideInInspector] public RankingData data;

    // �t�@�C���p�X
    string _filepath;

    // �t�@�C����
    private readonly string _fileName = "Data.json";

    void Awake()
    {
        // �p�X���擾
        _filepath = Application.streamingAssetsPath + "/" + _fileName;

        // �t�@�C�����Ȃ��Ƃ��A�t�@�C���쐬
        if (!File.Exists(_filepath)) Save(data, _filepath);

        // �t�@�C����ǂݍ����data�Ɋi�[
        data = Load<RankingData>(_filepath);
    }

    void Start()
    {
        // �����N�̕����ƐF��ݒ�
        RankDefinition();

        // �N���A���Ă�����^�C����\��
        if (ClearCheck()) _resultTime.text = "TIME : " + GetClearTime().ToString("N2") + "s";
        // �N���A���Ă��Ȃ�������^�C���Ȃ��Ƃ��ĕ\��
        else _resultTime.text = "TIME : - - : - -";

        // ���݂̋L�^�̃����N���Z�b�g
        rank = RankChecker(GetClearTime());

        // �����N�̕\��
        _rankText.text = rank.rankText;
        _rankText.colorGradientPreset = rank.rankColor;

        if (ClearCheck()) _toNextRankTimeText.text = "���̃����N�܂� " + ToNextRankTime(GetClearTime()).ToString("N2") + "s";
        else _toNextRankTimeText.text = "���̃����N�܂� - - : - -";

        // �����L���O�f�[�^�������ɕ��ёւ�
        Array.Sort(data.rank);

        for (int i = 0; i < RankingData.RankCount; i++)
        {
            // �����L���O�f�[�^�̏�������(������2�ʈȉ��؂�グ)
            if (data.rank[i] <= 0)
            {
                data.rank[i] = Mathf.Floor(GetClearTime() * 100) / 100;
                break;
            }
            if (i == RankingData.RankCount - 1 && GetClearTime() < data.rank[i] && ClearCheck())
            {
                data.rank[i] = Mathf.Floor(GetClearTime() * 100) / 100;
            }
        }
        // �f�[�^��ۑ�
        Save(data,_filepath);

        // �f�[�^�̓ǂݍ���
        data = Load<RankingData>(_filepath);

        // �����Ƀ\�[�g
        Array.Sort(data.rank);

        for (int i = 0; i < 3; i++)
        {
            float bestTime = data.rank[i];

            // �L�^�Ȃ��̏ꍇ�ڍs�̏������Ȃ��ɂ��ăX�L�b�v
            if (bestTime <= 0)
            {
                for (int j = i; j < 3; j++)
                {
                    _bestResultTimes[j].text = "- - : - -";

                    _bestRankTexts[j].text = rankE.rankText;
                    _bestRankTexts[i].colorGradientPreset = rankE.rankColor;
                }
                break;
            }

            // �����L���O�f�[�^�̃����N���Z�b�g
            Rank bestRank = RankChecker(bestTime);

            // �����L���O�f�[�^�̃^�C���̕\��
            _bestResultTimes[i].text = bestTime.ToString("N2") + "s";

            // �����L���O�f�[�^�̃����N�̕\��
            _bestRankTexts[i].text = bestRank.rankText;
            _bestRankTexts[i].colorGradientPreset = bestRank.rankColor;

            Animation();
        }
    }

    async void Animation()
    {
        _timeAnimator.SetTrigger("Time");
        await UniTask.WaitWhile(() => _timeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        _nextTimeAnimator.SetTrigger("NextTime");
        await UniTask.WaitWhile(() => _nextTimeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        _rankAnimator.gameObject.SetActive(true);
        _rankAnimator.SetTrigger("Rank");
        await UniTask.WaitWhile(() => _rankAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
    }

    private void Update()
    {
        // B�{�^���������ꂽ��Z���N�g��ʂɖ߂�
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) SceneManager.LoadScene("Select");
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) GetComponent<SceneChanger>().ChangeScene();
    }

    /// <summary>
    /// �����N�̒�`
    /// </summary>
    void RankDefinition()
    {
        rankS = new("S", new(Color.white, RankColorS, RankColorS, RankColorS));
        rankA = new("Jump", new(Color.white, RankColorA, RankColorA, Color.red));
        rankB = new("Jump", new(Color.white, RankColorB, RankColorB, RankColorB));
        rankC = new("C", new(Color.white, RankColorC, RankColorC, Color.gray));
        rankD = new("D", new(Color.white, RankColorD, RankColorD, RankColorD));
        rankE = new("E", new(Color.white, RankColorE, RankColorE, Color.black));
    }

    /// <summary>
    /// �^�C���ɉ����������N��Ԃ�
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    Rank RankChecker(float time)
    {
        switch (time)
        {
            case <= 0: return rankE;
            case <= RankTimeS: return rankS;
            case <= RankTimeA: return rankA;
            case <= RankTimeB: return rankB;
            case <= RankTimeC: return rankC;
            case <= RankTimeD: return rankD;
            default: return rankE;
        }
    }

    float ToNextRankTime(float time)
    {
        switch (time)
        {
            case <= RankTimeS: return 0;
            case <= RankTimeA: return time - RankTimeS;
            case <= RankTimeB: return time - RankTimeA;
            case <= RankTimeC: return time - RankTimeB;
            case <= RankTimeD: return time - RankTimeC;
        }
        return -1;
    }
}
