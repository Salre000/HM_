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

public class ResultManager : MonoBehaviour
{
    [SerializeField] Text _resultTime;
    [SerializeField] TextMeshProUGUI _rankText;

    [SerializeField] Text[] _bestResultTimes;
    [SerializeField] TextMeshProUGUI[] _bestRankTexts;

    [SerializeField] Image _backGround;

    Color32 Gold = new(255, 231, 0, 255);
    Color32 Red = new(255, 50, 150, 255);
    Color32 Orange = new(220, 155, 45, 255);
    Color32 Green = new(0, 255, 128, 255);
    Color32 Blue = new(64, 128, 255, 255);
    Color32 Gray = new(128, 128, 128, 255);

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
    string _fileName = "Data.json";

    void Awake()
    {
        // �p�X���擾
        _filepath = Application.dataPath + "/" + _fileName;

        // �t�@�C�����Ȃ��Ƃ��A�t�@�C���쐬
        if (!File.Exists(_filepath)) Save(data);

        // �t�@�C����ǂݍ����data�Ɋi�[
        data = Load(_filepath);
    }

    // �ۑ�
    void Save(RankingData data)
    {
        // json�ϊ�
        string json = JsonUtility.ToJson(data);

        // �������ݎw��
        StreamWriter wr = new(_filepath, false);

        // ��������
        wr.WriteLine(json);

        // �t�@�C�������
        wr.Close();
    }

    // json�t�@�C���ǂݍ���
    RankingData Load(string path)
    {
        // �ǂݍ��ݎw��
        StreamReader rd = new(path);

        // �t�@�C�����e�S�ēǂݍ���
        string json = rd.ReadToEnd();

        // �t�@�C�������
        rd.Close();

        // json�t�@�C�����^�ɖ߂��ĕԂ�
        return JsonUtility.FromJson<RankingData>(json);
    }

    void Start()
    {
        // �����N�̕����ƐF��ݒ�
        RankDefinition();

        // �N���A���Ă�����^�C����\��
        if (ClearCheck()) _resultTime.text = "TIME : " + GetClearTime().ToString("N2");
        //_resultTime.text = string.Format("TIME : {0}", GetClearTime());
        // �N���A���Ă��Ȃ�������^�C���Ȃ��Ƃ��ĕ\��
        else _resultTime.text = "TIME : - - : - -";

        // ���݂̋L�^�̃����N���Z�b�g
        rank = RankChecker(GetClearTime());

        // �����N�̕\��
        _rankText.text = rank.rankText;
        _rankText.colorGradientPreset = rank.rankColor;

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
            if(i == RankingData.RankCount - 1 && GetClearTime() < data.rank[i] && ClearCheck())
            {
                data.rank[i] = Mathf.Floor(GetClearTime() * 100) / 100;
            }
        }
        // �f�[�^��ۑ�
        Save(data);

        // �f�[�^�̓ǂݍ���
        data = Load(_filepath);

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
                    _rankText.colorGradientPreset = rankE.rankColor;
                }
                break;
            }

            // �����L���O�f�[�^�̃����N���Z�b�g
            Rank bestRank = RankChecker(bestTime);

            // �����L���O�f�[�^�̃^�C���̕\��
            _bestResultTimes[i].text = bestTime.ToString("N2");

            // �����L���O�f�[�^�̃����N�̕\��
            _bestRankTexts[i].text = bestRank.rankText;
            _rankText.colorGradientPreset = bestRank.rankColor;
        }
    }

    private void Update()
    {
        // B�{�^���������ꂽ��Z���N�g��ʂɖ߂�
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) SceneManager.LoadScene("Select");
    }

    void RankDefinition()
    {
        rankS = new("S", new(Color.white, Gold, Gold, Gold));
        rankA = new("A", new(Color.white, Red, Red, Color.red));
        rankB = new("B", new(Color.white, Orange, Orange, Orange));
        rankC = new("C", new(Color.white, Green, Green, Color.gray));
        rankD = new("D", new(Color.white, Blue, Blue, Blue));
        rankE = new("E", new(Color.white, Gray, Gray, Color.black));
    }

    Rank RankChecker(float time)
    {
        switch (time)
        {
            case <= 0: return rankE;
            case < RankTimeS: return rankS;
            case < RankTimeA: return rankA;
            case < RankTimeB: return rankB;
            case < RankTimeC: return rankC;
            case < RankTimeD: return rankD;
            default: return rankE;
        }
    }
}
