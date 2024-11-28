using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] Text resultTime;

    ResultRetention result;

    [SerializeField] TextMeshProUGUI rankText;

    [SerializeField] Image backGround;

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

    void Start()
    {
        rankS = new("S", new(Color.white, Gold, Gold, Gold));
        rankA = new("A", new(Color.white, Red, Red, Color.red));
        rankB = new("B", new(Color.white, Orange, Orange, Orange));
        rankC = new("C", new(Color.white, Green, Green, Color.gray));
        rankD = new("D", new(Color.white, Blue, Blue, Blue));
        rankE = new("E", new(Color.white, Gray, Gray, Color.black));


        result = GameObject.Find("RetentionObject").GetComponent<ResultRetention>();

        if (result.GetClearFlag()) resultTime.text = "Time : " + result.GetClearTime().ToString();
        else resultTime.text = "Time : --:--";

        switch (result.GetClearTime())
        {
            case < timeS: rank = rankS; break;
            case < timeA: rank = rankA; break;
            case < timeB: rank = rankB; break;
            case < timeC: rank = rankC; break;
            case < timeD: rank = rankD; break;
        }
        if (!result.GetClearFlag()) rank = rankE;

        rankText.text = rank.rankText;
        rankText.colorGradientPreset = rank.rankColor;
    }
}
