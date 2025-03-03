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
    // 現在のランクとタイムを表示するテキスト
    [SerializeField] Text _resultTime;
    [SerializeField] TextMeshProUGUI _rankText;
    [SerializeField] Text _toNextRankTimeText;

    // ハイスコアを表示するテキスト
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

    // ファイルパス
    string _filepath;

    // ファイル名
    private readonly string _fileName = "Data.json";

    void Awake()
    {
        // パス名取得
        _filepath = Application.streamingAssetsPath + "/" + _fileName;

        // ファイルがないとき、ファイル作成
        if (!File.Exists(_filepath)) Save(data, _filepath);

        // ファイルを読み込んでdataに格納
        data = Load<RankingData>(_filepath);
    }

    void Start()
    {
        // ランクの文字と色を設定
        RankDefinition();

        // クリアしていたらタイムを表示
        if (ClearCheck()) _resultTime.text = "TIME : " + GetClearTime().ToString("N2") + "s";
        // クリアしていなかったらタイムなしとして表示
        else _resultTime.text = "TIME : - - : - -";

        // 現在の記録のランクをセット
        rank = RankChecker(GetClearTime());

        // ランクの表示
        _rankText.text = rank.rankText;
        _rankText.colorGradientPreset = rank.rankColor;

        if (ClearCheck()) _toNextRankTimeText.text = "次のランクまで " + ToNextRankTime(GetClearTime()).ToString("N2") + "s";
        else _toNextRankTimeText.text = "次のランクまで - - : - -";

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
            if (i == RankingData.RankCount - 1 && GetClearTime() < data.rank[i] && ClearCheck())
            {
                data.rank[i] = Mathf.Floor(GetClearTime() * 100) / 100;
            }
        }
        // データを保存
        Save(data,_filepath);

        // データの読み込み
        data = Load<RankingData>(_filepath);

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
                    _bestRankTexts[i].colorGradientPreset = rankE.rankColor;
                }
                break;
            }

            // ランキングデータのランクをセット
            Rank bestRank = RankChecker(bestTime);

            // ランキングデータのタイムの表示
            _bestResultTimes[i].text = bestTime.ToString("N2") + "s";

            // ランキングデータのランクの表示
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
        // Bボタンが押されたらセレクト画面に戻る
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) SceneManager.LoadScene("Select");
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) GetComponent<SceneChanger>().ChangeScene();
    }

    /// <summary>
    /// ランクの定義
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
    /// タイムに応じたランクを返す
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
