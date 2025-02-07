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

    // ファイルパス
    string _filepath;

    // ファイル名
    string _fileName = "Data.json";

    void Awake()
    {
        // パス名取得
        _filepath = Application.dataPath + "/" + _fileName;

        // ファイルがないとき、ファイル作成
        if (!File.Exists(_filepath)) Save(data);

        // ファイルを読み込んでdataに格納
        data = Load(_filepath);
    }

    // 保存
    void Save(RankingData data)
    {
        // json変換
        string json = JsonUtility.ToJson(data);

        // 書き込み指定
        StreamWriter wr = new(_filepath, false);

        // 書き込み
        wr.WriteLine(json);

        // ファイルを閉じる
        wr.Close();
    }

    // jsonファイル読み込み
    RankingData Load(string path)
    {
        // 読み込み指定
        StreamReader rd = new(path);

        // ファイル内容全て読み込む
        string json = rd.ReadToEnd();

        // ファイルを閉じる
        rd.Close();

        // jsonファイルを型に戻して返す
        return JsonUtility.FromJson<RankingData>(json);
    }

    void Start()
    {
        // ランクの文字と色を設定
        RankDefinition();

        // クリアしていたらタイムを表示
        if (ClearCheck()) _resultTime.text = "TIME : " + GetClearTime().ToString("N2");
        //_resultTime.text = string.Format("TIME : {0}", GetClearTime());
        // クリアしていなかったらタイムなしとして表示
        else _resultTime.text = "TIME : - - : - -";

        // 現在の記録のランクをセット
        rank = RankChecker(GetClearTime());

        // ランクの表示
        _rankText.text = rank.rankText;
        _rankText.colorGradientPreset = rank.rankColor;

        // ランキングデータを昇順に並び替え
        Array.Sort(data.rank);

        for (int i = 0; i < RankingData.RankCount; i++) 
        {
            // ランキングデータの書き換え(小数第2位以下切り上げ)
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
        // データを保存
        Save(data);

        // データの読み込み
        data = Load(_filepath);

        // 昇順にソート
        Array.Sort(data.rank);

        for (int i = 0; i < 3; i++)
        {
            float bestTime = data.rank[i];

            // 記録なしの場合移行の処理をなしにしてスキップ
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

            // ランキングデータのランクをセット
            Rank bestRank = RankChecker(bestTime);

            // ランキングデータのタイムの表示
            _bestResultTimes[i].text = bestTime.ToString("N2");

            // ランキングデータのランクの表示
            _bestRankTexts[i].text = bestRank.rankText;
            _rankText.colorGradientPreset = bestRank.rankColor;
        }
    }

    private void Update()
    {
        // Bボタンが押されたらセレクト画面に戻る
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
