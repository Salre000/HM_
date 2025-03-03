using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using static ResultConst;

public class ColorTest : MonoBehaviour
{
    TextMeshProUGUI text;
    Rank rank;

    Rank rankS;
    Rank rankA;
    Rank rankB;
    Rank rankC;
    Rank rankD;
    Rank rankE;
    void Start()
    {
        RankDefinition();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) text.colorGradientPreset = new(Color.red);
        if (Input.GetKey(KeyCode.Q)) text.colorGradientPreset = new(Color.red);
        if (Input.GetKey(KeyCode.W)) text.colorGradientPreset = new(Color.green);
        if (Input.GetKey(KeyCode.S)) text.colorGradientPreset = new(Color.green);
        if (Input.GetKey(KeyCode.E)) text.colorGradientPreset = new(Color.blue);
        if (Input.GetKey(KeyCode.D)) text.colorGradientPreset = new(Color.blue);
    }
    public struct Rank
    {
        public string rankText;
        public TMP_ColorGradient rankColor;
        public Rank(string rank, TMP_ColorGradient rankColor) { this.rankText = rank; this.rankColor = rankColor; }
    }


    void RankDefinition()
    {
        rankS = new("S", new(Color.white, RankColorS, RankColorS, RankColorS));
        rankA = new("Jump", new(Color.white, RankColorA, RankColorA, Color.red));
        rankB = new("Jump", new(Color.white, RankColorB, RankColorB, RankColorB));
        rankC = new("C", new(Color.white, RankColorC, RankColorC, Color.gray));
        rankD = new("D", new(Color.white, RankColorD, RankColorD, RankColorD));
        rankE = new("E", new(Color.white, RankColorE, RankColorE, Color.black));
    }

    public void RankChecker(float time)
    {
        switch (time)
        {
            case <= 0: text.colorGradientPreset = rankE.rankColor;return;
            case <= RankTimeS: text.colorGradientPreset = rankS.rankColor; return;
            case <= RankTimeA: text.colorGradientPreset = rankA.rankColor; return;
            case <= RankTimeB: text.colorGradientPreset = rankB.rankColor; return;
            case <= RankTimeC: text.colorGradientPreset = rankC.rankColor;return;
            case <= RankTimeD: text.colorGradientPreset = rankD.rankColor; return;
        }
        text.colorGradientPreset = rankE.rankColor; 
    }
}
