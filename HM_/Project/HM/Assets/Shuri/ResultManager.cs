using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    ResultRetention result;

    [SerializeField] Text _resultTime;
    [SerializeField] TextMeshProUGUI _rankText;

    [SerializeField] Text[] _bestResultTimes;
    [SerializeField] TextMeshProUGUI[] _bestRankTexts;

    [SerializeField] Image _backGround;

    const float timeS = 60.0f;
    const float timeA = 180.0f;
    const float timeB = 300.0f;
    const float timeC = 480.0f;
    const float timeD = 600.0f;

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
        rankS = new("S", new(Color.white, Gold, Gold, Gold));
        rankA = new("A", new(Color.white, Red, Red, Color.red));
        rankB = new("B", new(Color.white, Orange, Orange, Orange));
        rankC = new("C", new(Color.white, Green, Green, Color.gray));
        rankD = new("D", new(Color.white, Blue, Blue, Blue));
        rankE = new("E", new(Color.white, Gray, Gray, Color.black));

        result = GameObject.Find("RetentionObject").GetComponent<ResultRetention>();

        if (result.GetClearFlag()) _resultTime.text = "Time : " + result.GetClearTime().ToString();
        else _resultTime.text = "Time : --:--";

        Array.Reverse(data.rank);

        if (data.rank[2] <= 0 || result.GetClearTime() < data.rank[2])
        {
            data.rank[2] = Mathf.Ceil(result.GetClearTime() * 100) / 100;
            Save(data);
            
        }

        data = Load(_filepath);
        Array.Sort(data.rank);

        for (int i = 0; i < 3; i++)
        {
            float bestTime;
            Rank bestRank = rankE;

            bestTime = data.rank[i];

            if (bestTime < 0) break;

            _bestResultTimes[i].text = "Time : " + bestTime.ToString("N2");

            switch (bestTime)
            {
                case < timeS: bestRank = rankS; break;
                case < timeA: bestRank = rankA; break;
                case < timeB: bestRank = rankB; break;
                case < timeC: bestRank = rankC; break;
                case < timeD: bestRank = rankD; break;
            }

            _bestRankTexts[i].text = bestRank.rankText;
            _rankText.colorGradientPreset = bestRank.rankColor;
        }


        switch (result.GetClearTime())
        {
            case < timeS: rank = rankS; break;
            case < timeA: rank = rankA; break;
            case < timeB: rank = rankB; break;
            case < timeC: rank = rankC; break;
            case < timeD: rank = rankD; break;
        }
        if (!result.GetClearFlag()) rank = rankE;

        _rankText.text = rank.rankText; 
        _rankText.colorGradientPreset = rank.rankColor;


        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) SceneManager.LoadScene("Select");
    }
}
