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

    const float timeS = 60.0f;
    const float timeA = 180.0f;
    const float timeB = 300.0f;

    struct Rank
    {
        public string rankText;
        public TMP_ColorGradient rankColor;
        public Rank(string rank, TMP_ColorGradient rankColor) { this.rankText = rank; this.rankColor = rankColor; }
    }

    Rank rank;

    void Start()
    {
        Rank rankS = new Rank("S", new TMP_ColorGradient(new Color32(255, 167, 0, 255), new Color32(255, 231, 0, 255), new Color32(255, 231, 0, 255), new Color32(255, 167, 0, 255)));
        Rank rankA = new Rank("A", new TMP_ColorGradient(new Color32(255, 0, 166, 255), new Color32(255, 221, 255, 255), new Color32(255, 221, 255, 255), new Color32(240, 128, 176, 255)));
        Rank rankB = new Rank("B", new TMP_ColorGradient(new Color32(245, 130, 47, 255), new Color32(245, 210, 176, 255), new Color32(245, 210, 176, 255), new Color32(245, 130, 47, 255)));

        result = GameObject.Find("RetentionObject").GetComponent<ResultRetention>();

        if (result.GetClearFlag()) resultTime.text = "Time : " + result.GetClearTime().ToString();
        else resultTime.text = "Time : --:--";


        switch (result.GetClearTime())
        {
            case < timeS: rank = rankS; break;
            case < timeA: rank = rankA; break;
            case < timeB: rank = rankB; break;
        }
        rankText.text = rank.rankText;
        rankText.colorGradientPreset = rank.rankColor;
        if (!result.GetClearFlag()) rankText.text = "E";
    }
}
