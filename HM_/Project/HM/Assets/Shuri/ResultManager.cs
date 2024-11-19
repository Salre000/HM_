using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultTime;

    ResultRetention result;

    [SerializeField]TextMeshProUGUI rankText;

    struct Rank
    {
        char rank;
        float time;
        public TMP_ColorGradient rankColor;
        public Rank(char rank,float time, TMP_ColorGradient rankColor) { this.rank = rank; this.time = time; this.rankColor = rankColor; }
    }

    Rank rankS = new Rank('S',60, new TMP_ColorGradient(new Color32(255, 167, 0, 255), new Color32(255, 231, 0, 255), new Color32(255, 231, 0, 255), new Color32(255, 167, 0, 255)));
    Rank rankA = new Rank('A',180, new TMP_ColorGradient(new Color32(255, 0, 166, 255), new Color32(255, 221, 255, 255), new Color32(255, 221, 255, 255), new Color32(240, 128, 176, 255)));
    Rank rankB = new Rank('B', 300, new TMP_ColorGradient(new Color32(245, 130, 47, 255), new Color32(245, 210, 176, 255), new Color32(245, 210, 176, 255), new Color32(245, 130, 47, 255)));

    void Start()
    {
        //result = GameObject.Find("RetentionObject").GetComponent<ResultRetention>();

        //if (result.GetClearFlag()) resultTime.text = "Time : " + result.GetClearTime().ToString();
        //else resultTime.text = "Time : --:--";

        rankText.colorGradientPreset = rankS.rankColor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rankText.colorGradientPreset == rankS.rankColor) rankText.colorGradientPreset = rankA.rankColor;
            else if (rankText.colorGradientPreset == rankA.rankColor) rankText.colorGradientPreset = rankB.rankColor;
            else if (rankText.colorGradientPreset == rankB.rankColor) rankText.colorGradientPreset = rankS.rankColor;
        }
    }
}
